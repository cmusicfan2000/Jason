using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace Jason.Models.Repositories
{
    /// <summary>
    /// A repository in which settings are stored
    /// </summary>
    public sealed class SettingsRepository
    {
        /// <summary>
        /// Gets the path to the paperless hymnal directory
        /// </summary>
        public StorageFolder PaperlessHymnalDirectory
        {
            get => Task.Run(() => GetFolderForTokenAsync(ApplicationData.Current.LocalSettings.Values[nameof(PaperlessHymnalDirectory)] as string)).Result;
            set
            {
                ForgetFolder(PaperlessHymnalDirectory);
                RememberFolder(value);
                ApplicationData.Current.LocalSettings.Values[nameof(PaperlessHymnalDirectory)] = RememberFolder(value);
            }
        }

        /// <summary>
        /// Gets the path to the directory in which to find backgrounds for prayer slides
        /// </summary>
        public StorageFolder PrayerSlideBackgroundsDirectory
        {
            get => Task.Run(() => GetFolderForTokenAsync(ApplicationData.Current.LocalSettings.Values[nameof(PrayerSlideBackgroundsDirectory)] as string)).Result;
            set
            {
                ForgetFolder(PrayerSlideBackgroundsDirectory);
                ApplicationData.Current.LocalSettings.Values[nameof(PrayerSlideBackgroundsDirectory)] = RememberFolder(value);
            }
        }

        private string RememberFolder(StorageFolder folder)
        {
            string token = Guid.NewGuid().ToString();
            StorageApplicationPermissions.FutureAccessList.AddOrReplace(token, folder);
            return token;
        }

        private void ForgetFolder(StorageFolder folder)
        {
            string guid = ApplicationData.Current.LocalSettings.Values[nameof(PaperlessHymnalDirectory)] as string;

            if (StorageApplicationPermissions.FutureAccessList.ContainsItem(guid))
                StorageApplicationPermissions.FutureAccessList.Remove(guid);
        }

        private async Task<StorageFolder> GetFolderForTokenAsync(string token)
        {
            if (!StorageApplicationPermissions.FutureAccessList.ContainsItem(token)) return null;
            return await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
        }
    }
}