using System;
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

            // .jws files are zip files which contain an xml file representing the order of worship and
            // a collection of zero or more images related to the order and referenced by it. We need
            // to open the file as a zip archive and extract each piece

            // Load the contents of the jws
            var stream = await file.OpenSequentialReadAsync();
            WorshipServiceOrder order = null;
            Collection<WorshipServiceImage> images = new Collection<WorshipServiceImage>();

            using (ZipArchive jws = new ZipArchive(stream.AsStreamForRead(), ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in jws.Entries)
                {
                    if (entry.Name.EndsWith(".xml"))
                    {
                        var result = await LoadOrder(entry);

                        if (result != null &&
                            order != null)
                            throw new InvalidOperationException("Unable to load service. Multiple orders found in file.");

                        order = result;
                    }

                    else if (entry.Name.EndsWith(".jpg"))
                    {
                        using (Stream s = entry.Open())
                        {
                            images.Add(new WorshipServiceImage(entry.Name, s));
                        }
                    }
                }
            }

            if (order == null)
                throw new InvalidOperationException("Unable to load service. No order found in file.");

            return new WorshipService(order, images);
        }

        private async Task<WorshipServiceOrder> LoadOrder(ZipArchiveEntry entry)
        {
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
                    doc.Validate(schemas, (o, e) =>
                    {
                        errors.Add(e.Message);
                    });

                    if (errors.Any())
                        return null;
                }

                // De-serialize the xml into models
                var serializer = new XmlSerializer(typeof(WorshipServiceOrder));
                return serializer.Deserialize(doc.CreateReader()) as WorshipServiceOrder;
            }
        }
    }
}
