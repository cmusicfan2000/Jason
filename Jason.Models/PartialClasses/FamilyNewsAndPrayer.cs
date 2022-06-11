using Jason.Enumerations;
using Jason.Interfaces.WorshipService;
using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class FamilyNewsAndPrayer : IWorshipServicePart
    {
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        [XmlIgnore()]
        public WorshipServicePartTypes Type => WorshipServicePartTypes.FamilyNewsAndPrayer;
    }
}