using Syncfusion.Presentation;
using System;
using System.Threading.Tasks;

namespace Jason.ViewModels.WorshipServices
{
    public abstract class WorshipServicePartViewModel : ViewModel
    {
        /// <summary>
        /// Gets the name of the part
        /// </summary>
        public string PartName { get; }

        #region Constructor
        public WorshipServicePartViewModel(ItemsChoiceType type)
        {
            switch (type)
            {
                case ItemsChoiceType.FamilyNewsAndPrayer:
                    PartName = "Family News and Prayer";
                    break;
                case ItemsChoiceType.LordsSupper:
                    PartName = "Lord's Supper";
                    break;
                default:
                    PartName = type.ToString();
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        protected virtual Task AddToSection(ISection section) => Task.CompletedTask;

        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        public async Task AddToPresentation(IPresentation presentation)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            // Create a new section
            ISection partSection = presentation.Sections.Add();
            partSection.Name = PartName;

            // Add the appropraite slides to it
            try
            {
                await AddToSection(partSection);
            }
            catch
            {
                // Clear any slides from the section
                partSection.Slides.Clear();

                // Add a blank slide to the section
                ISlide slide = partSection.Slides.Add(SlideLayoutType.Blank);

                //Adds a textbox in a slide by specifying its position and size
                IShape textShape = slide.AddTextBox(100, 75, 756, 200);

                //Adds a paragraph into the textShape
                IParagraph paragraph = textShape.TextBody.AddParagraph();

                //Set the horizontal alignment of paragraph
                paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;

                //Adds a textPart in the paragraph
                ITextPart textPart = paragraph.AddTextPart("Family News and Prayer");

                //Applies font formatting to the text
                textPart.Font.FontSize = 80;
            }
        }
        #endregion
    }
}