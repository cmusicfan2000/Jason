using CommunityToolkit.Mvvm.ComponentModel;
using Jason.Interfaces.WorshipService;
using Jason.Models;
using Jason.Models.Repositories;
using Jason.Services;
using Jason.ViewModels.WorshipServices;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Jason.ViewModels
{
    public class MainViewModel : ObservableObject
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
        private ICommand newServiceCommand;
        /// <summary>
        /// Gets a command to create a new empty worship service
        /// </summary>
        public ICommand NewServiceCommand
        {
            get
            {
                if (newServiceCommand == null)
                {
                    XamlUICommand cmd = new XamlUICommand()
                    {
                        Description = "Create a new worship service from scratch",
                        Label = "Create New",
                        IconSource = new SymbolIconSource()
                        {
                            Symbol = Symbol.Add
                        }
                    };
                    cmd.ExecuteRequested += OnNewServiceCommandExecuteRequested;
                    newServiceCommand = cmd;
                }

                return newServiceCommand;
            }
        }

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
        private async void OnNewServiceCommandExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            var repo = new JWSRepository(new PickerFilesService());

            try
            {
                IWorshipService newService = repo.Create();
                bool saveCompleted = await repo.SaveAsync(newService);

                if (saveCompleted)
                    WorshipService = new WorshipServiceViewModel(newService);
            }
            catch
            {
                // TODO: What to do here? I don't have a notification manager :(
                // - Make one
                throw;
            }
        }

        private async void OnOpenServiceCommandExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            var repo = new JWSRepository(new PickerFilesService());
            IWorshipService ws = await repo.LoadAsync();

            if (ws != null)
            {
                try
                {
                    WorshipService = new WorshipServiceViewModel(ws);
                }
                catch
                {
                    // TODO: What to do here? I don't have a notification manager :(
                    // - Make one
                    throw;
                }
            }
        }
        #endregion
    }
}
