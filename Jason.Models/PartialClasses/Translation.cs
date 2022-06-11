using Jason.Interfaces.WorshipService;

namespace Jason.Models
{
    public partial class Translation : ITranslation
    {
        /// <summary>
        /// Creates a <see cref="Translation"/> from a <see cref="ITranslation"/>
        /// </summary>
        /// <param name="translation">
        /// The source translation
        /// </param>
        /// <returns>
        /// A <see cref="Translation"/>, or null if null is provided
        /// </returns>
        public static Translation FromInterface(ITranslation translation)
            => translation is Translation ? translation as Translation :
               translation == null ? null
                                   : new Translation()
                                    {
                                        FullName = translation.FullName,
                                        Abbreviation = translation.Abbreviation
                                    };
    }
}