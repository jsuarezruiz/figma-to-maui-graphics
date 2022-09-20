using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
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


            var textStyle = textNode.style;
            
            if(textStyle != null)
            {
                var fontSize = textStyle.fontSize;
                builder.AppendLine($"canvas.FontSize = {fontSize}f;");

                var textFill = textStyle.fills;

                if (textFill != null && textFill.Length > 0)
                {
                    var textColor = textFill[0].color;
                    builder.AppendLine($"canvas.FontColor  = {textColor};");
                }
            }

            var bounds = textNode.absoluteBoundingBox;
            string text = textNode.name;
            builder.AppendLine($"canvas.DrawString(\"{text}\", {bounds.X}, {bounds.Y}, {bounds.Width}, {bounds.Height}, HorizontalAlignment.Center, VerticalAlignment.Center);");
            
            builder.AppendLine("canvas.RestoreState();");

            return builder.ToString();
        }

        public override Views.IView ConvertToView(FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService)
        {
            throw new NotImplementedException();
        }

        public override Type GetControlType(FigmaNode currentNode) => typeof(View);
    }
}
