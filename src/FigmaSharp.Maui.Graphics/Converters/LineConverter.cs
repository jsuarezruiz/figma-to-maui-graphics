using FigmaSharp.Converters;
using FigmaSharp.Maui.Graphics.Extensions;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace FigmaSharp.Maui.Graphics.Converters
{
    internal class LineConverter : LineConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaLine lineNode)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            var bounds = lineNode.absoluteBoundingBox;

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            builder.AppendLine("canvas.SaveState();");

            if (lineNode.HasStrokes)
            {
                var strokePaint = lineNode.strokes.FirstOrDefault();

                if (strokePaint != null && strokePaint.visible)
                {
                    if (strokePaint.color != null)
                    {
                        builder.AppendLine($"canvas.StrokeColor  = {strokePaint.color.ToCodeString()};");

                        builder.AppendLine($"canvas.Alpha  = {strokePaint.color.A};");
                    }
                }

                if (lineNode.strokeWeight != 0)
                {
                    var strokeSize = lineNode.strokeWeight;
                    builder.AppendLine($"canvas.StrokeSize = {strokeSize};");
                }

                if (lineNode.strokeDashes != null && lineNode.strokeDashes.Count() > 0)
                {
                    var strokeSize = lineNode.strokeWeight;
                    builder.AppendLine($"canvas.StrokeDashPattern = {lineNode.strokeDashes.ToCodeString()};");
                }

            }

            var x1 = bounds.X;
            var y1 = bounds.Y;
            
            var x2 = bounds.X + bounds.Width;
            var y2 = bounds.Y + bounds.Height;
                  
            builder.AppendLine($"canvas.DrawLine(new Point({x1.ToString(nfi)}, {y1.ToString(nfi)}), new Point({x2.ToString(nfi)}, {y2.ToString(nfi)}));");

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
