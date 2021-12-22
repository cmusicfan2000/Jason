using Jason.Models;
using Jason.Models.Repositories;
using System;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Input;

namespace Jason.ViewModels
{
    public class PaperlessHymnalSettingsViewModel : ViewModel
    {
        #region Fields
        private readonly SettingsRepository settingsRepo = new SettingsRepository();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the path to the directory in which the slides are located
        /// </summary>
        public string PaperlessHymnalDirectory
        {
            get => settingsRepo.PaperlessHymnalDirectory;
            private set
            {
                if (settingsRepo.PaperlessHymnalDirectory != value)
                {
                    settingsRepo.PaperlessHymnalDirectory = value;
                    InvokePropertyChanged();
                }
            }
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
                PaperlessHymnalDirectory = folder.Path;
        }
        #endregion
    }
}