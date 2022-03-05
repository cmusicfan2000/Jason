using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Jason.Models.Extensions
{
    public static class IPresentationExtensions
    {
        #region Fields
        private static StorageFolder localFolder = null;
        #endregion

        #region Methods
        /// <summary>
        /// Converts a presentation to images
        /// </summary>
        /// <param name="presentation"></param>
        /// <returns></returns>
        public static async byte[] ToImagesAsync(this IPresentation presentation, string presentationName)
        {
            if (localFolder == null)
                localFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);

            
        }


        /// <summary>
        /// Convert a slide as image
        /// </summary>
        /// <param name="slide">The slide to be converted</param>
        /// <returns></returns>
        private static async Task<String> ConvertSlide(IPresentation presentation, ISlide slide, StorageFolder location)
        {
            

            string mainImageFileName;
            string thumbImageFileName;
            if (presentation.Slides[0].SlideNumber > 0)
            {
                mainImageFileName = "Slide " + slide.SlideNumber.ToString() + ".jpg";
                thumbImageFileName = "ThumbSlide " + slide.SlideNumber.ToString() + ".jpg";
            }
            else
            {
                mainImageFileName = "Slide " + (slide.SlideNumber + 1) + ".jpg";
                thumbImageFileName = "ThumbSlide " + (slide.SlideNumber + 1) + ".jpg";
            }

            StorageFile mainImageStorageFile = await _localFolder.CreateFileAsync(mainImageFileName, CreationCollisionOption.ReplaceExisting);
            StorageFile thumbImageStorageFile = await _localFolder.CreateFileAsync(thumbImageFileName, CreationCollisionOption.ReplaceExisting);
            RenderingOptions options = new RenderingOptions() { ScaleX = 0.25f, ScaleY = 0.25f };
            await slide.SaveAsImageAsync(mainImageStorageFile, _cancellationToken.Token);
            await slide.SaveAsImageAsync(thumbImageStorageFile, options, _cancellationToken.Token);
            if (LoadingStatusCanvas != null && slide.SlideNumber == _slideCount)
                LoadingStatusCanvas.Visibility = Visibility.Collapsed;
            _convertedSlides = slide.SlideNumber;
            return mainImageStorageFile.Path;
        }
        #endregion
    }
}