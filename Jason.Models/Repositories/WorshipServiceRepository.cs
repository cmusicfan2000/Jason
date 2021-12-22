using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Windows.Storage;

namespace Jason.Models.Repositories
{
    public class WorshipServiceRepository
    {
        public async Task<WorshipService> GetWorshipServiceAsync(StorageFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            // Load the contents of the file as XML
            var stream = await file.OpenSequentialReadAsync();
            XDocument doc = await XDocument.LoadAsync(stream.AsStreamForRead(), LoadOptions.None, CancellationToken.None);

            using (StreamReader xsdReader = new StreamReader(this.GetType()
                                                          .Assembly
                                                          .GetManifestResourceStream("Jason.Models.XML.WorshipService.xsd")))
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
                    throw new InvalidOperationException("Unable to load worship service from the file provided." + Environment.NewLine +
                                                         Environment.NewLine +
                                                         "Additional Info:" + Environment.NewLine +
                                                         errors.Aggregate((x,y) => x + Environment.NewLine + y));

                // De-serialize the xml into models
                var serializer = new XmlSerializer(typeof(WorshipService));
                return serializer.Deserialize(doc.CreateReader()) as WorshipService;
            }
        }
    }
}
