using Windows.Storage;

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
        public string PaperlessHymnalDirectory
        {
            get => ApplicationData.Current.LocalSettings.Values[nameof(PaperlessHymnalDirectory)] as string;
            set => ApplicationData.Current.LocalSettings.Values[nameof(PaperlessHymnalDirectory)] = value;
        }
    }
}