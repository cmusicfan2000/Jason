using Syncfusion.Presentation;

namespace Jason.Models
{
    public sealed class PowerpointPresentation : IPowerpointPresentation
    {
        /// <summary>
        /// Gets or sets the name of the presentation
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the presentation data
        /// </summary>
        public IPresentation Presentation { get; set; }
    }
}
