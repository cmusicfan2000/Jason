using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class SongPart : ISongPart
    {
        #region Properties
        private ICollection<int> slideNumbes;
        /// <summary>
        /// Gets a collection of slide numbers to include in the part
        /// </summary>
        [XmlIgnore()]
        public ICollection<int> SlideNumbers
        {
            get
            {
                if (slideNumbes == null)
                {
                    ObservableCollection<int> numbers = new ObservableCollection<int>(Slides?.Split(' ')
                                                                                             .Where(i => int.TryParse(i, out _))
                                                                                             .Select(s => int.Parse(s)) ?? Enumerable.Empty<int>());
                    numbers.CollectionChanged += OnNumbersCollectionChanged;
                    slideNumbes = numbers;
                }

                return slideNumbes;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a <see cref="SongPart"/> from a <see cref="ISongPart"/>
        /// </summary>
        /// <param name="part">
        /// The source part
        /// </param>
        /// <returns>
        /// A <see cref="SongPart"/>, or null if null is provided
        /// </returns>
        public static SongPart FromInterface(ISongPart part)
            => part is SongPart ? part as SongPart :
                   part == null ? null
                                :  new SongPart()
                                   {
                                       Name = part.Name,
                                       Slides = SlidesCollectionToString(part.SlideNumbers)
                                   };
        
        /// <summary>
        /// Update the <see cref="Slides"/> property when the collection of slide numbers changes
        /// </summary>
        /// <param name="sender">The collection that fired the event</param>
        /// <param name="e">Not used</param>
        private void OnNumbersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => Slides = SlidesCollectionToString(sender as IEnumerable<int>);

        private static string SlidesCollectionToString(IEnumerable<int> slides)
            => slides?.Select(i => i.ToString())
                     ?.Aggregate((x, y) => $"{x} {y}") ?? string.Empty;
        #endregion
    }
}