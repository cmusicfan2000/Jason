using Jason.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Jason.Services
{
    public class PickerFilesService : IStorageService
    {
        /// <summary>
        /// Gets a single file using a <see cref="FileOpenPicker"/>
        /// </summary>
        /// <param name="extension">The desired extension of the file</param>
        /// <returns>
        /// An <see cref="IStorageFile"/> if a file is selected, otherwise null
        /// </returns>
        public async Task<IStorageFile> GetSingleFileAsync(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

            // Obtain a file with the .jws extension
            var picker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(extension);

            return await picker.PickSingleFileAsync();
        }

        /// <summary>
        /// Attempts to obtain a <see cref="IStorageFile"/> instance
        /// for the item at the provided path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<IStorageFile> GetExistingFileAsync(string path)
        {
            return await StorageFile.GetFileFromPathAsync(path);
        }

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
        public async Task<IStorageFile> GetSaveFileAsync(string defaultExtension, string suggestedName, IDictionary<string, IList<string>> suggestedExtensions)
        {
            if (string.IsNullOrEmpty(defaultExtension))
                throw new ArgumentNullException(nameof(defaultExtension));

            var picker = new FileSavePicker()
            {
                CommitButtonText = "Save",
                DefaultFileExtension = defaultExtension,
                SuggestedFileName = suggestedName ?? string.Empty,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            if (suggestedExtensions != null)
            {
                foreach (var kvp in suggestedExtensions)
                    picker.FileTypeChoices.Add(kvp.Key, kvp.Value);
            }

            return await picker.PickSaveFileAsync();
        }
    }
}