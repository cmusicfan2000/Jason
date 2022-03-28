namespace Jason.Models
{
    public interface ITranslation
    {
        /// <summary>
        /// Gets or sets the fully spelled-out name of the translation
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation representing the translation
        /// </summary>
        string Abbreviation { get; set; }
    }
}