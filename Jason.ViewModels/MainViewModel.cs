using Jason.Models.Repositories;
using Jason.ViewModels.WorshipServices;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Input;

namespace Jason.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Fields
        private readonly RecentFilesRepository recentServicesRepo = new RecentFilesRepository("Services");
        #endregion

        #region Properties
        private ObservableCollection<RecentServiceViewModel> recentServices = new ObservableCollection<RecentServiceViewModel>();
        private ReadOnlyObservableCollection<RecentServiceViewModel> roRecentServies;
        /// <summary>
        /// Gets a collection of paths to the most recently opened services
        /// </summary>
        public ReadOnlyObservableCollection<RecentServiceViewModel> RecentServices
        {
            get
            {
                if (roRecentServies == null)
                {
                    LoadRecentServicesAsync();
                    roRecentServies = new ReadOnlyObservableCollection<RecentServiceViewModel>(recentServices);
                }

                return roRecentServies;
            }
        }

        private WorshipServiceViewModel worshipService;
        /// <summary>
        /// Gets the currently open worship service
        /// </summary>
        public WorshipServiceViewModel WorshipService
        {
            get => worshipService;
            private set => SetProperty(ref worshipService, value);
        }
        #endregion

        #region Commands
        private ICommand openServiceCommand;
        /// <summary>
        /// Gets a command to open an existing worship service
        /// </summary>
        public ICommand OpenServiceCommand
        {
            get
            {
                if (openServiceCommand == null)
                {
                    StandardUICommand cmd = new StandardUICommand(StandardUICommandKind.Open);
                    cmd.ExecuteRequested += OnOpenServiceCommandExecuteRequested;
                    openServiceCommand = cmd;
                }

                return openServiceCommand;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the list of recent servies opened
        /// </summary>
        private async void LoadRecentServicesAsync()
        {
            var recentFiles = await recentServicesRepo.GetRecentFilesAsync();

            foreach (StorageFile sf in recentFiles)
                recentServices.Add(new RecentServiceViewModel(sf));
        }
        #endregion

        #region Event Handlers
        private async void OnOpenServiceCommandExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            var picker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".ws");
            picker.FileTypeFilter.Add(".xml");

            var file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                var repo = new WorshipServiceRepository();

                try
                {
                    WorshipService ws = await repo.GetWorshipServiceAsync(file);

                    if (ws != null)
                        WorshipService = new WorshipServiceViewModel(ws);
                }
                catch (Exception ex)
                {
                    // What to do here? I don't have a notification manager :(
                    // - Make one
                    throw;
                }
            }
        }
        #endregion
    }
}
