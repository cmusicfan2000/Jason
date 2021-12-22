using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace Jason.Models.Repositories
{
    public class RecentFilesRepository
    {
        #region Properties
        /// <summary>
        /// Gets the name of the list of recent files
        /// </summary>
        /// <remarks>
        /// The value of this property is used as the key into application settings
        /// </remarks>
        public string Name { get; }
        #endregion

        #region Constructor
        public RecentFilesRepository(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<StorageFile>> GetRecentFilesAsync()
        {
            var mru = StorageApplicationPermissions.MostRecentlyUsedList;

            // Look at all recent entries that belong to this list
            Collection <StorageFile> results = new Collection<StorageFile>();
            foreach (var entry in mru.Entries
                                     .Where(e => e.Metadata == Name))
            {
                StorageFile sf = await mru.GetFileAsync(entry.Token);
                results.Add(sf);
            }

            return results;
        }

        public void Add(StorageFile recentFile)
        {
            StorageApplicationPermissions.MostRecentlyUsedList
                                         .Add(recentFile, Name);
        }
        #endregion
    }
}
