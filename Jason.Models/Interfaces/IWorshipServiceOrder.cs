using System.Collections.Generic;

namespace Jason.Models
{
    public interface IWorshipServiceOrder
    {
        /// <summary>
        /// Gets or sets the theme color in the format #RRGGBB, where
        /// R, G, and B are in hex
        /// </summary>
        string ThemeColor { get; set; }

        /// <summary>
        /// Gets an array of objects representing parts of a worship service order
        /// </summary>
        IWorshipServicePart[] Parts { get; }
    }
}