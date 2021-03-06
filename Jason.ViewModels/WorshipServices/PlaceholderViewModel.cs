using Jason.Models;
using Syncfusion.Drawing;
using Syncfusion.Presentation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jason.ViewModels.WorshipServices
{
    /// <summary>
    /// Represents a placeholder for various single entry items in a worship service (Ex. Welcome)
    /// </summary>
    public class PlaceholderViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly Placeholder model;
        private readonly WorshipService serviceModel;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the placeholder
        /// </summary>
        public string Name => model.Name;

        public override string DisplayName => model.Name;
        #endregion

        #region Constructor
        public PlaceholderViewModel(Placeholder model, WorshipService serviceModel)
        {
            this.model = model;
            this.serviceModel = serviceModel;
        }
        #endregion

        #region Methods
        protected override async Task AddToSection(ISection section)
        {
            // Add a blank slide to the section
            ISlide slide = section.Slides.Add(SlideLayoutType.Blank);

            if (!string.IsNullOrEmpty(model.BackgroundImageName))
            {
                string imageName = model.BackgroundImageName;
                if (!imageName.EndsWith(".jpg"))
                    imageName += ".jpg";

                WorshipServiceImage wsi = serviceModel.Images.SingleOrDefault(i => i.Name == imageName);

                if (wsi != null)
                {
                    using (Stream imageStream = wsi.AsStream())
                    {
                        slide.Background.Fill.FillType = FillType.Picture;
                        slide.Background.Fill.PictureFill.ImageBytes = Image.FromStream(imageStream).ImageData;
                    }
                }
            }
        }
        #endregion
    }
}