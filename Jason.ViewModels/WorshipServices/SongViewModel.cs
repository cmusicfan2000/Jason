using Syncfusion.Presentation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jason.ViewModels.WorshipServices
{
    public class SongViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly WorshipServiceSong model;
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
        #endregion

        #region Constructor
        public SongViewModel(WorshipServiceSong model)
            : base(ItemsChoiceType.Song)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            parts = new ObservableCollection<SongPartViewModel>(model.Part.Select(p => new SongPartViewModel(p)));
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
