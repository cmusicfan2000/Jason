namespace Jason.Interfaces.WorshipService
{
    /// <summary>
    /// Represents a part of a worship service which may have an image associated with it
    /// </summary>
    public interface IHasBackgroundImage
    {
        /// <summary>
        /// Gets or sets the name of the background image
        /// </summary>
        string BackgroundImageName { get; set; }
    }
}