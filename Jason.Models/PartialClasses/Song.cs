using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private ICollection<ISongPart> parts;
        /// <summary>
        /// Gets a collection of parts in the song
        /// </summary>
        [XmlIgnore()]
        public ICollection<ISongPart> Parts
        {
            get
            {
                if (parts == null)
                {
                    ObservableCollection<ISongPart> observableParts = new ObservableCollection<ISongPart>(Part);
                    observableParts.CollectionChanged += OnPartsCollectionChanged;
                    parts = observableParts;
                }

                return parts;
            }
        }

        private IPowerpointPresentation presentation;
        /// <summary>
        /// Gets or sets the presentation associated with this song
        /// </summary>
        public IPowerpointPresentation Presentation
        {
            get => presentation;
            set
            {
                presentation = value;
                Slideshow = presentation.Name;
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
                                Presentation = song.Presentation,
                                Title = song.Title,
                                Part = song.Parts.Select(p => SongPart.FromInterface(p)).ToArray()
                            };
        #endregion

        #region Event Handlers
        private void OnPartsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Part = (sender as IEnumerable<ISongPart>)?.Select(isp => SongPart.FromInterface(isp))
                                                     .ToArray()
                                                     ?? new SongPart[] { };
        }
        #endregion
    }
}