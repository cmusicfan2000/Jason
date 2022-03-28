using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class WorshipServiceOrder : IWorshipServiceOrder
    {
        private ICollection<IWorshipServicePart> parts;
        /// <summary>
        /// Gets a collection of parts in the song
        /// </summary>
        [XmlIgnore()]
        public ICollection<IWorshipServicePart> Parts
        {
            get
            {
                if (parts == null)
                {
                    ObservableCollection<IWorshipServicePart> observableParts = new ObservableCollection<IWorshipServicePart>(Items.OfType<IWorshipServicePart>());
                    observableParts.CollectionChanged += OnPartsCollectionChanged;
                    parts = observableParts;
                }

                return parts;
            }
        }

        private void OnPartsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Collection<object> parts = new Collection<object>();

            foreach (IWorshipServicePart part in Parts)
            {
                if (part.Type == WorshipServicePartTypes.FamilyNewsAndPrayer)
                    parts.Add(new FamilyNewsAndPrayer());
                else if (part is ILordsSupper lsPart)
                    parts.Add(LordsSupper.FromInterface(lsPart));
                else if (part is IPlaceholder phPart)
                    parts.Add(Placeholder.FromInterface(phPart));
                else if (part is IScripture sPart)
                    parts.Add(Scripture.FromInterface(sPart));
                else if (part is ISermon sermonPart)
                    parts.Add(Sermon.FromInterface(sermonPart));
                else if (part is ISong songPart)
                    parts.Add(Song.FromInterface(songPart));
            }

            Items = parts.ToArray();
        }
    }
}
