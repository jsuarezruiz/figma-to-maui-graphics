using FigmaSharp.Converters;
using FigmaSharp.Maui.Graphics.Extensions;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace FigmaSharp.Maui.Graphics.Converters
{
    internal class TextConverter : TextConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaText textNode)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("canvas.SaveState();");

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            if (textNode.HasFills)
            {
                var textPaint = textNode.fills.FirstOrDefault();

                if (textPaint.color != null)
                {
                    builder.AppendLine($"canvas.FontColor  = {textPaint.color.ToCodeString()};");

                    builder.AppendLine($"canvas.Alpha  = {textPaint.color.A};");
                }
            }

            var textStyle = textNode.style;

            if (textStyle != null)
            {
                var fontSize = textStyle.fontSize;
                builder.AppendLine($"canvas.FontSize = {fontSize}f;");
            }

            var bounds = textNode.absoluteBoundingBox;
            string text = textNode.name;

            builder.AppendLine($"canvas.DrawString(\"{text}\", {bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f, HorizontalAlignment.Left, VerticalAlignment.Top);");

            builder.AppendLine("canvas.RestoreState();");

            return builder.ToString();
        }

        public override Views.IView ConvertToView(FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService)
        {
            throw new NotImplementedException();
        }

        public override Type GetControlType(FigmaNode currentNode) 
            => typeof(View);
    }
}
