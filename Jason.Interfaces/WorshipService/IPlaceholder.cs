namespace Jason.Interfaces.WorshipService
{
    /// <summary>
    /// Represents a placeholder in a worship service, such as a welcome slide
    /// </summary>
    public interface IPlaceholder : IHasBackgroundImage
    {
        /// <summary>
        /// Gets or sets the name of the placeholder
        /// </summary>
        string Name { get; set; }
    }
}