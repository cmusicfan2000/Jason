using System;

namespace Jason.ViewModels.WorshipServices
{
    public class ScriptureViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly Scripture model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the scripture reference information
        /// </summary>
        /// <remarks>
        /// Could this be changed to something more specific?
        /// </remarks>
        public string Reference
        {
            get => model.Reference;
            set
            {
                if (model.Reference != value)
                {
                    model.Reference = value;
                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the book from which the scripture comes
        /// </summary>
        public ScriptureBook Book
        {
            get => model.Book;
            set
            {
                if (model.Book != value)
                {
                    model.Book = value;
                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the translation being referenced
        /// </summary>
        public ScriptureTranslation Translation
        {
            get => model.Translation;
            set
            {
                if (model.Translation != value)
                {
                    model.Translation = value;
                    InvokePropertyChanged();
                }
            }
        }

        public override string DisplayName => $"Scripture: {Book} {Reference} ({Translation})";
        #endregion

        #region Constructor
        public ScriptureViewModel(Scripture model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion
    }
}