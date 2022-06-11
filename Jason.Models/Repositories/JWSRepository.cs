using Jason.Interfaces.Services;
using Syncfusion.Presentation;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using Jason.Interfaces.WorshipService;

namespace Jason.Models.Repositories
{
    /// <summary>
    /// Provides access to worship service data stored in a .jws file
    /// </summary>
    /// <remarks>
    /// A .jws file is a .zip archive with the following entries:
    /// - order.xml - The order of worship
    /// - Images     - A directory containing images referenced by the order of worship
    /// - Slideshows - A directory containing slideshows referenced by the order of worship
    /// </remarks>
    public class JWSRepository : IWorshipServiceRepository
    {
        #region Fields
        private const string imageFolderName = "Images";
        private const string slideshowFolderName = "Slideshows";
        private const string orderEntryName = "order.xml";
        private const string extension = ".jws";
        private readonly IStorageService filesService;
        #endregion

        #region Constructors
        public JWSRepository(IStorageService filesService)
        {
            if (filesService == null)
                throw new ArgumentNullException(nameof(filesService));

            this.filesService = filesService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new <see cref="IWorshipService"/>
        /// </summary>
        /// <returns>
        /// An empty <see cref="IWorshipService"/>
        /// </returns>
        public IWorshipService Create()
        {
            return new WorshipService()
            {
                Order = new WorshipServiceOrder()
            };
        }

        /// <summary>
        /// Loads an existing <see cref="IWorshipService"/>
        /// asynchronously
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> which returns a <see cref="IWorshipService"/>
        /// when complete
        /// </returns>
        public async Task<IWorshipService> LoadAsync()
        {
            IStorageFile storage = await filesService.GetSingleFileAsync(extension);
            if (storage == null)
                return null;

            // Convert the .jws file into a worship service order
            using (IInputStream stream = await storage.OpenSequentialReadAsync())
            using (ZipArchive jws = new ZipArchive(stream.AsStreamForRead(), ZipArchiveMode.Read))
            {
                // Load the order
                ZipArchiveEntry entry = jws.GetEntry(orderEntryName);
                if (entry == null)
                    throw new InvalidOperationException("Unable to load service. No order found in file.");

                // Deserialize the order
                using (Stream s = entry.Open())
                {
                    // Attempt to load as XML
                    XDocument doc = await XDocument.LoadAsync(s, LoadOptions.None, CancellationToken.None);

                    // Validate the XML against the XSD
                    ValidateWorshipServiceOrder(doc);

                    // De-serialize the xml into models
                    var serializer = new XmlSerializer(typeof(WorshipServiceOrder));
                    WorshipServiceOrder order = serializer.Deserialize(doc.CreateReader()) as WorshipServiceOrder;

                    // Create the worship service object
                    IWorshipService service = new WorshipService();
                    service.Name = storage.Name;
                    service.Location = storage.Path;

                    // load images
                    foreach (ZipArchiveEntry imageEntry in jws.Entries
                                                              .Where(e => e.FullName.StartsWith($"{imageFolderName}/") &&
                                                                          e.Name.ToLower().EndsWith(".jpg")))
                    {
                        using (Stream imageEntryStream = imageEntry.Open())
                        {
                            service.Images.Add(new WorshipServiceImage(imageEntry.Name, imageEntryStream));
                        }
                    }

                    // Load presentations
                    foreach (ZipArchiveEntry presentationEntry in jws.Entries
                                                                     .Where(e => e.FullName.StartsWith($"{slideshowFolderName}/") &&
                                                                                 e.Name.ToLower().EndsWith(".pptx")))
                    {
                        using (Stream presentationStream = presentationEntry.Open())
                        using (MemoryStream ms = new MemoryStream())
                        {
                            presentationStream.CopyTo(ms);
                            service.Presentations.Add(new PowerpointPresentation()
                            {
                                Name = presentationEntry.Name,
                                Presentation = Presentation.Open(ms)
                            });
                        }
                    }

                    return service;
                }
            }
        }

        /// <summary>
        /// Saves a <see cref="IWorshipServiceOrder"/>
        /// asynchronously
        /// </summary>
        /// <param name="service">
        /// The <see cref="IWorshipServiceOrder"/> to save 
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the save operation
        /// </returns>
        public async Task<bool> SaveAsync(IWorshipService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            bool completed = true;

            if (string.IsNullOrEmpty(service.Location))
                completed = await SaveAsAsync(service);
            else
            {
                IStorageFile storage = await filesService.GetExistingFileAsync(service.Location);
                if (storage == null)
                    throw new Exception("Unable to obtain save file");

                // Clear the contents of the stream
                using (Stream s = await storage.OpenStreamForWriteAsync())
                {
                    s.SetLength(0);
                    await s.FlushAsync();
                }

                await SaveServiceToZip(service, storage);
            }

            return completed;
        }

        /// <summary>
        /// Saves a <see cref="IWorshipService"/> asynchronously to a
        /// newly selected location
        /// </summary>
        /// <param name="service">
        /// The <see cref="IWorshipService"/> to save 
        /// </param>
        public async Task<bool> SaveAsAsync(IWorshipService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (service.Order == null)
                throw new ArgumentException("Order cannot be null. The service must have an order defined.", nameof(service));

            IDictionary<string, IList<string>> suggestedExtensions = new Dictionary<string, IList<string>>();
            suggestedExtensions.Add($"Jason Worship Service ({extension})",
                                    new Collection<string>() { extension });
            IStorageFile storage = await filesService.GetSaveFileAsync(extension,
                                                                       service.Name,
                                                                       suggestedExtensions);

            if (storage == null)
                return false;

            await SaveServiceToZip(service, storage);
            service.Name = storage.Name;
            service.Location = storage.Path;
            
            return true;
        }

        private async Task SaveServiceToZip(IWorshipService service, IStorageFile storage)
        {
            using (Stream stream = await storage.OpenStreamForWriteAsync())
            using (ZipArchive jws = new ZipArchive(stream, ZipArchiveMode.Update))
            {
                // Create an entry for the order
                using (Stream s = jws.CreateEntry(orderEntryName)
                                     .Open())
                {
                    service.Order.Serialize(s);
                }

                // Create image entries
                foreach (IWorshipServiceImage image in service.Images)
                {
                    using (Stream s = jws.CreateEntry($"{imageFolderName}/{image.Name}")
                                         .Open())
                    {
                        image.AsMemoryStream().CopyTo(s);
                    }
                }

                // Create Presentation entries
                foreach (IPowerpointPresentation presentation in service.Presentations)
                {
                    using (Stream s = jws.CreateEntry($"{slideshowFolderName}/{presentation.Name}")
                                          .Open())
                    {
                        presentation.Presentation.Save(s);
                    }
                }
            }
        }

        /// <summary>
        /// Validates an <see cref="XDocument"/> against the XSD for
        /// worship service orders and throws an exception if there are errors
        /// </summary>
        /// <param name="doc">
        /// The document to validate
        /// </param>
        private void ValidateWorshipServiceOrder(XDocument doc)
        {
            if (doc == null)
                throw new InvalidOperationException("order.xml is not valid XML");

            using (StreamReader xsdReader = new StreamReader(this.GetType()
                                                                .Assembly
                                                                .GetManifestResourceStream("Jason.Models.XML.WorshipServiceOrder.xsd")))
            {
                // Load the xsd into the schema set
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", XmlReader.Create(xsdReader));

                Collection<string> errors = new Collection<string>();
                try
                {
                    doc.Validate(schemas, (o, e) =>
                    {
                        errors.Add(e.Message);
                    });
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Unable to load service. The parser threw an error", ex);
                }

                if (errors.Any())
                    throw new InvalidOperationException("Unable to load service. The order contained errors.");
            }
        }
        #endregion
    }
}
