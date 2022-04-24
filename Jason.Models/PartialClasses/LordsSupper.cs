using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class LordsSupper : ILordsSupper, IWorshipServicePart
    {
        #region Properties
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        [XmlIgnore()]
        public WorshipServicePartTypes Type => WorshipServicePartTypes.LordsSupper;

        /// <summary>
        /// Gets or sets the scripture to be read during the Lord's Supper
        /// as an <see cref="IScripture"/>
        /// </summary>
        [XmlIgnore()]
        IScripture ILordsSupper.Scripture
        {
            get => Scripture;
            set => Scripture = Scripture.FromInterface(value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a <see cref="LordsSupper"/> from a <see cref="ILordsSupper"/>
        /// </summary>
        /// <param name="lordsSupper">
        /// The source object
        /// </param>
        /// <returns>
        /// A <see cref="LordsSupper"/>, or null if null is provided
        /// </returns>
        public static LordsSupper FromInterface(ILordsSupper lordsSupper)
            => lordsSupper is LordsSupper ? lordsSupper as LordsSupper :
                      lordsSupper == null ? null
                                          : new LordsSupper()
                                          {
                                              Scripture = Scripture.FromInterface(lordsSupper.Scripture)
                                          };
        #endregion
    }
}