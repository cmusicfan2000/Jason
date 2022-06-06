using Jason.Interfaces.Services;
using System.Threading.Tasks;
using Windows.Storage;

namespace Jason.UnitTests.Mocks
{
    public class MockFilesService : IStorageService
    {
        public IStorageFile FileToReturn { get; set; }

        public Task<IStorageFile> GetSingleFileAsync(string extension)
            => Task.Run(() => FileToReturn );
    }
}
