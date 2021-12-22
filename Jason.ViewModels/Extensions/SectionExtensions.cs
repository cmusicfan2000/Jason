using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Linq;

namespace Jason.ViewModels.Extensions
{
    /// <summary>
    /// Extends behaviors of the <see cref="Section"/> class
    /// </summary>
    public static class SectionExtensions
    {
        /// <summary>
        /// Gets the parent <see cref="DocumentFormat.OpenXml.Presentation.Presentation"/> of the <see cref="Section"/>
        /// </summary>
        /// <param name="section">The section for which to locate the parent</param>
        /// <returns>
        /// The parent presentation of the section, or null if there is no parent or the section
        /// is null
        /// </returns>
        public static Presentation Presentation(this Section section)
            => section?.Ancestors<Presentation>().FirstOrDefault();

        /// <summary>
        /// Adds a slide to a section and it's parent presentation
        /// </summary>
        /// <param name="section">The section to which to add the slide</param>
        /// <param name="slide">The slide to add</param>
        /// <returns>
        /// The <see cref="SlideId"/> of the slide within the section's presentation
        /// </returns>
        public static SlideId AddSlide(this Section section, Slide slide)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            Presentation presentation = section.Presentation();

            if (presentation == null)
                throw new ArgumentException("The section must be contained in a presentation", nameof(section));
            if (presentation.PresentationPart == null)
                throw new ArgumentException("The section's presentation must be stored in a presentation part", nameof(section));

            // Add the slide to the parent presentation
            SlideId slideID = presentation.AddSlide(slide);

            if (slideID == null)
                throw new Exception("Unable to add the slide to the presentation. The slideID returned was null");

            // Get / Create the list of slides in the section
            SectionSlideIdList slideList = section.GetOrCreateChild<SectionSlideIdList>();

            // Add the slide to the section's slide list
            slideList.Append(new SectionSlideIdListEntry()
            {
                Id = slideID.Id
            });

            // Save the modified presentation.
            presentation.Save();

            return slideID;
        }
    }
}