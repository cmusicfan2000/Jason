using Syncfusion.Presentation;
using System.Collections.Generic;

namespace Jason.Models
{
    /// <summary>
    /// Represents a song in a worship service
    /// </summary>
    public interface ISong
    {
        /// <summary>
        /// Gets or sets the songbook number associated with the song
        /// </summary>
        ushort? SongBookNumber { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the presentation associated with the song
        /// </summary>
        string Slideshow { get; set; }

        /// <summary>
        /// Gets or sets the presentation associated with this song
        /// </summary>
        IPowerpointPresentation Presentation { get; set; }

        /// <summary>
        /// Gets a collection of parts in the song
        /// </summary>
        ICollection<ISongPart> Parts { get; }
    }
}