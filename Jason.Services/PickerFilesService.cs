using Jason.Interfaces.Services;
using System;
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
    }
}