using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jason.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviours of the <see cref="Scripture"/> class
    /// </summary>
    [TestClass]
    public class ScriptureTests
    {
        /// <summary>
        /// Tests that the <see cref="Scripture.Type"/> property returns the
        /// expected value
        /// </summary>
        [TestMethod]
        public void TypeTests()
        {
            Scripture model = new Scripture();
            Assert.AreEqual(WorshipServicePartTypes.Scripture, model.Type);
        }

        /// <summary>
        /// Tests that the <see cref="IScripture.Translation"/> property
        /// behaves as follows:
        /// 
        /// A: The property returns null by default
        /// B: When the <see cref="Scripture.Translation"/> proeprty is set
        ///    <see cref="IScripture.Translation"/> returns the new value
        /// C: When the proeprty is set to an <see cref="ITranslation"/> that
        ///    is actualy a <see cref="Translation"/> the exact object is
        ///    returned by both this property and <see cref="Scripture.Translation"/>
        /// D: When the property is set to an <see cref="ITranslation"/> that
        ///    is not a <see cref="Translation"/> a new <see cref="Translation"/>
        ///    with the same values is returned by both this property and
        ///    <see cref="Scripture.Translation"/>
        /// </summary>
        [TestMethod]
        public void TranslationTests()
        {
            // A
            // Arrange
            Scripture model = new Scripture();

            // Assert
            Assert.IsNull(model.Translation);


            // B
            // Arrange
            Translation testTranslation = CreateTestTranslation();

            // Act
            model.Translation = testTranslation;

            // Assert
            Assert.AreSame(testTranslation, (model as IScripture).Translation);


            // C
            // Arrange
            testTranslation = CreateTestTranslation();

            // Act
            (model as IScripture).Translation = testTranslation;

            // Assert
            Assert.AreSame(testTranslation, (model as IScripture).Translation);
            Assert.AreSame(testTranslation, model.Translation);


            // D
            // Arrange
            ITranslation mock = new MockTranslation()
            {
                FullName = "Test Translation 2",
                Abbreviation = "TT2"
            };

            // Act
            (model as IScripture).Translation = mock;

            // Assert
            ITranslation actual = (model as IScripture).Translation;
            Assert.AreNotSame(mock, actual);
            Assert.AreEqual(mock.FullName, actual.FullName);
            Assert.AreEqual(mock.Abbreviation, actual.Abbreviation);

            Assert.AreNotSame(mock, model.Translation);
            Assert.AreEqual(mock.FullName, model.Translation.FullName);
            Assert.AreEqual(mock.Abbreviation, model.Translation.Abbreviation);
        }

        /// <summary>
        /// Tests the following behavior of the
        /// <see cref="Scripture.FromInterface(IScripture)"/> method:
        /// 
        /// A: Null is returned if null is passed in
        /// B: If a <see cref="Scripture"/> instance is passed in the exact
        ///    instance is returned
        /// C: If an object that is not a <see cref="Scripture"/> is passed in
        ///    a new <see cref="Scripture"/> instance is created with the same
        ///    properties
        /// </summary>
        [TestMethod]
        public void FromInterfaceTests()
        {
            // A
            // Assert
            Assert.IsNull(Scripture.FromInterface(null));


            // B
            // Arrange
            Scripture model = new Scripture()
            {
                Translation = CreateTestTranslation()
            };

            // Assert
            Assert.AreSame(model, Scripture.FromInterface(model));


            // C
            // Arrange
            IScripture mockModel = new MockScripture()
            {
                Translation = CreateTestTranslation()
            };

            // Act
            Scripture result = Scripture.FromInterface(mockModel);

            // Assert
            Assert.AreNotSame(mockModel, result);
            Assert.AreSame(mockModel.Translation, result.Translation);
        }

        /// <summary>
        /// Creates a new <see cref="Translation"/> to use for testing
        /// </summary>
        private Translation CreateTestTranslation()
            => new Translation()
                {
                    FullName = "Test Translation",
                    Abbreviation = "TT"
                };
    }
}
