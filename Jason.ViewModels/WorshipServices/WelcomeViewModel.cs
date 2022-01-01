using Syncfusion.Drawing;
using Syncfusion.Presentation;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Jason.ViewModels.WorshipServices
{
    public class WelcomeViewModel : WorshipServicePartViewModel
    {
        public WelcomeViewModel()
            : base(ItemsChoiceType.Welcome) { }

        //protected override async Task AddToSection(ISection section)
        //{
        //    // Locate the powerpoint file containing the welcome slide
        //    var picker = new FileOpenPicker()
        //    {
        //        CommitButtonText = "Select",
        //        SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        //    };
        //    picker.FileTypeFilter.Add(".pptx");

        //    StorageFile file = await picker.PickSingleFileAsync();

        //    if (file != null)
        //    {

        //    }

        //    // Add a blank slide to the section
        //    ISlide slide = section.Slides.Add(SlideLayoutType.Blank);

        //    using (Stream imageStream = this.GetType()
        //                                    .Assembly
        //                                    .GetManifestResourceStream("Jason.ViewModels.Resources.FamilyNewsandPrayer.jpg"))
        //    {
        //        slide.Background.Fill.FillType = FillType.Picture;
        //        slide.Background.Fill.PictureFill.ImageBytes = Image.FromStream(imageStream).ImageData;
        //    }
        //}
    }
}
