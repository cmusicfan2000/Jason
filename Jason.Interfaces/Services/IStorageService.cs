using System.Threading.Tasks;
using Windows.Storage;

namespace Jason.Interfaces.Services
{
    /// <summary>
    /// A service used to obtain <see cref="IStorageFile"/>s
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Gets a single existing <see cref="IStorageFile"/> by extension
        /// </summary>
        /// <param name="extension">
        /// The file extension of the file to locate
        /// </param>
        /// <returns>
        /// A task which returns an <see cref="IStorageFile"/>
        /// when complete
        /// </returns>
        Task<IStorageFile> GetSingleFileAsync(string extension);

        /// <summary>
        /// Gets an <see cref="IStorageFile"/> to which to save
        /// </summary>
        /// <param name="extension">
        /// The file extension to use by default
        /// </param>
        /// <returns>
        /// A task which returns an <see cref="IStorageFile"/>
        /// when complete
        /// </returns>
        Task<IStorageFile> GetSaveFileAsync(string extension);

        /// <summary>
        /// Attempts to obtain a <see cref="IStorageFile"/> instance
        /// for the item at the provided path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IStorageFile> GetExistingFileAsync(string path);
    }
}