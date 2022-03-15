using AsyncAwaitBestPractices.MVVM;
using CommunityToolkit.Mvvm.ComponentModel;
using Jason.Models.Extensions;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Jason.ViewModels.Powerpoint
{
    public class PresentationViewModel : ObservableObject
    {
        #region Fields
        private readonly IPresentation presentation;
        private readonly string name;
        #endregion

        #region Properties
        private bool areSlidesLoading;
        /// <summary>
        /// Gets if the slides are currently being loaded
        /// </summary>
        public bool AreSlidesLoading
        {
            get => areSlidesLoading;
            private set => SetProperty(ref areSlidesLoading, value);
        }

        private IEnumerable<SlideViewModel> slides;
        /// <summary>
        /// Gets the slides contained in this presentation
        /// </summary>
        public IEnumerable<SlideViewModel> Slides
        {
            get
            {
                if (slides == null && !AreSlidesLoading)
                    RefreshSlidesCommand.Execute(null);

                return slides;
            }
            private set => SetProperty(ref slides, value);
        }
        #endregion

        #region Commands
        private IAsyncCommand refreshSlidesCommand;
        /// <summary>
        /// Gets a command to refresh the slides for the song
        /// </summary>
        public IAsyncCommand RefreshSlidesCommand
        {
            get
            {
                if (refreshSlidesCommand == null)
                    refreshSlidesCommand = new AsyncCommand(RefreshSlidesAsync,
                                                            _ => !AreSlidesLoading);

                return refreshSlidesCommand;
            }
        }
        #endregion

        #region Constructor
        public PresentationViewModel(IPresentation presentation, string name)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            this.presentation = presentation;
            this.name = name;
        }
        #endregion

        #region Methods
        private async Task RefreshSlidesAsync()
        {
            AreSlidesLoading = true;

            try
            {
                BitmapImage[] slideImages = await presentation.ToImagesAsync(name).ConfigureAwait(true);
                Slides = slideImages.Select((si, i) => new SlideViewModel(presentation.Slides[i], si, i));
            }
            catch
            {
                Slides = Enumerable.Empty<SlideViewModel>();
            }
            finally
            {
                AreSlidesLoading = false;
            }
        }
        #endregion
    }
}