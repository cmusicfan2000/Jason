namespace Jason.Interfaces.WorshipService
{
    public interface ILordsSupper
    {
        /// <summary>
        /// Gets or sets the scripture to be read during the Lord's Supper
        /// </summary>
        IScripture Scripture { get; set; }
    }
}