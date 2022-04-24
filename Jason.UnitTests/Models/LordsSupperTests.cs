using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jason.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviours of the <see cref="LordsSupper"/> class
    /// </summary>
    [TestClass]
    public class LordsSupperTests
    {
        /// <summary>
        /// Tests that the <see cref="LordsSupper.Type"/> property returns the
        /// expected value
        /// </summary>
        [TestMethod]
        public void TypeTests()
        {
            LordsSupper model = new LordsSupper();
            Assert.AreEqual(WorshipServicePartTypes.LordsSupper, model.Type);
        }

        /// <summary>
        /// Tests that the <see cref="ILordsSupper.Scripture"/> property
        /// behaves as follows:
        /// 
        /// A: The property returns null by default
        /// B: When the <see cref="LordsSupper.Scripture"/> proeprty is set
        ///    <see cref="ILordsSupper.Scripture"/> returns the new value
        /// C: When the proeprty is set to an <see cref="IScripture"/> that
        ///    is actualy a <see cref="Scripture"/> the exact object is
        ///    returned by both this property and <see cref="LordsSupper.Scripture"/>
        /// D: When the property is set to an <see cref="IScripture"/> that
        ///    is not a <see cref="Scripture"/> a new <see cref="Scripture"/>
        ///    with the same values is returned by both this property and
        ///    <see cref="LordsSupper.Scripture"/>
        /// </summary>
        [TestMethod]
        public void ScriptureTests()
        {
            // A
            // Arrange
            LordsSupper model = new LordsSupper();

            // Assert
            Assert.IsNull(model.Scripture);


            // B
            // Arrange
            Scripture testScripture = CreateTestScripture();

            // Act
            model.Scripture = testScripture;

            // Assert
            Assert.AreSame(testScripture, (model as ILordsSupper).Scripture);


            // C
            // Arrange
            testScripture = CreateTestScripture();

            // Act
            (model as ILordsSupper).Scripture = testScripture;

            // Assert
            Assert.AreEqual(testScripture, (model as ILordsSupper).Scripture);
            Assert.AreEqual(testScripture, model.Scripture);


            // D
            // Arrange
            IScripture mock = new MockScripture()
            {
                Book = ScriptureBook.Acts,
                Reference = "5:5",
                BackgroundImageName = "testImage",
                Translation = new MockTranslation()
                {
                    FullName = "Test Translation",
                    Abbreviation = "TT"
                },
                Text = "Blah blah blah"
            };

            // Act
            (model as ILordsSupper).Scripture = mock;

            // Assert
            IScripture actual = (model as ILordsSupper).Scripture;
            Assert.AreNotSame(mock, actual);
            Assert.AreEqual(mock.Book, actual.Book);
            Assert.AreEqual(mock.Reference, actual.Reference);
            Assert.AreEqual(mock.BackgroundImageName, actual.BackgroundImageName);
            Assert.AreEqual(mock.Translation.FullName, actual.Translation.FullName);
            Assert.AreEqual(mock.Translation.Abbreviation, actual.Translation.Abbreviation);
            Assert.AreEqual(mock.Text, actual.Text);

            Assert.AreNotSame(mock, model.Scripture);
            Assert.AreEqual(mock.Book, model.Scripture.Book);
            Assert.AreEqual(mock.Reference, model.Scripture.Reference);
            Assert.AreEqual(mock.BackgroundImageName, model.Scripture.BackgroundImageName);
            Assert.AreEqual(mock.Translation.FullName, model.Scripture.Translation.FullName);
            Assert.AreEqual(mock.Translation.Abbreviation, model.Scripture.Translation.Abbreviation);
            Assert.AreEqual(mock.Text, model.Scripture.Text);
        }

        /// <summary>
        /// Tests the following behavior of the
        /// <see cref="LordsSupper.FromInterface(ILordsSupper)"/> method:
        /// 
        /// A: Null is returned if null is passed in
        /// B: If a <see cref="LordsSupper"/> instance is passed in the exact
        ///    instance is returned
        /// C: If an object that is not a <see cref="LordsSupper"/> is passed in
        ///    a new <see cref="LordsSupper"/> instance is created with the same
        ///    <see cref="LordsSupper.Scripture"/>
        /// </summary>
        [TestMethod]
        public void FromInterfaceTests()
        {
            // A
            // Assert
            Assert.IsNull(LordsSupper.FromInterface(null));


            // B
            // Arrange
            LordsSupper model = new LordsSupper()
            {
                Scripture = CreateTestScripture()
            };

            // Assert
            Assert.AreSame(model, LordsSupper.FromInterface(model));


            // C
            // Arrange
            ILordsSupper mockModel = new MockLordsSupper()
            {
                Scripture = CreateTestScripture()
            };

            // Act
            LordsSupper result = LordsSupper.FromInterface(mockModel);

            // Assert
            Assert.AreNotSame(mockModel, result);
            Assert.AreEqual(mockModel.Scripture, result.Scripture);
        }

        /// <summary>
        /// Creates a new <see cref="Scripture"/> to use for testing
        /// </summary>
        private Scripture CreateTestScripture()
            => new Scripture()
            {
                Book = ScriptureBook.Genesis,
                Reference = "1:1",
                BackgroundImageName = "testImage",
                Translation = new MockTranslation()
                {
                    FullName = "Test Translation",
                    Abbreviation = "TT"
                },
                Text = "Blah blah blah"
            };
    }
}
