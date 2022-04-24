using Jason.Models;

namespace Jason.UnitTests.Mocks
{
    public class MockWorshipServicePart : IWorshipServicePart
    {
        public WorshipServicePartTypes Type { get; set; }
    }
}