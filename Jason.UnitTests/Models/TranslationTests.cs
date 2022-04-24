using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jason.Models.UnitTests
{
    [TestClass]
    public class TranslationTests
    {
        /// <summary>
        /// Tests the following behavior of the
        /// <see cref="Translation.FromInterface(ITranslation)"/> method:
        /// 
        /// A: Null is returned if null is passed in
        /// B: If a <see cref="Translation"/> instance is passed in the exact
        ///    instance is returned
        /// C: If an object that is not a <see cref="Translation"/> is passed in
        ///    a new <see cref="Translation"/> instance is created with the same
        ///    <see cref="Translation.Abbreviation"/> and <see cref="Translation.FullName"/>
        /// </summary>
        [TestMethod]
        public void FromInterfaceTests()
        {
            // Assert A
            Assert.IsNull(Translation.FromInterface(null));

            // B
            // Arrange
            Translation model = new Translation()
            {
                Abbreviation = "ABV",
                FullName = "I'm a translation!"
            };

            // Assert
            Assert.AreSame(model, Translation.FromInterface(model));

            // C
            // Arrange
            ITranslation mockModel = new MockTranslation()
            {
                Abbreviation = "DIFF",
                FullName = "I'm not the first guy!"
            };

            // Act
            Translation result = Translation.FromInterface(mockModel);

            // Assert
            Assert.AreNotSame(mockModel, result);
            Assert.AreEqual(mockModel.Abbreviation, result.Abbreviation);
            Assert.AreEqual(mockModel.FullName, result.FullName);
        }
    }
}
