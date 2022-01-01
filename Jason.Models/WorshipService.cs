using System.Collections.Generic;

namespace Jason.Models
{
    public sealed class WorshipService
    {
        /// <summary>
        /// Gets the order of worship for the service
        /// </summary>
        public WorshipServiceOrder Order { get; }

        /// <summary>
        /// Gets a collection of images that will be used in the service
        /// </summary>
        public ICollection<WorshipServiceImage> Images { get; }

        public WorshipService(WorshipServiceOrder order, ICollection<WorshipServiceImage> images)
        {
            Order = order;
            Images = images;
        }
    }
}
