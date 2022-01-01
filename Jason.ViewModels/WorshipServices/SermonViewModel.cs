using Syncfusion.Presentation;
using System;

namespace Jason.ViewModels.WorshipServices
{
    public class SermonViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly WorshipServiceSermon model;
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
        #endregion

        #region Constructor
        public SermonViewModel(WorshipServiceSermon model)
            : base(ItemsChoiceType.Sermon)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion

        #region Methods
        //protected override void AddToSection(ISection section)
        //{
        //    // Add a blank slide to it
        //    ISlide slide = section.Slides.Add(SlideLayoutType.Blank);

        //    //Adds a textbox in a slide by specifying its position and size
        //    IShape textShape = slide.AddTextBox(100, 75, 756, 200);

        //    //Adds a paragraph into the textShape
        //    IParagraph paragraph = textShape.TextBody.AddParagraph();

        //    //Set the horizontal alignment of paragraph
        //    paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;

        //    //Adds a textPart in the paragraph
        //    ITextPart textPart = paragraph.AddTextPart(PartName);

        //    //Applies font formatting to the text
        //    textPart.Font.FontSize = 80;
        //}
        #endregion
    }
}