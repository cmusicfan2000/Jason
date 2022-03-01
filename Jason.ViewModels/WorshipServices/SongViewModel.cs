using Jason.ViewModels.Extensions;
using Syncfusion.OfficeChartToImageConverter;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Jason.ViewModels.WorshipServices
{
    public class SongViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly Song model;
        private readonly IPresentation songPresentation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title of the song
        /// </summary>
        public string Title
        {
            get => model.Title;
            set
            {
                if (model.Title != value)
                {
                    model.Title = value;
                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the song book reference number
        /// </summary>
        public ushort? BookNumber
        {
            get => model.BookNumberSpecified ? (ushort?)model.BookNumber
                                             : null;
            set
            {
                if (model.BookNumber != value)
                {
                    model.BookNumberSpecified = value != null;
                    if (value != null)
                        model.BookNumber = (ushort)value;

                    InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// A display-friendly name for this instance
        /// </summary>
        public override string DisplayName => BookNumber == null ? $"{Title}"
                                                                 : $"{Title} (#{BookNumber})";

        /// <summary>
        /// Gets the text to show on the previous slide when this song is coming up next
        /// </summary>
        public override string CommingNextText => BookNumber == null ? $"\"{Title}\""
                                                                     : $"\"{Title}\" (#{BookNumber})";

        /// <summary>
        /// Gets the name of the slideshow containing slides for the song
        /// </summary>
        public string Slideshow
        {
            get => model.Slideshow;
            private set
            {
                if (model.Slideshow != value)
                {
                    model.Slideshow = value;
                    InvokePropertyChanged();
                }
            }
        }

        public IEnumerable<byte[]> slides;
        /// <summary>
        /// Gets the slides as images
        /// </summary>
        public IEnumerable<byte[]> Slides
        {
            get
            {
                //if (slides == null)
                //{
                //    var task = getSlidesAsBytes();
                //    task.Wait();
                //    slides = task.Result;
                //}

                return slides;
            }
        }

        //private async Task<IEnumerable<byte[]>> getSlidesAsBytes()
        //{
        //    //Initialize the ‘ChartToImageConverter’ instance to convert the charts in the slides
        //    //songPresentation.ChartToImageConverter = new ChartToImageConverter();
        //    Collection<byte[]> slideImages = new Collection<byte[]>();
        //    foreach (ISlide slide in songPresentation.Slides)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {

        //        }

        //        slide.SaveAsImageAsync()


        //        //Convert the slide to an image.
        //        await slide.SaveAsImageAsync(tempFile);

        //        using (Stream stream = await tempFile.OpenStreamForReadAsync())
        //        {
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                stream.CopyTo(memoryStream);
        //                slideImages.Add(memoryStream.ToArray());
        //            }
        //        }

        //        //using (var imageStream = await tempFile.OpenStreamForWriteAsync())
        //        //{
        //        //    await slide.SaveAsImageAsync(imageStream);
        //        //}


        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            try
        //            {
        //                slide.SaveAsImageAsync(ms).Wait();
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }


        //            byte[] stuff = ms.ToArray();
        //            slideImages.Add(stuff);
        //        }
        //    }

        //    return slideImages;
        //}

        private readonly ObservableCollection<SongPartViewModel> parts;
        private ReadOnlyObservableCollection<SongPartViewModel> roParts;
        /// <summary>
        /// Gets a collection of parts of the song to be included in the service
        /// </summary>
        public ReadOnlyObservableCollection<SongPartViewModel> Parts
        {
            get
            {
                if (roParts == null)
                    roParts = new ReadOnlyObservableCollection<SongPartViewModel>(parts);

                return roParts;
            }
        }
        #endregion

        #region Constructor
        public SongViewModel(Song model)
            : this(model, null) { }

        public SongViewModel(Song model, IPresentation songPresentation)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            this.songPresentation = songPresentation;
            parts = new ObservableCollection<SongPartViewModel>(model.Part.Select(p => new SongPartViewModel(p)));
        }
        #endregion

        #region Methods
        protected override void AddToSection(ISection section, IColor theme, string commingNext)
        {
            if (songPresentation == null)
                throw new InvalidOperationException("Unable to add the song to the section. No presentation is associated with the song.");

            foreach (SongPartViewModel part in Parts)
            {
                foreach (ISlide slide in part.GetSlides(songPresentation))
                {
                    // Locate the current header bar and remove it if present
                    IShape header = slide.Shapes
                                         .Cast<IShape>()
                                         .FirstOrDefault(s => s.Top < 15 &&
                                                              s.Left < 15 &&
                                                              s.TextBody?.Text.Any() == true);
                    if (header != null)
                        slide.Shapes.Remove(header);

                    // Add the header bar to the slide
                    AddThemedTextBox(slide, 0, 0, $"{part.Name} - {DisplayName}", theme);
                    
                    // Add the slide to the presentation
                    section.Slides.Add(slide);
                }
            }

            // Add the ending bar to the last slide
            ISlide lastSlide = section.Slides.LastOrDefault();
            if (lastSlide != null &&
                commingNext != null)
            {
                // Add the textbox
                AddThemedTextBox(lastSlide, 0, lastSlide.SlideSize.Height - 32, $"Next: {commingNext}", theme, marginLeft: 50);

                // Add the BR Logo
                using (Stream imageStream = this.GetType()
                                .Assembly
                                .GetManifestResourceStream("Jason.ViewModels.Resources.BRLogo.png"))
                {
                    lastSlide.Shapes.AddPicture(imageStream, 20, lastSlide.SlideSize.Height - 32, 16, 32);
                }
            }
        }

        /// <summary>
        /// Adds a textbox to a slide that matches the theme for the presentation
        /// </summary>
        /// <param name="slide">The slide to which to add</param>
        /// <param name="left">Represents the left position, in points. The Left value ranges from -169056 to 169056.</param>
        /// <param name="top">Represents the top position, in points. The Top value ranges from -169056 to 169056.</param>
        /// <param name="text">The text to place in the box</param>
        /// <param name="theme">The theme to apply</param>
        /// <param name="marginLeft">
        /// Optional: specifies the distance between the left edge of the text body and the left
        /// edge of the rectangle shape that contains the text, in points. Value ranges from 1 to 1583.
        /// </param>
        private void AddThemedTextBox(ISlide slide, double left, double top, string text, IColor theme, double marginLeft = 10)
        {
            IShape tb = slide.Shapes.AddTextBox(left, top, slide.SlideSize.Width, 32);
            tb.TextBody.Text = text;
            tb.TextBody.MarginLeft = marginLeft;

            tb.TextBody.Paragraphs[0].Font.Color = theme.BackgroundToForeground();
            tb.TextBody.Paragraphs[0].Font.FontName = "Ebrima";
            tb.TextBody.Paragraphs[0].Font.FontSize = 26;
            tb.TextBody.MarginTop = 0;
            tb.TextBody.MarginBottom = 0;
            tb.Fill.FillType = FillType.Solid;
            tb.Fill.SolidFill.Color = theme;
        }
        #endregion
    }
}