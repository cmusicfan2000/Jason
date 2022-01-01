using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jason.ViewModels.WorshipServices
{
    public class SongViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly Song model;
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


        public override string DisplayName => BookNumber == null ? $"Song: {Title}"
                                                                 : $"Song: {Title} (#{BookNumber})";
        #endregion

        #region Constructor
        public SongViewModel(Song model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            parts = new ObservableCollection<SongPartViewModel>(model.Part.Select(p => new SongPartViewModel(p)));
        }
        #endregion
    }
}
