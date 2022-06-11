using Jason.Enumerations;
using Jason.Interfaces.WorshipService;
using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class Sermon : ISermon, IWorshipServicePart
    {
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        [XmlIgnore()]
        public WorshipServicePartTypes Type => WorshipServicePartTypes.Sermon;

        /// <summary>
        /// Creates a <see cref="Sermon"/> from a <see cref="ISermon"/>
        /// </summary>
        /// <param name="sermon">
        /// The source translation
        /// </param>
        /// <returns>
        /// A <see cref="Sermon"/>, or null if null is provided
        /// </returns>
        public static Sermon FromInterface(ISermon sermon)
            => sermon is Sermon ? sermon as Sermon :
                 sermon == null ? null
                                : new Sermon()
                                  {
                                      Title = sermon.Title,
                                      Presenter = sermon.Presenter
                                  };
    }
}