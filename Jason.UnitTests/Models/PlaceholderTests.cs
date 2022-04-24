using Jason.Models;
using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jaso.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviours of the <see cref="Placeholder"/> class
    /// </summary>
    [TestClass]
    public class PlaceholderTests
    {
        /// <summary>
        /// Tests that the <see cref="Placeholder.Type"/> property returns the
        /// expected value
        /// </summary>
        [TestMethod]
        public void TypeTests()
        {
            Placeholder model = new Placeholder();
            Assert.AreEqual(WorshipServicePartTypes.Placeholder, model.Type);
        }

        /// <summary>
        /// Tests the following behavior of the
        /// <see cref="Placeholder.FromInterface(IPlaceholder)"/> method:
        /// 
        /// A: Null is returned if null is passed in
        /// B: If a <see cref="Placeholder"/> instance is passed in the exact
        ///    instance is returned
        /// C: If an object that is not a <see cref="Placeholder"/> is passed in
        ///    a new <see cref="Placeholder"/> instance is created with the same
        ///    <see cref="Placeholder.Name"/>
        /// </summary>
        [TestMethod]
        public void FromInterfaceTests()
        {
            // A
            // Assert
            Assert.IsNull(Placeholder.FromInterface(null));


            // B
            // Arrange
            Placeholder model = new Placeholder()
            {
                Name = "test",
                BackgroundImageName = "testImage"
            };

            // Assert
            Assert.AreSame(model, Placeholder.FromInterface(model));


            // C
            // Arrange
            IPlaceholder mockModel = new MockPlaceholder()
            {
                Name = "test",
                BackgroundImageName = "testImage"
            };

            // Act
            Placeholder result = Placeholder.FromInterface(mockModel);

            // Assert
            Assert.AreNotSame(mockModel, result);
            Assert.AreEqual(mockModel.Name, result.Name);
            Assert.AreEqual(mockModel.BackgroundImageName, result.BackgroundImageName);
        }
    }
}
