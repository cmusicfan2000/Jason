using Syncfusion.Presentation;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Jason.Models.Extensions
{
    public static class IPresentationExtensions
    {
        /// <summary>
        /// Converts a presentation to images
        /// </summary>
        /// <param name="presentation"></param>
        /// <returns></returns>
        public static async Task<BitmapImage[]> ToImagesAsync(this IPresentation presentation, string presentationName)
        {
            // Return an empty array if there are no slides to convert
            if (presentation?.Slides?.Count == null ||
                presentation.Slides.Count < 1)
                return new BitmapImage[0];
            if (string.IsNullOrEmpty(presentationName))
                throw new ArgumentNullException(nameof(presentationName));

            // Get or Create the images folder if it doesn't already exist
            StorageFolder localFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);

            // Get or Create a folder for the presentation
            StorageFolder presentationFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(presentationName, CreationCollisionOption.OpenIfExists);

            // Create images for each slide
            BitmapImage[] images = new BitmapImage[presentation.Slides.Count];
            for (int i = 0; i < presentation.Slides.Count; i++)
            {
                images[i] = await ConvertSlideAsync(presentation.Slides[i], i, presentationFolder);
            }

            return images;
        }

        /// <summary>
        /// Convert a slide as image
        /// </summary>
        /// <param name="slide">The slide to be converted</param>
        /// <returns></returns>
        private static async Task<BitmapImage> ConvertSlideAsync(ISlide slide, int slideIndex, StorageFolder location)
        {
            string mainImageFileName = $"Slide {slideIndex + 1}.jpg";

            // Create files for each of the images
            StorageFile mainImageStorageFile = await location.CreateFileAsync(mainImageFileName, CreationCollisionOption.ReplaceExisting);

            // Save the images
            await slide.SaveAsImageAsync(mainImageStorageFile);

            return new BitmapImage(new Uri(mainImageStorageFile.Path));
        }
    }
}