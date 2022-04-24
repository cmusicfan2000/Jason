using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jason.Models.UnitTests
{
    [TestClass]
    public class FamilyNewsAndPrayerTests
    {
        /// <summary>
        /// Tests that the <see cref="FamilyNewsAndPrayer.Type"/> property
        /// returns the expected value
        /// </summary>
        [TestMethod]
        public void TypeTests()
        {
            // Arrange
            FamilyNewsAndPrayer model = new FamilyNewsAndPrayer();

            // Assert
            Assert.AreEqual(WorshipServicePartTypes.FamilyNewsAndPrayer, model.Type);
        }
    }
}
