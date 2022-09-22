using FigmaSharp.Models;
using Newtonsoft.Json.Linq;
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
            StringBuilder builder = new StringBuilder();
             
            builder.Append("new LinearGradientPaint");
            builder.Append("{");

            builder.Append("GradientStops = new PaintGradientStop[]");
            builder.Append("{");

            int i = 0;
            var separator = ",";

            foreach (var colorStop in colorStops)
            {
                var color = colorStop.color;

                int red = Convert.ToInt32(color.R * 255);
                int green = Convert.ToInt32(color.G * 255);
                int blue = Convert.ToInt32(color.B * 255);

                builder.Append($"new PaintGradientStop({colorStop.position}, new Color({red}, {green}, {blue})) {(i < colorStops.Count() ? separator : string.Empty)}");
                i++;
            }

            builder.Append("}");

            builder.Append("}");

            return builder.ToString();
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

        public static string ToCodeString(this FigmaTypeStyle style)
        {
            var fontFamily = style.fontPostScriptName ?? style.fontFamily;
            var fontWeight = style.fontWeight;

            string fontStyleType = "FontStyleType.Normal";

            if (!string.IsNullOrEmpty(fontFamily) && fontFamily.Contains("Italic", StringComparison.CurrentCultureIgnoreCase))
                fontStyleType = "FontStyleType.Italic";

            return $"new Microsoft.Maui.Graphics.Font(\"{fontFamily}\", {fontWeight}, {fontStyleType})";
        }
   
        public static string ToHorizontalAignment(this string value)
        {
            switch(value)
            {
                case "LEFT":
                    return "HorizontalAlignment.Left";
                case "CENTER":
                    return "HorizontalAlignment.Center";
                case "RIGHT":
                    return "HorizontalAlignment.Right";
                case "SCALE":
                    return "HorizontalAlignment.Justified";
                default:
                    return "HorizontalAlignment.Left";
            }
        }

        public static string ToVerticalAlignment(this string value)
        {
            switch (value)
            {
                case "TOP":
                    return "VerticalAlignment.Top";
                case "CENTER":
                case "SCALE":
                    return "VerticalAlignment.Center";
                case "BOTTOM":
                    return "VerticalAlignment.Bottom";
                default:
                    return "VerticalAlignment.Top";
            }
        }
    }
}