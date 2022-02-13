using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jason.ViewModels.WorshipServices
{
    public class SongPartViewModel : ViewModel
    {
        #region Fields
        private readonly SongPart model;
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
        public SongPartViewModel(SongPart model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion

        #region Methods
        public IEnumerable<ISlide> GetSlides(IPresentation presentation)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            Collection<ISlide> slides = new Collection<ISlide>();

            foreach (string slideNumberString in model.Slides.Split(' '))
            {
                if (int.TryParse(slideNumberString, out int slideNumber))
                {
                    ISlide slideAtIndex = presentation.Slides[slideNumber];

                    if (slideAtIndex != null)
                        slides.Add(slideAtIndex);
                }
            }

            return slides;
        }
        #endregion
    }
}
