using Jason.Models;

namespace Jason.UnitTests.Mocks
{
    public class MockTranslation : ITranslation
    {
        public string FullName { get; set; }
        public string Abbreviation { get; set; }
    }
}
