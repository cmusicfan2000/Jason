using System.Collections.Generic;

namespace Jason.Models
{
    public interface ISongPart
    {
        /// <summary>
        /// Gets or sets the name of the part
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets a collection of slide numbers to include in the part
        /// </summary>
        ICollection<int> SlideNumbers { get; }
    }
}