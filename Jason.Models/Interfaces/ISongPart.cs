namespace Jason.Models
{
    public interface ISongPart
    {
        /// <summary>
        /// Gets or sets the name of the part
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the space-seperated slide numbers to include in the part
        /// </summary>
        string Slides { get; set; } 
    }
}