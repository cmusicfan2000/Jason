using DocumentFormat.OpenXml.Presentation;
using System;
using System.Linq;

namespace Jason.ViewModels.Extensions
{
    /// <summary>
    /// Extends the behavior of the <see cref="Slide"/> class
    /// </summary>
    public static class SlideExtensions
    {
        public static SlideId GetID(this Slide slide, Presentation presentation)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));
            if (slide.SlidePart == null)
                throw new ArgumentException("The slide is not currently stored in a slide part", nameof(slide));
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            // Obtain the ID of the slide
            string slideRelID = presentation.PresentationPart?.GetIdOfPart(slide.SlidePart);

            if (slideRelID == null)
                return null;

            return presentation.SlideIdList
                               ?.OfType<SlideId>()
                               .FirstOrDefault(si => si.RelationshipId == slideRelID);
        }

        public static void AddTitle(this Slide slide, string title)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            // Get / Create the common slide data and shape tree
            if (slide.CommonSlideData == null)
                slide.CommonSlideData = new CommonSlideData(new ShapeTree());
            if (slide.CommonSlideData.ShapeTree == null)
                slide.CommonSlideData.ShapeTree = new ShapeTree();

            uint drawingObjectId = 1;

            // Get / Create the non-visual drawing properties of the slide
            NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.NonVisualGroupShapeProperties;
            if (nonVisualProperties == null)
            {
                nonVisualProperties = new NonVisualGroupShapeProperties()
                {
                    NonVisualDrawingProperties = new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "" },
                    NonVisualGroupShapeDrawingProperties = new NonVisualGroupShapeDrawingProperties(),
                    ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties()
                };
                slide.CommonSlideData.ShapeTree.AppendChild(nonVisualProperties);
            }

            // Add the title shape to the slide
            Shape titleShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());
            drawingObjectId++;

            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new NonVisualShapeProperties(
                new NonVisualDrawingProperties()
                {
                    Id = drawingObjectId, Name = "Title"
                },
                new NonVisualShapeDrawingProperties(new DocumentFormat.OpenXml.Drawing.ShapeLocks()
                {
                    NoGrouping = true
                }),
                new ApplicationNonVisualDrawingProperties(new PlaceholderShape()
                {
                    Type = PlaceholderValues.Title
                }));
            titleShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the title shape.
            titleShape.TextBody = new TextBody(new DocumentFormat.OpenXml.Drawing.BodyProperties(),
                    new DocumentFormat.OpenXml.Drawing.ListStyle(),
                    new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text() { Text = title })));
        }
    }
}