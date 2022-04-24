using Jason.Models;

namespace Jason.UnitTests.Mocks
{
    public class MockScripture : IScripture
    {
        public string Reference { get; set; }
        public string Text { get; set; }
        public string BackgroundImageName { get; set; }
        public ITranslation Translation { get; set; }
        public ScriptureBook Book { get; set; }
    }
}
