using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Presentation;
using System;

namespace Jason.ViewModels.WorshipServices
{
    public abstract class WorshipServicePartViewModel : ObservableObject
    {
        #region Properties
        /// <summary>
        /// Gets a display-friendly name for the part
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets the text to display on the previous slide if this part is coming next
        /// </summary>
        public virtual string CommingNextText => DisplayName;
        #endregion

        #region Methods
        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        protected virtual void AddToSection(ISection section, IColor theme, string commingNext) { }

        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        public void AddToPresentation(IPresentation presentation, IColor theme, string commingNext)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            // Create a new section
            ISection partSection = presentation.Sections.Add();
            partSection.Name = DisplayName;

            // Add the appropraite slides to it
            try
            {
                AddToSection(partSection, theme, commingNext);
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
                ITextPart textPart = paragraph.AddTextPart(partSection.Name);

                //Applies font formatting to the text
                textPart.Font.FontSize = 80;
            }
        }
        #endregion
    }
}