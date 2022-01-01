using Jason.Models.Repositories;
using Syncfusion.Drawing;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Jason.ViewModels.WorshipServices
{
    public class PrayerViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly SettingsRepository settingsRepo = new SettingsRepository();
        #endregion

        public PrayerViewModel()
            : base(ItemsChoiceType.Prayer) { }

        protected override async Task AddToSection(ISection section)
        {
            // Add a blank slide to the section
            ISlide slide = section.Slides.Add(SlideLayoutType.Blank);

            IReadOnlyList<StorageFile> files = await settingsRepo.PrayerSlideBackgroundsDirectory?.GetFilesAsync();

            if (files?.Any() == true)
            {
                var imageFiles = files.Where(f => f.Name.EndsWith(".jpg"));

                if (imageFiles.Any())
                {
                    // Randomly select an image from the folder to set as the background
                    var rand = new Random();
                    using (Stream fs = (await imageFiles.ElementAt(rand.Next(imageFiles.Count())).OpenSequentialReadAsync()).AsStreamForRead())
                    {
                        slide.Background.Fill.FillType = FillType.Picture;
                        slide.Background.Fill.PictureFill.ImageBytes = Image.FromStream(fs).ImageData;
                    }
                }
                else
                    AddDummyText(slide);
            }
            else
                AddDummyText(slide);
        }

        private void AddDummyText(ISlide slide)
        {
            //Adds a textbox in a slide by specifying its position and size
            IShape textShape = slide.AddTextBox(100, 75, 756, 200);

            //Adds a paragraph into the textShape
            IParagraph paragraph = textShape.TextBody.AddParagraph();

            //Set the horizontal alignment of paragraph
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;

            //Adds a textPart in the paragraph
            ITextPart textPart = paragraph.AddTextPart("Prayer");

            //Applies font formatting to the text
            textPart.Font.FontSize = 80;
        }
    }
}
