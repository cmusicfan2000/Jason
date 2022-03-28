namespace Jason.Models
{
    /// <summary>
    /// Represents a part of a worship service which may have an image associated with it
    /// </summary>
    public interface IHasBackgroundImage
    {
        /// <summary>
        /// Gets or sets the background image
        /// </summary>
        IWorshipServiceImage Image { get; set; }
    }
}