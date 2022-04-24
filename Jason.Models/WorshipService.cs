﻿using Jason.Models.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jason.Models
{
    /// <summary>
    /// A basic implementation of the <see cref="IWorshipService"/> interface
    /// </summary>
    public class WorshipService : IWorshipService
    {
        /// <summary>
        /// Gets or sets the order or worship for the service
        /// </summary>
        public IWorshipServiceOrder Order { get; set; }

        /// <summary>
        /// Gets a collection of images associated with the service
        /// </summary>
        public ICollection<IWorshipServiceImage> Images { get; } = new Collection<IWorshipServiceImage>();

        /// <summary>
        /// Gets a collection of powerpoint presentations associated with the service
        /// </summary>
        public ICollection<IPowerpointPresentation> Presentations { get; } = new Collection<IPowerpointPresentation>();
    }
}