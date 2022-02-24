using Jason.ViewModels.Extensions;
using Syncfusion.Presentation;

namespace Jason.ViewModels.WorshipServices
{
    public class FamilyNewsAndPrayerViewModel : WorshipServicePartViewModel
    {
        public override string DisplayName => "Family News and Prayer";

        protected override void AddToSection(ISection section, IColor theme, string commingNext)
        {
            // Add a blank slide to the section
            ISlide slide = section.Slides.Add(SlideLayoutType.Blank);
            slide.SetBackgroundFromResource("FamilyNewsandPrayer.jpg");
        }
    }
}