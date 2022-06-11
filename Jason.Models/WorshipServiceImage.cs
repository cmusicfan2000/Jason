using Jason.Interfaces.WorshipService;
using System;
using System.IO;

namespace Jason.Models
{
    public sealed class WorshipServiceImage : IWorshipServiceImage
    {
        #region fields
        private byte[] data;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the image
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Constructor
        public WorshipServiceImage(string name, Stream data)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            SetData(data);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the image to the data contained in the stream
        /// </summary>
        /// <param name="source">
        /// The stream from which to update the data
        /// </param>
        public void SetData(Stream source)
        {
            if (source == null)
                data = new byte[] { };
            else if (!source.CanRead)
                throw new AccessViolationException("The provided stream cannot be read");
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    source.CopyTo(memoryStream);
                    data = memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets the image data as a <see cref="Stream"/>
        /// </summary>
        /// <returns>
        /// The image data as a <see cref="Stream"/>
        /// </returns>
        public MemoryStream AsMemoryStream() => new MemoryStream(data);
        #endregion
    }
}