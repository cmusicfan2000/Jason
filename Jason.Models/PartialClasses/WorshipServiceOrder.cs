using System.Linq;
using System.Xml.Serialization;

namespace Jason.Models
{
    /// <summary>
    /// Represents an order of worship
    /// </summary>
    public partial class WorshipServiceOrder : IWorshipServiceOrder
    {
        /// <summary>
        /// Gets a collection of parts in the song
        /// </summary>
        [XmlIgnore()]
        public IWorshipServicePart[] Parts
        {
            get => Items?.OfType<IWorshipServicePart>()
                        .ToArray();
            set => Items = value;
        }
    }
}