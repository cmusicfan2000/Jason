using Jason.Interfaces.Services;
using Jason.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Jason.Models.Repositories.UnitTests
{
    /// <summary>
    /// Tests the behaviors of the <see cref="JWSRepository"/> class
    /// </summary>
    [TestClass]
    public class JWSRepositoryTests
    {
        /// <summary>
        /// Tests that the constructor throws an exception if a null
        /// <see cref="IStorageService"/> is passed in
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullFileServiceTest()
        {
            new JWSRepository(null);
        }

        /// <summary>
        /// Tests the following behaviors of the <see cref="JWSRepository.CreateAsync"/> method:
        /// 
        /// A: An empty <see cref="IWorshipService"/> is returned
        /// </summary>
        [TestMethod]
        public void CreateAsyncTests()
        {
            // Arrange
            MockFilesService service = new MockFilesService();
            JWSRepository repo = new JWSRepository(service);

            // Act
            Task<IWorshipService> result = repo.CreateAsync();
            result.Wait();

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Order);
            Assert.IsNull(result.Result.Order.ThemeColor);
            Assert.IsNotNull(result.Result.Order.Parts);
            Assert.IsFalse(result.Result.Order.Parts.Any());
            Assert.IsNotNull(result.Result.Images);
            Assert.IsFalse(result.Result.Images.Any());
            Assert.IsNotNull(result.Result.Presentations);
            Assert.IsFalse(result.Result.Presentations.Any());
        }

        /// <summary>
        /// Tests that if the <see cref="IStorageService"/> returns a file that
        /// cannot be opened for reading an exception is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoadAsyncFileCannotBeReadTest()
        {
            // Arrange
            Task<StorageFile> getBadFile = StorageFile.GetFileFromPathAsync("ICannotBeOpened")
                                                      .AsTask();
            getBadFile.Wait();
            MockFilesService service = new MockFilesService()
            {
                FileToReturn = getBadFile.Result
            };
            JWSRepository repo = new JWSRepository(service);

            // Act
            Task<IWorshipService> result = repo.LoadAsync();
            result.Wait();
        }

        /// <summary>
        /// Tests that if the <see cref="IStorageService"/> provided returns a
        /// file that is not a zip archive an exception is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoadAsyncFileNotZipArchiveTest()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests the following behaviors of the <see cref="JWSRepository.CreateAsync"/>
        /// method:
        /// 
        /// A: If the <see cref="IStorageService"/> provided to the constructor is unable
        ///    to obtain a .jws file null is returned
        /// B: If a .JWS file does not contain an order.xml file a valid <see cref="IWorshipService"/>
        ///    is still returned
        /// C: If a .JWS file does not contain any entries in a folder named Images a valid
        ///    <see cref="IWorshipService"/> is still returned with no images
        /// D: If a .JWS file does not contain any entries in a folder named Slideshows a valid
        ///    <see cref="IWorshipService"/> is still returned with no presentations
        /// E: If a .JWS file is returned by the <see cref="IStorageService"/> then an
        ///    <see cref="IWorshipService"/> is returned containing the correct contents
        /// </summary>
        [TestMethod]
        public void LoadAsyncTests()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests that if <see cref="JWSRepository.SaveAsync(IWorshipService)"/>
        /// if provided null an exception is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveAsyncNullServiceTests()
        {

        }

        /// <summary>
        /// Tests that if the <see cref="IStorageService"/> returns a file that
        /// cannot be opened for writing an exception is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SaveAsyncFileCannotBeWrittenTest()
        {

        }

        /// <summary>
        /// Tests the following behaviors of the <see cref="JWSRepository.SaveAsync(IWorshipService)"/>
        /// method:
        /// 
        /// A: The <see cref="IWorshipService"/> provided is saved using the <see cref="IStorageService"/>
        /// </summary>
        [TestMethod]
        public void SaveAsyncTests()
        {

        }
    }
}
