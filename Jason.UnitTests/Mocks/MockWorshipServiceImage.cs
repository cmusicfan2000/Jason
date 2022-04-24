using Jason.Models;
using System.IO;

namespace Jason.UnitTests.Mocks
{
    public class MockWorshipServiceImage : IWorshipServiceImage
    {
        public string Name { get; set; }
        public MemoryStream DataStream { get; set; }
     
        public MockWorshipServiceImage(string name)
        {
            Name = name;
        }

        public MemoryStream AsMemoryStream()
            => DataStream;

        public void SetData(Stream source)
        {
            DataStream = new MemoryStream();
            source.CopyTo(DataStream);
        }
    }
}
