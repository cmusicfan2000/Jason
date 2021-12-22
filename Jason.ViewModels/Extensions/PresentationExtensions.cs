using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Linq;

namespace Jason.ViewModels.Extensions
{
    /// <summary>
    /// Extends the behaviors of the <see cref="Presentation"/> class
    /// </summary>
    public static class PresentationExtensions
    {
        /// <summary>
        /// Adds a section with the specified name to the presentation
        /// </summary>
        /// <param name="presentation">The presentation to which to add the section</param>
        /// <param name="name">The name of the new section</param>
        public static Section AddSection(this Presentation presentation, string name)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            // Create the section
            if (presentation.PresentationExtensionList == null)
                presentation.PresentationExtensionList = new PresentationExtensionList();

            // Add an extension if one doesn't already exist
            PresentationExtension extension = presentation.PresentationExtensionList.GetFirstChild<PresentationExtension>();
            if (extension == null)
            {
                extension = new PresentationExtension() { Uri = "{DCFCB54F-F5C9-4659-A805-7FBBBFBC68F2}" };
                presentation.PresentationExtensionList.Append(extension);
            }

            // Add a section list of one doesn't already exist
            SectionList sectionList = extension.GetOrCreateChild<SectionList>();

            // Add the new section
            Section section = new Section()
            {
                Name = name
            };
            sectionList.Append(section);

            return section;
        }

        /// <summary>
        /// Adds a slide to a section and it's parent presentation
        /// </summary>
        /// <param name="presentation">The presentation to which to add the slide</param>
        /// <param name="slide">The slide to add</param>
        /// <returns>
        /// The ID of the slide within the presentation
        /// </returns>
        public static SlideId AddSlide(this Presentation presentation, Slide slide)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));
            if (presentation.PresentationPart == null)
                throw new ArgumentException("The section's presentation must be stored in a presentation part", nameof(presentation));

            // Ensure there is a slide ID list
            if (presentation.SlideIdList == null)
                presentation.SlideIdList = new SlideIdList();

            // Create the slide part for the new slide.
            SlidePart slidePart = presentation.PresentationPart.AddNewPart<SlidePart>();

            // Save the new slide part.
            slide.Save(slidePart);

            // Find the highest slide ID in the current list.
            SlideId prevSlideId = presentation.SlideIdList
                                              .ChildElements
                                              .OfType<SlideId>()
                                              .OrderBy(si => si.Id)
                                              .LastOrDefault();

            // Copy the layout from the last slide if present
            if (prevSlideId != null)
            {
                SlidePart lastSlidePart = (SlidePart)presentation.PresentationPart.GetPartById(prevSlideId.RelationshipId);
                if (lastSlidePart.SlideLayoutPart != null)
                    slidePart.AddPart(lastSlidePart.SlideLayoutPart);
            }

            // Insert the new slide into the slide list after the previous slide.
            SlideId newID = new SlideId()
            {
                Id = (prevSlideId?.Id ?? 0) + 1,
                RelationshipId = presentation.PresentationPart.GetIdOfPart(slidePart)
            };

            if (prevSlideId == null)
                presentation.SlideIdList.Append(newID);
            else
                presentation.SlideIdList.InsertAfter(newID, prevSlideId);

            // Save the modified presentation.
            presentation.Save();

            return newID;
        }
    }
}
