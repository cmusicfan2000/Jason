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
        /// Gets or sets the title of the song
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the presentation associated with the song
        /// </summary>
        string Slideshow { get; set; }

        /// <summary>
        /// Gets an array of parts in the song
        /// </summary>
        ISongPart[] Parts { get; }
    }
}