using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml.Input;
using Syncfusion.Presentation;

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
            get => model.Date;
            set
            {
                if (model.Date != value)
                {
                    model.Date = value;
                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the focus of the service
        /// </summary>
        public string Focus
        {
            get => model.Focus;
            set
            {
                if (model.Focus != value)
                {
                    model.Focus = value;
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
            for (int i = 0; i < model.Items.Length; i++)
            {
                switch (model.ItemsElementName[i])
                {
                    case ItemsChoiceType.LordsSupper:
                        parts.Add(new LordsSupperViewModel(model.Items[i] as WorshipServiceLordsSupper));
                        break;
                    case ItemsChoiceType.Scripture:
                        parts.Add(new ScriptureViewModel(model.Items[i] as ScriptureType));
                        break;
                    case ItemsChoiceType.Sermon:
                        parts.Add(new SermonViewModel(model.Items[i] as WorshipServiceSermon));
                        break;
                    case ItemsChoiceType.Song:
                        parts.Add(new SongViewModel(model.Items[i] as WorshipServiceSong));
                        break;
                    case ItemsChoiceType.Prayer:
                        parts.Add(new PrayerViewModel());
                        break;
                    case ItemsChoiceType.FamilyNewsAndPrayer:
                        parts.Add(new FamilyNewsAndPrayerViewModel());
                        break;
                    default:
                        parts.Add(new PlaceholderViewModel(model.ItemsElementName[i]));
                        break;
                }
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
