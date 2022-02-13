using Syncfusion.Presentation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Jason.ViewModels.WorshipServices
{
    public class SongViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly Song model;
        private readonly IPresentation songPresentation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title of the song
        /// </summary>
        public string Title
        {
            get => model.Title;
            set
            {
                if (model.Title != value)
                {
                    model.Title = value;
                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the song book reference number
        /// </summary>
        public ushort? BookNumber
        {
            get => model.BookNumberSpecified ? (ushort?)model.BookNumber
                                             : null;
            set
            {
                if (model.BookNumber != value)
                {
                    model.BookNumberSpecified = value != null;
                    if (value != null)
                        model.BookNumber = (ushort)value;

                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// A display-friendly name for this instance
        /// </summary>
        public override string DisplayName => BookNumber == null ? $"Song: {Title}"
                                                                 : $"Song: {Title} (#{BookNumber})";

        /// <summary>
        /// Gets the name of the slideshow containing slides for the song
        /// </summary>
        public string Slideshow
        {
            get => model.Slideshow;
            private set
            {
                if (model.Slideshow != value)
                {
                    model.Slideshow = value;
                    InvokePropertyChanged();
                }
            }
        }

        private readonly ObservableCollection<SongPartViewModel> parts;
        private ReadOnlyObservableCollection<SongPartViewModel> roParts;
        /// <summary>
        /// Gets a collection of parts of the song to be included in the service
        /// </summary>
        public ReadOnlyObservableCollection<SongPartViewModel> Parts
        {
            get
            {
                if (roParts == null)
                    roParts = new ReadOnlyObservableCollection<SongPartViewModel>(parts);

                return roParts;
            }
        }
        #endregion

        #region Constructor
        public SongViewModel(Song model)
            : this(model, null) { }

        public SongViewModel(Song model, IPresentation songPresentation)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            this.songPresentation = songPresentation;
            parts = new ObservableCollection<SongPartViewModel>(model.Part.Select(p => new SongPartViewModel(p)));
        }
        #endregion

        #region Methods
        protected override async Task AddToSection(ISection section)
        {
            if (songPresentation == null)
                throw new InvalidOperationException("Unable to add the song to the section. No presentation is associated with the song.");

            foreach (SongPartViewModel part in Parts)
            {
                foreach (ISlide slide in part.GetSlides(songPresentation))
                    section.Slides.Add(slide);
            }
        }
        #endregion
    }
}
