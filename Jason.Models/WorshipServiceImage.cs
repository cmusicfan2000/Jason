using System.IO;

namespace Jason.Models
{
    public class WorshipServiceImage
    {
        private byte[] data;

        /// <summary>
        /// Gets or sets the name of the image
        /// </summary>
        public string Name { get; set; }

        public WorshipServiceImage(string name, Stream data)
        {
            Name = name;
            SetData(data);
        }

        /// <summary>
        /// Sets the image to the data contained in the stream
        /// </summary>
        /// <param name="source">
        /// The stream from which to update the data
        /// </param>
        public void SetData(Stream source)
        {
            using (var memoryStream = new MemoryStream())
            {
                source.CopyTo(memoryStream);
                data = memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Gets the image data as a <see cref="Stream"/>
        /// </summary>
        /// <returns>
        /// The image data as a <see cref="Stream"/>
        /// </returns>
        public Stream AsStream() => new MemoryStream(data);
    }
}