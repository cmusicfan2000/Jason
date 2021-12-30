using Jason.Models.Repositories;
using System;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Input;

namespace Jason.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        #region Fields
        private readonly SettingsRepository settingsRepo = new SettingsRepository();
        #endregion

        #region Properties
        private string paperlessHymnalDirectory;
        /// <summary>
        /// Gets the path to the directory in which the slides are located
        /// </summary>
        public string PaperlessHymnalDirectory
        {
            get => paperlessHymnalDirectory;
            private set => SetProperty(ref paperlessHymnalDirectory, value);
        }

        private string prayerBackgroundsDirectory;
        /// <summary>
        /// Gets the path to the directory in which the slides are located
        /// </summary>
        public string PrayerBackgroundsDirectory
        {
            get => prayerBackgroundsDirectory;
            private set => SetProperty(ref prayerBackgroundsDirectory, value);
        }
        #endregion

        #region Commands
        private StandardUICommand updateSlidesDirectoryPathCommand;
        /// <summary>
        /// Gets a command to update the path to the slides directory
        /// </summary>
        public ICommand UpdateSlidesDirectoryPathCommand
        {
            get
            {
                if (updateSlidesDirectoryPathCommand == null)
                {
                    updateSlidesDirectoryPathCommand = new StandardUICommand(StandardUICommandKind.Open);
                    updateSlidesDirectoryPathCommand.ExecuteRequested += OnUpdateSlidesDirectoryPathCommandExecuteRequested;
                }

                return updateSlidesDirectoryPathCommand;
            }
        }

        private StandardUICommand updatePrayerBackgroundsDirectoryCommand;
        /// <summary>
        /// Gets a command to update the path to the prayer backgrounds directory
        /// </summary>
        public ICommand UpdatePrayerBackgroundsDirectoryCommand
        {
            get
            {
                if (updatePrayerBackgroundsDirectoryCommand == null)
                {
                    updatePrayerBackgroundsDirectoryCommand = new StandardUICommand(StandardUICommandKind.Open);
                    updatePrayerBackgroundsDirectoryCommand.ExecuteRequested += OnUpdatePrayerBackgroundsDirectoryCommandExecutedRequested;
                }

                return updatePrayerBackgroundsDirectoryCommand;
            }
        }
        #endregion

        #region Constructors
        public SettingsViewModel()
        {
            paperlessHymnalDirectory = settingsRepo.PaperlessHymnalDirectory?.Path;
            prayerBackgroundsDirectory = settingsRepo.PrayerSlideBackgroundsDirectory?.Path;
        }
        #endregion

        #region EventHandlers
        private async void OnUpdateSlidesDirectoryPathCommandExecuteRequested(XamlUICommand command, ExecuteRequestedEventArgs e)
        {
            var picker = new FolderPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            var folder = await picker.PickSingleFolderAsync();

            if (folder != null)
            {
                settingsRepo.PaperlessHymnalDirectory = folder;
                PaperlessHymnalDirectory = folder.Path;
            }
        }

        private async void OnUpdatePrayerBackgroundsDirectoryCommandExecutedRequested(XamlUICommand command, ExecuteRequestedEventArgs e)
        {
            var picker = new FolderPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            var folder = await picker.PickSingleFolderAsync();

            if (folder != null)
            {
                settingsRepo.PrayerSlideBackgroundsDirectory = folder;
                PrayerBackgroundsDirectory = folder.Path;
            }
        }
        #endregion
    }
}