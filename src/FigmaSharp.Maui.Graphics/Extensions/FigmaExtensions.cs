using FigmaSharp.Models;
using System.Globalization;
using System.Text;

namespace FigmaSharp.Maui.Graphics.Extensions
{
    public static class FigmaExtensions
    {
        public static string ToCodeString(this Color color)
        {
            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            int red = Convert.ToInt32(color.R * 255);
            int green = Convert.ToInt32(color.G * 255);
            int blue = Convert.ToInt32(color.B * 255); 

            return $"Color.FromRgb({red.ToString(nfi)}, {green.ToString(nfi)}, {blue.ToString(nfi)})";
        }

        public static string ToCodeString(this ColorStop[] colorStops)
        {
            return string.Empty;
        }

        public static string ToCodeString(this float[] values)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("new float[] {");
            var separator = ",";
            int i = 0;

            foreach (var value in values)
            {
                builder.Append($"{ToCodeString(value)}{(i < values.Length ? separator : string.Empty)}");
                i++;
            }

            builder.Append("}");

            return builder.ToString();
        }

        public static string ToCodeString(this float value)
        {
            return string.Concat(value.ToString(), "f");
        }

        public static string ToCodeString(this double value)
        {
            return string.Concat(value.ToString(), "f");
        }
    }
}