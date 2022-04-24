using Jason.Models;

namespace Jason.UnitTests.Mocks
{
    public class MockSong : ISong
    {
        public ushort? SongBookNumber { get; set; }
        public string Title { get; set; }
        public string Slideshow { get; set; }
        public ISongPart[] Parts { get; set; }
    }
}