using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jason.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviours of the <see cref="Song"/> class
    /// </summary>
    [TestClass]
    public class SongTests
    {
        /// <summary>
        /// Tests that the <see cref="Song.Type"/> property returns the
        /// expected value
        /// </summary>
        [TestMethod]
        public void TypeTests()
        {
            Song model = new Song();
            Assert.AreEqual(WorshipServicePartTypes.Song, model.Type);
        }

        /// <summary>
        /// Tests the following behaviors of the <see cref="Song.SongBookNumber"/>
        /// property:
        /// 
        /// A: By default the property returns null
        /// B: When set the <see cref="Song.BookNumber"/> and
        ///    <see cref="Song.BookNumberSpecified"/> properties
        ///    are updated
        /// C: When <see cref="Song.BookNumber"/> is set the
        ///    property is updated
        /// D: When <see cref="Song.BookNumberSpecified"/> is set to false
        ///    the property is updated
        /// E: When <see cref="Song.BookNumberSpecified"/> is set to true
        ///    the property is updated
        /// </summary>
        [TestMethod]
        public void SongBookNumberTests()
        {
            // Arrange
            Song model = new Song();
            ushort expected = 5;
            ushort alternate = 8;

            // A
            // Assert
            Assert.IsNull(model.SongBookNumber);


            // B
            // Act
            model.SongBookNumber = expected;

            // Assert
            Assert.AreEqual(expected, model.BookNumber);
            Assert.IsTrue(model.BookNumberSpecified);


            // C
            // Act
            model.BookNumber = alternate;

            // Assert
            Assert.AreEqual(alternate, model.SongBookNumber);


            // D
            // Act
            model.BookNumberSpecified = false;

            // Assert
            Assert.IsNull(model.SongBookNumber);

            // E
            // Act
            model.BookNumberSpecified = true;

            // Assert
            Assert.AreEqual(alternate, model.SongBookNumber);
        }

        /// <summary>
        /// Tests the following behaviors of the <see cref="Song.Parts"/>
        /// property:
        /// 
        /// A: By default the property returns null
        /// B: When set the <see cref="Song.Part"/>
        ///    property is updated as follows:
        ///    - Instances of <see cref="SongPart"/> are returned as the same instance
        ///    - New <see cref="SongPart"/> intances are created for other classes
        /// C: When <see cref="Song.Part"/> is set the
        ///    property is updated
        /// </summary>
        [TestMethod]
        public void PartsTests()
        {
            // Arrange
            Song model = new Song();
            SongPart[] expected = new SongPart[]
            {
                new SongPart()
                {
                    Name = "testPart1"
                },
                new SongPart()
                {
                    Name = "testPart2"
                }
            };
            ISongPart[] expectedMixed = new ISongPart[]
            {
                new SongPart()
                {
                    Name = "testPart3"
                },
                new MockSongPart()
                {
                    Name = "testPart4"
                }
            };

            // A
            // Assert
            Assert.IsNull(model.Parts);


            // B
            // Act
            model.Parts = expectedMixed;

            // Assert
            Assert.AreEqual(expectedMixed.Length, model.Part.Length);
            for (int i = 0; i < expectedMixed.Length; i++)
            {
                if (expectedMixed[i] is SongPart)
                    Assert.AreSame(expectedMixed[i], model.Parts[i]);
                else
                {
                    Assert.AreNotSame(expectedMixed[i], model.Part[i]);
                    Assert.AreEqual(expectedMixed[i].Name, model.Part[i].Name);
                    Assert.AreEqual(expectedMixed[i].Slides, model.Part[i].Slides);
                }
            }


            // C
            // Act
            model.Part = expected;

            // Assert
            Assert.AreEqual(expected.Length, model.Parts.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreSame(expected[i], model.Parts[i]);
        }

        /// <summary>
        /// Tests the following behavior of the
        /// <see cref="Song.FromInterface(ISong)"/> method:
        /// 
        /// A: Null is returned if null is passed in
        /// B: If a <see cref="Song"/> instance is passed in the exact
        ///    instance is returned
        /// C: If an object that is not a <see cref="Song"/> is passed in
        ///    a new <see cref="Song"/> instance is created with the same
        ///    properties
        /// </summary>
        [TestMethod]
        public void FromInterfaceTests()
        {
            // A
            // Assert
            Assert.IsNull(Song.FromInterface(null));


            // B
            // Arrange
            Song model = new Song()
            {
                Title = "Test Song"
            };

            // Assert
            Assert.AreSame(model, Song.FromInterface(model));


            // C
            // Arrange
            ISongPart[] expectedParts = new ISongPart[]
            {
                new SongPart()
                {
                    Name = "testPart3"
                },
                new SongPart()
                {
                    Name = "testPart4"
                }
            };
            ISong mockModel = new MockSong()
            {
                Title = "Mock Song",
                SongBookNumber = 5,
                Parts = expectedParts,
                Slideshow = "MySlides.pptx"
            };

            // Act
            Song result = Song.FromInterface(mockModel);

            // Assert
            Assert.AreNotSame(mockModel, result);
            Assert.AreEqual(mockModel.Title, result.Title);
            Assert.AreEqual(mockModel.SongBookNumber, result.SongBookNumber);
            Assert.AreEqual(mockModel.Slideshow, result.Slideshow);
            Assert.AreEqual(mockModel.Parts.Length, result.Parts.Length);
            for (int i = 0; i < mockModel.Parts.Length; i++)
                Assert.AreSame(mockModel.Parts[i], result.Parts[i]);
        }
    }
}