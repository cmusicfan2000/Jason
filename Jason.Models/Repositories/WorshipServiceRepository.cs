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
using Windows.Storage.Pickers;

namespace Jason.Models.Repositories
{
    public class WorshipServiceRepository
    {
        public void Save(WorshipService service)
        {

        }

        public async Task<WorshipService> GetWorshipServiceAsync()
        {
            var picker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".jws");

            var file = await picker.PickSingleFileAsync();

            if (file == null)
                return null;

            // .jws file structure:
            // order.xml - The order of worship
            // Images    - A directory containing images referenced by the order of worship
            // Songs     - A directory containing slideshows with songs pertaining to the service

            // Load the contents of the jws
            var stream = await file.OpenSequentialReadAsync();
            WorshipService service = new WorshipService();

            using (ZipArchive jws = new ZipArchive(stream.AsStreamForRead(), ZipArchiveMode.Read))
            {
                // Load the order
                service.Order = await LoadOrder(jws.GetEntry("order.xml"));

                // Load images
                IEnumerable<ZipArchiveEntry> imageEntries = jws.Entries.Where(e => e.FullName.StartsWith("Images/") &&
                                                                                   e.Name.EndsWith(".jpg"));
                foreach (ZipArchiveEntry entry in imageEntries)
                {
                    using (Stream s = entry.Open())
                    {
                        service.Images.Add(new WorshipServiceImage(entry.Name, s));
                    }
                }

                // Load song slides
                IEnumerable<ZipArchiveEntry> songEntries = jws.Entries.Where(e => e.FullName.StartsWith("Songs/") &&
                                                                                  e.Name.EndsWith(".pptx"));
                foreach (ZipArchiveEntry entry in songEntries)
                {
                    using (Stream s = entry.Open())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            s.CopyTo(ms);
                            service.Songs.Add(entry.Name, Presentation.Open(ms));
                        }
                    }
                }
            }

            return service;
        }

        private async Task<WorshipServiceOrder> LoadOrder(ZipArchiveEntry entry)
        {
            if (entry == null)
                throw new InvalidOperationException("Unable to load service. No order found in file.");

            using (Stream s = entry.Open())
            {
                // Attempt to load as XML
                XDocument doc = await XDocument.LoadAsync(s, LoadOptions.None, CancellationToken.None);

                if (doc == null)
                    return null;

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

                // De-serialize the xml into models
                var serializer = new XmlSerializer(typeof(WorshipServiceOrder));

                WorshipServiceOrder wso;
                try
                {

                    wso = serializer.Deserialize(doc.CreateReader()) as WorshipServiceOrder;
                }
                catch (Exception ex)
                {

                    throw;
                }


                return wso;
            }
        }
    }
}
