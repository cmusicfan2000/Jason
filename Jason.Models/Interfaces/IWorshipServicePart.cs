namespace Jason.Models
{
    /// <summary>
    /// Represents a part of a worship service
    /// </summary>
    public interface IWorshipServicePart
    {
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        WorshipServicePartTypes Type { get; }
    }
}