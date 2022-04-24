using System.Collections.Generic;

namespace Jason.Models.Interfaces
{
    /// <summary>
    /// Represents a worship service
    /// </summary>
    public interface IWorshipService
    {
        /// <summary>
        /// Gets or sets the order or worship for the service
        /// </summary>
        IWorshipServiceOrder Order { get; set; }

        /// <summary>
        /// Gets a collection of images associated with the service
        /// </summary>
        ICollection<IWorshipServiceImage> Images { get; }

        /// <summary>
        /// Gets a collection of powerpoint presentations associated with the service
        /// </summary>
        ICollection<IPowerpointPresentation> Presentations { get; }
    }
}