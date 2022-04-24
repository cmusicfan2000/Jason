using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jason.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviours of the <see cref="SongPart"/> class
    /// </summary>
    [TestClass]
    public class SongPartTests
    {
        /// <summary>
        /// Tests that following behaviors of the <see cref="SongPart.FromInterface(ISongPart)"/>
        /// method:
        /// 
        /// A: Null is returned if null is passed in
        /// B: If a <see cref="SongPart"/> instance is passed in the exact
        ///    instance is returned
        /// C: If an object that is not a <see cref="SongPart"/> is passed in
        ///    a new <see cref="SongPart"/> instance is created with the same
        ///    properties
        /// </summary>
        [TestMethod]
        public void FromInterfaceTests()
        {
            // Assert A
            Assert.IsNull(SongPart.FromInterface(null));

            // B
            // Arrange
            SongPart model = new SongPart()
            {
                Name = "Song Part",
                Slides = "1 2"
            };

            // Assert
            Assert.AreSame(model, SongPart.FromInterface(model));

            // C
            // Arrange
            ISongPart mockModel = new MockSongPart()
            {
                Name = "Mock Song Part",
                Slides = "1 2"
            };

            // Act
            SongPart result = SongPart.FromInterface(mockModel);

            // Assert
            Assert.AreNotSame(mockModel, result);
            Assert.AreEqual(mockModel.Name, result.Name);
            Assert.AreEqual(mockModel.Slides, result.Slides);
        }
    }
}
