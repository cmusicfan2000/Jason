using Syncfusion.Presentation;

namespace Jason.Models
{
    public interface IPowerpointPresentation
    {
        /// <summary>
        /// Gets or sets the name of the presentation
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the presentation data
        /// </summary>
        IPresentation Presentation { get; set; }
    }
}
