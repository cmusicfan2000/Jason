using Jason.Enumerations;

namespace Jason.Interfaces.WorshipService
{
    public interface IScripture : IHasBackgroundImage
    {
        /// <summary>
        /// Gets or sets the chapter and verse reference
        /// </summary>
        string Reference { get; set; }

        /// <summary>
        /// Gets or sets the text of the scripture
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the translation to which the scripture belongs
        /// </summary>
        ITranslation Translation { get; set; }

        /// <summary>
        /// Gets or sets the book of the Bible in which the scripture is found
        /// </summary>
        ScriptureBook Book { get; set; }
    }
}