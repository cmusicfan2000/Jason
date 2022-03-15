using Syncfusion.Presentation;
using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Jason.ViewModels.Powerpoint
{
    public class SlideViewModel
    {
        #region Properties
        /// <summary>
        /// Gets the index of the slide within it's presentation
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the slide as an image
        /// </summary>
        public BitmapImage Image { get; }

        /// <summary>
        /// Gets the <see cref="ISlide"/> represented
        /// </summary>
        public ISlide Slide { get; }
        #endregion

        #region Constructor
        public SlideViewModel(ISlide slide, BitmapImage image, int index)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            Slide = slide;
            Image = image;
            Index = index;
        }
        #endregion
    }
}