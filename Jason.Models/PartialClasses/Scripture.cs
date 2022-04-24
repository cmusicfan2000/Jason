using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class Scripture : IScripture, IWorshipServicePart
    {
        #region Properties
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        [XmlIgnore()]
        public WorshipServicePartTypes Type => WorshipServicePartTypes.Scripture;

        /// <summary>
        /// Gets or sets the translation of the Bible from which the scripture comes
        /// </summary>
        [XmlIgnore()]
        public ITranslation Translation
        {
            get => Item;
            set => Item = Models.Translation.FromInterface(value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a <see cref="Scripture"/> instance from an <see cref="IScripture"/>
        /// instance
        /// </summary>
        /// <param name="scripture">
        /// The <see cref="IScripture"/> from which to create the <see cref="Scripture"/>
        /// </param>
        /// <returns>
        /// A new <see cref="Scripture"/> instance created from an <see cref="IScripture"/>
        /// </returns>
        public static Scripture FromInterface(IScripture scripture)
            => scripture is Scripture ? scripture as Scripture :
                 scripture == null ? null
                                   : new Scripture()
                                    {
                                        BackgroundImageName = scripture.BackgroundImageName,
                                        Book = scripture.Book,
                                        Reference = scripture.Reference,
                                        Text = scripture.Text,
                                        Translation = scripture.Translation
                                    };
        #endregion
    }
}