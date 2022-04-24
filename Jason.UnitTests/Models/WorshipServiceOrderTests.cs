using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Jason.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviors of the <see cref="WorshipServiceOrder"/> class
    /// </summary>
    [TestClass]
    public class WorshipServiceOrderTests
    {
        /// <summary>
        /// Tests the following behaviors of the <see cref="WorshipServiceOrder.Parts"/>
        /// property:
        /// 
        /// A: The property returns null by default
        /// B: When set the <see cref="WorshipServiceOrder.Items"/>
        ///    property is updated with the same instances
        /// C: When the <see cref="WorshipServiceOrder.Items"/> proeprty
        ///    is set the property returns only instances that are
        ///    <see cref="IWorshipServicePart"/>s
        /// </summary>
        [TestMethod]
        public void PartsTests()
        {
            // Arrange
            WorshipServiceOrder model = new WorshipServiceOrder();
            IWorshipServicePart[] expected = new IWorshipServicePart[]
            {
                new MockWorshipServicePart()
                {
                    Type = WorshipServicePartTypes.FamilyNewsAndPrayer
                },
                new MockWorshipServicePart()
                {
                    Type = WorshipServicePartTypes.LordsSupper
                },
                new MockWorshipServicePart()
                {
                    Type = WorshipServicePartTypes.Placeholder
                },
                new MockWorshipServicePart()
                {
                    Type = WorshipServicePartTypes.Scripture
                },
                new MockWorshipServicePart()
                {
                    Type = WorshipServicePartTypes.Sermon
                },
                new MockWorshipServicePart()
                {
                    Type = WorshipServicePartTypes.Song
                },
                new FamilyNewsAndPrayer(),
                new LordsSupper(),
                new Placeholder(),
                new Scripture(),
                new Sermon(),
                new Song()
            };

            // A
            // Assert
            Assert.IsNull(model.Parts);


            // B
            // Act
            model.Parts = expected;

            // Assert
            Assert.AreEqual(expected.Length, model.Items.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreSame(expected[i], model.Items[i]);


            // C
            // Act
            model.Items = expected.Concat(new object[]
            {
                "not a part",
                new object()
            }).ToArray();

            // Assert
            Assert.AreEqual(expected.Length, model.Parts.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreSame(expected[i], model.Parts[i]);
        }
    }
}