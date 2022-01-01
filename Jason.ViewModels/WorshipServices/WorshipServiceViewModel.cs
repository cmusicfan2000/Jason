using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml.Input;
using Syncfusion.Presentation;
using Jason.Models;

namespace Jason.ViewModels.WorshipServices
{
    public class WorshipServiceViewModel : ViewModel
    {
        #region Fields
        private readonly WorshipService model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the date on which the order of worship will be used
        /// </summary>
        public DateTime Date
        {
            get => model.Order.Date;
            set
            {
                if (model.Order.Date != value)
                {
                    model.Order.Date = value;
                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the focus of the service
        /// </summary>
        public string Focus
        {
            get => model.Order.Focus;
            set
            {
                if (model.Order.Focus != value)
                {
                    model.Order.Focus = value;
                    InvokePropertyChanged();
                }
            }
        }

        private readonly ObservableCollection<WorshipServicePartViewModel> parts;
        private ReadOnlyObservableCollection<WorshipServicePartViewModel> roParts;
        /// <summary>
        /// Gets a collection of parts of the song to be included in the service
        /// </summary>
        public ReadOnlyObservableCollection<WorshipServicePartViewModel> Parts
        {
            get
            {
                if (roParts == null)
                    roParts = new ReadOnlyObservableCollection<WorshipServicePartViewModel>(parts);

                return roParts;
            }
        }
        #endregion

        #region Commands
        private ICommand generatePowerpointCommand;
        /// <summary>
        /// Gets a command to generate a powerpoint presentation from the order of worship
        /// </summary>
        public ICommand GeneratePowerpointCommand
        {
            get
            {
                if (generatePowerpointCommand == null)
                {
                    StandardUICommand cmd = new StandardUICommand(StandardUICommandKind.None);
                    cmd.ExecuteRequested += OnGeneratePowerpointCommandExecuteRequested;
                    generatePowerpointCommand = cmd;
                }

                return generatePowerpointCommand;
            }
        }
        #endregion

        #region Constructor
        public WorshipServiceViewModel(WorshipService model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            parts = new ObservableCollection<WorshipServicePartViewModel>();

            foreach (object item in model.Order.Items)
            {
                if (item is LordsSupper lsItem)
                    parts.Add(new LordsSupperViewModel(lsItem));
                else if (item is Scripture sItem)
                    parts.Add(new ScriptureViewModel(sItem));
                else if (item is Sermon srItem)
                    parts.Add(new SermonViewModel(srItem));
                else if (item is Song songItem)
                    parts.Add(new SongViewModel(songItem));
                else if (item is FamilyNewsAndPrayer)
                    parts.Add(new FamilyNewsAndPrayerViewModel());
                else if (item is Placeholder ph)
                    parts.Add(new PlaceholderViewModel(ph, model));
            }
        }
        #endregion

        #region Methods
        private async void ExportToPowerpointAsync(StorageFile target)
        {
            if (target == null)
                return;

            // Create a new presentation
            IPresentation presentation = Presentation.Create();

            // Add each part
            foreach (WorshipServicePartViewModel part in Parts)
                await part.AddToPresentation(presentation);

            // Save and close the presentation
            using (Stream s = (await target.OpenAsync(FileAccessMode.ReadWrite)).AsStream())
            {
                presentation.Save(s);
            }
            presentation.Close();

            // Open the presentation in powerpoint
            await Launcher.LaunchFileAsync(target);
        }
        #endregion

        #region Event Handlers
        private async void OnGeneratePowerpointCommandExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            var picker = new FileSavePicker()
            {
                CommitButtonText = "Generate",
                SuggestedFileName = Date.ToShortDateString().Replace('/','-'),
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeChoices.Add("Powerpoint Presentation", new string[] { ".pptx" });

            ExportToPowerpointAsync(await picker.PickSaveFileAsync());
        }
        #endregion
    }
}
