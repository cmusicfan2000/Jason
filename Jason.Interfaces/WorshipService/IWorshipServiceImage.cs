using System.IO;

namespace Jason.Interfaces.WorshipService
{
    public interface IWorshipServiceImage
    {
        /// <summary>
        /// Gets or sets the name of the image
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Sets the image to the data contained in the stream
        /// </summary>
        /// <param name="source">
        /// The stream from which to update the data
        /// </param>
        void SetData(Stream source);

        /// <summary>
        /// Gets the image data as a <see cref="MemoryStream"/>
        /// </summary>
        /// <returns>
        /// The image as a <see cref="MemoryStream"/>
        /// </returns>
        MemoryStream AsMemoryStream();
    }
}