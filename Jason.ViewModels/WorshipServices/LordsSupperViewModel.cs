using Jason.ViewModels.Extensions;
using Syncfusion.Presentation;
using System;

namespace Jason.ViewModels.WorshipServices
{
    public class LordsSupperViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private const int scriptureReferenceBarHeight = 95;
        private const int scriptureMarginLeft = 50;
        private const int scriptureMarginTop = 30;
        private const int maxCharactersPerSlide = 350;

        private readonly LordsSupper model;
        #endregion

        #region Properties
        private ScriptureViewModel scripture;
        /// <summary>
        /// Gets the scripture reference for the Lord's Supper
        /// </summary>
        public ScriptureViewModel Scripture
        {
            get
            {
                if (scripture == null &&
                    model.Scripture != null)
                    scripture = new ScriptureViewModel(model.Scripture);

                return scripture;
            }
        }

        public override string DisplayName => $"Lord's Supper: {Scripture.Book} {Scripture.Reference} ({Scripture.Translation})";

        /// <summary>
        /// Gets the text to show on the previous slide when this part is coming up next
        /// </summary>
        public override string CommingNextText => "The Lord's Supper";
        #endregion

        #region Constructor
        public LordsSupperViewModel(LordsSupper model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion

        #region Methods
        protected override void AddToSection(ISection section, IColor theme, string commingNext)
        {
            // Add Lord's Supper slide
            section.AddSlide(SlideLayoutType.Blank)
                   .SetBackgroundFromResource("LordsSupperTitle.JPG");

            // Add scripture slides
            if (!string.IsNullOrEmpty(scripture?.FullText))
            {
                ISlide currentSlide = AddScriptureSlide(section);
                IShape currentShape = currentSlide.Shapes[0] as IShape;

                string[] words = scripture.FullText.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];

                    // Add spaces to text when needed
                    string textToAppend = string.IsNullOrEmpty(currentShape.TextBody.Text) ? word
                                                                                           : $" {word}";

                    // Move to the next slide if the text will not fit
                    if (currentShape.TextBody.Text.Length + textToAppend.Length > maxCharactersPerSlide)
                    {
                        currentShape = AddScriptureSlide(section).Shapes[0] as IShape;
                        textToAppend = word;
                    }

                    // Add the text
                    if (i < words.Length - 1)
                        currentShape.TextBody.Text += textToAppend;
                    else
                    {
                        // Underline the last word as it is added
                        ITextPart lastWord = currentShape.TextBody.Paragraphs[0].AddTextPart();
                        lastWord.Text = textToAppend;
                        lastWord.UnderlineColor = ColorObject.White;
                        lastWord.Font.Underline = TextUnderlineType.Single;
                    }
                }
            }

            // Add empty communion slideslide
            section.AddSlide(SlideLayoutType.Blank)
                   .SetBackgroundFromResource("LordsSupperThoughts.JPG");
        }

        private ISlide AddScriptureSlide(ISection section)
        {
            // Create a slide with the proper background
            ISlide slide = section.AddSlide(SlideLayoutType.Blank);
            slide.SetBackgroundFromResource("LordsSupperScripture.JPG");

            // Add the textbox for the passage text
            IShape passage = slide.Shapes.AddTextBox(50,
                                                     30,
                                                     slide.SlideSize.Width - (scriptureMarginLeft * 2),
                                                     slide.SlideSize.Height - scriptureMarginTop - scriptureReferenceBarHeight);
            passage.TextBody.Text = string.Empty;
            passage.TextBody.Paragraphs[0].Font.FontName = "Ebrima";
            passage.TextBody.Paragraphs[0].Font.FontSize = 40;
            passage.TextBody.Paragraphs[0].Font.Color = ColorObject.White;

            // Add the scripture reference
            IShape reference = slide.Shapes.AddTextBox(0,
                                                       slide.SlideSize.Height - scriptureReferenceBarHeight,
                                                       slide.SlideSize.Width,
                                                       scriptureReferenceBarHeight);
            reference.TextBody.Text = Scripture.DisplayName;
            reference.TextBody.Paragraphs[0].Font.FontName = "Sitka Banner";
            reference.TextBody.Paragraphs[0].Font.FontSize = 60;
            reference.TextBody.Paragraphs[0].Font.Color = ColorObject.White;
            reference.TextBody.Paragraphs[0].Font.Bold = true;
            reference.TextBody.Paragraphs[0].HorizontalAlignment = HorizontalAlignmentType.Center;

            return slide;
        }
        #endregion
    }
}