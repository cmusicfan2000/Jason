using System;

namespace Jason.ViewModels.WorshipServices
{
    public class SermonViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly Sermon model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title of the sermon
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
        /// Gets or sets the name of the presenter
        /// </summary>
        public string Presenter
        {
            get => model.Presenter;
            set
            {
                if (model.Presenter != value)
                {
                    model.Presenter = value;
                    InvokePropertyChanged();
                }
            }
        }


        public override string DisplayName => "Sermon";
        #endregion

        #region Constructor
        public SermonViewModel(Sermon model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion
    }
}