using System.Linq;
using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class Song : ISong, IWorshipServicePart
    {
        #region Properties
        /// <summary>
        /// Gets the type of part represented
        /// </summary>
        [XmlIgnore()]
        public WorshipServicePartTypes Type => WorshipServicePartTypes.Song;

        /// <summary>
        /// Gets or sets the songbook number associated with the song
        /// </summary>
        [XmlIgnore()]
        public ushort? SongBookNumber
        {
            get => BookNumberSpecified ? (ushort?)BookNumber
                                       : null;
            set
            {
                if (value == null)
                {
                    BookNumber = 0;
                    BookNumberSpecified = false;
                }
                else
                {
                    BookNumber = (ushort)value;
                    BookNumberSpecified = true;
                }
            }
        }

        /// <summary>
        /// Gets an array of parts in the song
        /// </summary>
        [XmlIgnore()]
        public ISongPart[] Parts
        {
            get => Part;
            set
            {
                Part = value.Select(isp => SongPart.FromInterface(isp))
                            .ToArray();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a <see cref="Song"/> from a <see cref="ISong"/>
        /// </summary>
        /// <param name="song">
        /// The source song
        /// </param>
        /// <returns>
        /// A <see cref="Song"/>, or null if null is provided
        /// </returns>
        public static Song FromInterface(ISong song)
            => song is Song ? song as Song :
               song == null ? null
                            : new Song()
                            {
                                SongBookNumber = song.SongBookNumber,
                                Slideshow = song.Slideshow,
                                Title = song.Title,
                                Part = song.Parts
                                           .Select(p => SongPart.FromInterface(p))
                                           .ToArray()
                            };
        #endregion
    }
}