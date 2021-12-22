using DocumentFormat.OpenXml;

namespace Jason.ViewModels.Extensions
{
    /// <summary>
    /// Extends the behaviors of the <see cref="OpenXmlElement"/> class
    /// </summary>
    public static class OpenXmlElementExtensions
    {
        public static T GetOrCreateChild<T>(this OpenXmlElement parent) where T : OpenXmlElement, new()
        {
            T child = parent.GetFirstChild<T>();
            if (child == null)
            {
                child = new T();
                parent.Append(child);
            }
            return child;
        }
    }
}
