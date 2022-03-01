using Syncfusion.Drawing;
using Syncfusion.Presentation;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Jason.ViewModels.Extensions
{
    /// <summary>
    /// Extensions to the <see cref="ISlide"/> interface
    /// </summary>
    public static class ISlideExtensions
    {
        /// <summary>
        /// Sets the backgrond of a slide to an image saved as a resource in Jason.ViewModels/Resources
        /// </summary>
        /// <param name="slide">The slide whose background to set</param>
        /// <param name="resourceName">The name of an image resource located in in Jason.ViewModels/Resources</param>
        public static void SetBackgroundFromResource(this ISlide slide, string resourceName)
        {
            using (Stream imageStream = typeof(ISlideExtensions).Assembly
                                                                .GetManifestResourceStream($"Jason.ViewModels.Resources.{resourceName}"))
            using (MemoryStream ms = new MemoryStream())
            {
                imageStream.CopyTo(ms);

                slide.Background.Fill.FillType = FillType.Picture;
                slide.Background.Fill.PictureFill.ImageBytes = ms.ToArray();
            }
        }
    }
}