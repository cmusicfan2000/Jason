using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace Jason.Models
{
    /// <summary>
    /// Provides access to worship service data stored in a .jws file
    /// </summary>
    /// <remarks>
    /// A .jws file is a .zip archive with the following entries:
    /// - order.xml - The order of worship
    /// - Images   - A directory containing images referenced by the order of worship
    /// - Songs    - A directory containing slideshows with songs referenced by the order of worship
    /// </remarks>
    public class JWSWorshipServiceAccessor : IWorshipServiceAccessor
    {
        /// <summary>
        /// Creates a new <see cref="IWorshipServiceOrder"/>
        /// asynchronously
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> which returns a <see cref="IWorshipServiceOrder"/>
        /// when complete
        /// </returns>
        public Task<IWorshipServiceOrder> CreateAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads an existing <see cref="IWorshipServiceOrder"/>
        /// asynchronously
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> which returns a <see cref="IWorshipServiceOrder"/>
        /// when complete
        /// </returns>
        public async Task<IWorshipServiceOrder> LoadAsync()
        {
            // Obtain a file with the .jws extension
            var picker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".jws");

            StorageFile storage = await picker.PickSingleFileAsync();
            if (storage == null)
                return null;

            // Convert the .jws file into a worship service order
            using (IInputStream stream = await storage.OpenSequentialReadAsync())
            using (ZipArchive jws = new ZipArchive(stream.AsStreamForRead(), ZipArchiveMode.Read))
            {
                // Load the order
                ZipArchiveEntry entry = jws.GetEntry("order.xml");
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

                    // Load image and song presentation entries
                    IEnumerable<ZipArchiveEntry> imageEntries = jws.Entries.Where(e => e.FullName.StartsWith("Images/") &&
                                                                                       e.Name.ToLower().EndsWith(".jpg"));
                    IEnumerable<ZipArchiveEntry> songEntries = jws.Entries.Where(e => e.FullName.StartsWith("Songs/") &&
                                                                                      e.Name.ToLower().EndsWith(".pptx"));

                    // load images
                    foreach (ImageBackground ib in order.Items
                                                        .OfType<ImageBackground>()
                                                        .Where(x => !string.IsNullOrEmpty(x?.BackgroundImageName)))
                    {
                        ZipArchiveEntry imageEntry = imageEntries.SingleOrDefault(ie => ie.Name == $"{ib.BackgroundImageName}.jpg");

                        if (imageEntry != null)
                        {
                            using (Stream imageEntryStream = imageEntry.Open())
                            {
                                ib.Image = new WorshipServiceImage(ib.BackgroundImageName, imageEntryStream);
                            }
                        }
                    }

                    // Load song presentations
                    foreach (Song song in order.Parts
                                                .OfType<Song>()
                                                .Where(x => !string.IsNullOrEmpty(x?.Slideshow)))
                    {
                        ZipArchiveEntry songEntry = songEntries.SingleOrDefault(ie => ie.Name == $"{song.Slideshow}.pptx");

                        if (songEntry != null)
                        {
                            using (Stream songStream = songEntry.Open())
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    s.CopyTo(ms);
                                    song.Presentation = new PowerpointPresentation()
                                    { 
                                        Name = song.Slideshow,
                                        Presentation = Presentation.Open(ms)
                                    };
                                }
                            }
                        }
                    }

                    return order;
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
        public Task SaveAsync(IWorshipServiceOrder service)
        {
            // TODO: Make sure to only output images that belong in the file. If the image is no longer being used remove/don't output it

            throw new NotImplementedException();
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
    }
}
