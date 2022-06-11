using CommunityToolkit.Mvvm.ComponentModel;
using Jason.Enumerations;
using Jason.Interfaces.Services;
using Jason.Interfaces.WorshipService;
using Jason.Models;
using Jason.Services;
using Microsoft.Toolkit.Uwp.Helpers;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Input;

namespace Jason.ViewModels.WorshipServices
{
    public class WorshipServiceViewModel : ObservableObject
    {
        #region Fields
        private readonly IWorshipService model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the service
        /// </summary>
        public string Name => model.Name;

        private Color themeColor;
        /// <summary>
        /// Gets or sets
        /// </summary>
        public Color ThemeColor
        {
            get
            {
                if (themeColor == default(Color))
                    themeColor = model.Order.ThemeColor.ToColor();

                return themeColor;
            }
            //set
            //{
            //    if (themeColor != value)
            //    {
            //        // TODO: Make sure this spits out the right format
            //        model.Order.ThemeColor = value.ToHex();

            //        InvokePropertyChanged();
            //    }
            //}
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
        public WorshipServiceViewModel(IWorshipService model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            parts = new ObservableCollection<WorshipServicePartViewModel>();

            if (model.Order?.Parts?.Any() == true)
            {
                foreach (IWorshipServicePart part in model.Order.Parts)
                {
                    switch (part.Type)
                    {
                        case WorshipServicePartTypes.LordsSupper:
                            parts.Add(new LordsSupperViewModel(part as ILordsSupper));
                            break;
                        case WorshipServicePartTypes.Scripture:
                            parts.Add(new ScriptureViewModel(part as IScripture));
                            break;
                        case WorshipServicePartTypes.Sermon:
                            parts.Add(new SermonViewModel(part as ISermon));
                            break;
                        case WorshipServicePartTypes.Song:
                            {
                                ISong song = part as ISong;
                                IPowerpointPresentation pres = string.IsNullOrEmpty(song.Slideshow) ? null
                                                                                                    : model.Presentations.SingleOrDefault(p => p.Name == song.Slideshow);
                                if (pres == null)
                                    parts.Add(new SongViewModel(song));
                                else
                                    parts.Add(new SongViewModel(song, pres.Presentation));

                                break;
                            }
                        case WorshipServicePartTypes.FamilyNewsAndPrayer:
                            parts.Add(new FamilyNewsAndPrayerViewModel());
                            break;
                        case WorshipServicePartTypes.Placeholder:
                            parts.Add(new PlaceholderViewModel(part as IPlaceholder, model));
                            break;
                    }
                }
            }
        }
        #endregion

        #region Methods
        private async void ExportToPowerpointAsync(IStorageFile target)
        {
            if (target == null)
                return;

            // Create a new presentation
            IPresentation presentation = Presentation.Create();

            IColor theme = ColorObject.FromArgb(ThemeColor.R, ThemeColor.G, ThemeColor.B);

            // Add each part
            int lastPartIndex = Parts.Count - 1;
            for (int i = 0; i < Parts.Count; i++)
            {
                await Task.Run(() =>
                {
                    Parts[i].AddToPresentation(presentation,
                                               theme,
                                               i == lastPartIndex ? null
                                                                  : Parts[i + 1].CommingNextText);
                });
            }

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
            IStorageService service = new PickerFilesService();
            IDictionary<string, IList<string>> suggestedExtensions = new Dictionary<string, IList<string>>();
            suggestedExtensions.Add("Powerpoint Presentation (.pptx)", new Collection<string>() { ".pptx" });

            ExportToPowerpointAsync(await service.GetSaveFileAsync(".pptx",
                                                                   model.Name,
                                                                   suggestedExtensions));
        }
        #endregion
    }
}
