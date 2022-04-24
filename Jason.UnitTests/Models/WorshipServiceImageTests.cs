using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Jason.Models.UnitTests
{
    /// <summary>
    /// Tests the behaviors of the <see cref="WorshipServiceImage"/> class
    /// </summary>
    [TestClass]
    public class WorshipServiceImageTests
    {
        /// <summary>
        /// Tests that the constructor throws an exception if
        /// a null name is passed in
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullNameTest()
        {
            new WorshipServiceImage(null, null);
        }

        /// <summary>
        /// Tests that the constructor throws an exception if
        /// an empty name is passed in
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorEmptyNameTest()
        {
            new WorshipServiceImage(string.Empty, null);
        }

        /// <summary>
        /// Tests that the constructor throws an exception if
        /// the stream provided cannot be read
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AccessViolationException))]
        public void ConstructorNotReadableStream()
        {
            new WorshipServiceImage("test", GetReadLockedStream());
        }

        /// <summary>
        /// Tests the following behaviors of the constructor:
        /// 
        /// A: The <see cref="WorshipServiceImage.Name"/> property
        ///    is set correctly
        /// B: When null data is provided no exception is thrown
        /// C: When an empty stream is provided no exception is thrown
        /// D: When a non-empty stream is provided no exception is thrown
        /// </summary>
        [TestMethod]
        public void ConstructorTests()
        {
            // A and B
            // Arrange
            string name = "test";

            // Act
            WorshipServiceImage model = new WorshipServiceImage(name, null);

            // Assert
            Assert.AreEqual(name, model.Name);


            // C
            // Act
            _ = new WorshipServiceImage(name, new MemoryStream());


            // D
            // Act
            _ = new WorshipServiceImage(name, new MemoryStream(GetTestData()));
        }

        /// <summary>
        /// Tests that the <see cref="WorshipServiceImage.SetData(Stream)"/> method
        /// throws an exception when a Stream which cannot be read is provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AccessViolationException))]
        public void SetDataCantReadStreamTest()
        {
            // Arrange
            WorshipServiceImage model = new WorshipServiceImage("test", new MemoryStream());

            // Act
            model.SetData(GetReadLockedStream());
        }

        /// <summary>
        /// Tests the following behaviors of the <see cref="WorshipServiceImage.SetData(Stream)"/>
        /// and <see cref="WorshipServiceImage.AsMemoryStream"/> methods:
        /// 
        /// A: When <see cref="WorshipServiceImage.SetData(Stream)"/> is provided null
        ///    <see cref="WorshipServiceImage.AsMemoryStream"/> returns en empty stream
        /// B: When <see cref="WorshipServiceImage.SetData(Stream)"/> is provided a stream of data
        ///    <see cref="WorshipServiceImage.AsMemoryStream"/> returns a stream containing the same data
        /// C: When <see cref="WorshipServiceImage.SetData(Stream)"/> is provided an empty
        ///    stream <see cref="WorshipServiceImage.AsMemoryStream"/> returns en empty stream
        /// </summary>
        [TestMethod]
        public void DataTests()
        {
            // Arrange
            WorshipServiceImage model = new WorshipServiceImage("test", new MemoryStream(GetTestData()));
            byte[] expected = GetTestData();
            byte[] result;

            // A
            // Act
            model.SetData(null);
            result = model.AsMemoryStream().ToArray();

            // Assert
            Assert.AreEqual(0, result.Length);


            // B
            // Act
            model.SetData(new MemoryStream(expected));
            result = model.AsMemoryStream().ToArray();

            // Assert
            CollectionAssert.AreEqual(expected.ToList(), result.ToList());


            // C
            model.SetData(new MemoryStream());
            result = model.AsMemoryStream().ToArray();

            // Assert
            Assert.AreEqual(0, result.Length);
        }

        private byte[] GetTestData()
            => new byte[] { 1, 2, 3, 4 };

        private Stream GetReadLockedStream()
        {
            Stream s = new MemoryStream(new byte[] { 1 });
            s.Close();
            return s;
        }
    }
}