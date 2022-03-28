using System.Xml.Serialization;

namespace Jason.Models
{
    public partial class ImageBackground : IHasBackgroundImage
    {
        private IWorshipServiceImage image;
        /// <summary>
        /// Gets or sets the image associated with the object
        /// </summary>
        [XmlIgnore()]
        public IWorshipServiceImage Image
        {
            get => image;
            set
            {
                image = value;
                BackgroundImageName = value?.Name;
            }
        }
    }
}