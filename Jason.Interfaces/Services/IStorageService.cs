using System.Collections.Generic;
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
        /// <param name="defaultExtension">
        /// The file extension to use by default
        /// </param>
        /// <param name="suggestedName">
        /// The suggested name of the file
        /// </param>
        /// <param name="suggestedExtensions">
        /// An <see cref="IDictionary{TKey, TValue}"/> that contains a collection of valid file
        /// types (extensions) that the user can use to save a file. Each element in this
        /// collection maps a display name to a corresponding collection of file name extensions.
        /// The key is a single string, the value is a list/vector of strings representing
        /// one or more extension choices.
        /// </param>
        /// <returns>
        /// A task which returns an <see cref="IStorageFile"/>
        /// when complete
        /// </returns>
        Task<IStorageFile> GetSaveFileAsync(string defaultExtension, string suggestedName, IDictionary<string, IList<string>> suggestedExtensions);

        /// <summary>
        /// Attempts to obtain a <see cref="IStorageFile"/> instance
        /// for the item at the provided path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IStorageFile> GetExistingFileAsync(string path);
    }
}