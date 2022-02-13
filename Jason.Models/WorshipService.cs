using Syncfusion.Presentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jason.Models
{
    public sealed class WorshipService
    {
        /// <summary>
        /// Gets the order of worship for the service
        /// </summary>
        public WorshipServiceOrder Order { get; set; }

        /// <summary>
        /// Gets a collection of images that will be used in the service
        /// </summary>
        public ICollection<WorshipServiceImage> Images { get; set; } = new Collection<WorshipServiceImage>();

        /// <summary>
        /// Gets a collection of presentations containing slides for songs in the service
        /// </summary>
        public IDictionary<string, IPresentation> Songs { get; set; } = new Dictionary<string, IPresentation>();
    }
}