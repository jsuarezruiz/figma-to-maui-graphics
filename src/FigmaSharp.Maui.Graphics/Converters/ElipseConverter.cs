using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Text;

namespace FigmaSharp.Maui.Graphics.Converters
{
    internal class ElipseConverter : ElipseConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaElipse elipseNode)
            {
                return string.Empty;
            }
            
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("canvas.SaveState();");

            if (elipseNode.strokeWeight != 0)
            {
                var strokeSize = elipseNode.strokeWeight;
                builder.AppendLine($"canvas.StrokeSize = {strokeSize};");
            }
                
            var bounds = elipseNode.absoluteBoundingBox;
            builder.AppendLine(string.Format($"canvas.DrawEllipse({bounds.X}, {bounds.Y}, {bounds.Width}, {bounds.Height});"));

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