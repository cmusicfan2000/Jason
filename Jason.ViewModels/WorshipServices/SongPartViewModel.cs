using System;

namespace Jason.ViewModels.WorshipServices
{
    public class SongPartViewModel : ViewModel
    {
        #region Fields
        private readonly WorshipServiceSongPart model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the part (Verse 1, Chorus, etc)
        /// </summary>
        public string Name
        {
            get => model.Name;
            set
            {
                if (model.Name != value)
                {
                    model.Name = value;
                    InvokePropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor
        public SongPartViewModel(WorshipServiceSongPart model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion
    }
}
