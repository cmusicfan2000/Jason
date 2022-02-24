using Syncfusion.Presentation;
using System;

namespace Jason.ViewModels.Extensions
{
    /// <summary>
    /// Extensions to the <see cref="IColor"/> interface
    /// </summary>
    public static class IColorExtensions
    {
        /// <summary>
        /// Returns the correct foreground color (white or black) for the
        /// given background color
        /// </summary>
        /// <param name="c">
        /// The background color for which to calculate
        /// </param>
        public static IColor BackgroundToForeground(this IColor c)
        {
            int percievedBrightness = (int)Math.Sqrt(
            c.R * c.R * .241 +
            c.G * c.G * .691 +
            c.B * c.B * .068);

            return percievedBrightness > 130 ? ColorObject.Black
                                             : ColorObject.White;
        }
    }
}
