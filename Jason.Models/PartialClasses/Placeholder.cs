using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class Placeholder : IPlaceholder, IWorshipServicePart
    {
        #region Properties
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        [XmlIgnore()]
        public WorshipServicePartTypes Type => WorshipServicePartTypes.Placeholder;
        #endregion

        #region Methods
        /// <summary>
        /// Creates a <see cref="Placeholder"/> from a <see cref="IPlaceholder"/>
        /// </summary>
        /// <param name="placeholder">
        /// The source placeholder
        /// </param>
        /// <returns>
        /// A <see cref="Placeholder"/>, or null if null is provided
        /// </returns>
        public static Placeholder FromInterface(IPlaceholder placeholder)
            => placeholder is Placeholder ? placeholder as Placeholder :
                      placeholder == null ? null
                                          : new Placeholder()
                                            {
                                                Image = placeholder.Image,
                                                Name = placeholder.Name
                                            };
        #endregion
    }
}