namespace Jason.Interfaces.WorshipService
{
    public interface ISermon
    {
        /// <summary>
        /// Gets or sets the title of the sermon
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the speaker
        /// </summary>
        string Presenter { get; set; }
    }
}
