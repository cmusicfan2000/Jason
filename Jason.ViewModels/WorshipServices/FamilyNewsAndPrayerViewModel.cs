using Syncfusion.Drawing;
using Syncfusion.Presentation;
using System.IO;
using System.Threading.Tasks;

namespace Jason.ViewModels.WorshipServices
{
    public class FamilyNewsAndPrayerViewModel : WorshipServicePartViewModel
    {
        public override string DisplayName => "Family News and Prayer";

        protected override async Task AddToSection(ISection section)
        {
            // Add a blank slide to the section
            ISlide slide = section.Slides.Add(SlideLayoutType.Blank);

            using (Stream imageStream = this.GetType()
                                            .Assembly
                                            .GetManifestResourceStream("Jason.ViewModels.Resources.FamilyNewsandPrayer.jpg"))
            {
                slide.Background.Fill.FillType = FillType.Picture;
                slide.Background.Fill.PictureFill.ImageBytes = Image.FromStream(imageStream).ImageData;
            }
        }
    }
}