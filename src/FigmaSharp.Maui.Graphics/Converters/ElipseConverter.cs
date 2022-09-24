using FigmaSharp.Converters;
using FigmaSharp.Maui.Graphics.Extensions;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
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

            var bounds = elipseNode.absoluteBoundingBox;

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            if (elipseNode.HasFills)
            {
                var backgroundPaint = elipseNode.fills.FirstOrDefault();

                if (backgroundPaint != null && backgroundPaint.visible)
                {
                    if (backgroundPaint.color != null)
                    {
                        builder.AppendLine($"canvas.FillColor  = {backgroundPaint.color.ToCodeString()};");
                    
                        builder.AppendLine($"canvas.Alpha  = {backgroundPaint.color.A};");
                    }

                    if (backgroundPaint.gradientStops != null)
                    {
                        if (backgroundPaint.type.Equals("GRADIENT_LINEAR", StringComparison.CurrentCultureIgnoreCase))
                            builder.AppendLine($"canvas.SetFillPaint({backgroundPaint.gradientStops.ToLinearGradientPaint()}, new RectF({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f));");

                        if (backgroundPaint.type.Equals("GRADIENT_RADIAL", StringComparison.CurrentCultureIgnoreCase))
                            builder.AppendLine($"canvas.SetFillPaint({backgroundPaint.gradientStops.ToRadialGradientPaint()}, new RectF({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f));");
                    }

                    if (backgroundPaint.imageRef != null)
                        builder.AppendLine($"canvas.FillColor  = Colors.White;");

                    builder.AppendLine(string.Format($"canvas.FillEllipse({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f);"));
                }
            }

            if (elipseNode.HasStrokes)
            {
                var strokePaint = elipseNode.strokes.FirstOrDefault();

                if (strokePaint.color != null)
                {
                    builder.AppendLine($"canvas.StrokeColor  = {strokePaint.color.ToCodeString()};");

                    builder.AppendLine($"canvas.Alpha  = {strokePaint.color.A};");
                }

                if (strokePaint.gradientStops != null)
                {
                    if (strokePaint.type.Equals("GRADIENT_LINEAR", StringComparison.CurrentCultureIgnoreCase))
                        builder.AppendLine($"canvas.SetFillPaint({strokePaint.gradientStops.ToLinearGradientPaint()}, new RectF({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f));");

                    if (strokePaint.type.Equals("GRADIENT_RADIAL", StringComparison.CurrentCultureIgnoreCase))
                        builder.AppendLine($"canvas.SetFillPaint({strokePaint.gradientStops.ToRadialGradientPaint()}, new RectF({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f));");
                }

                if (strokePaint.imageRef != null)
                    builder.AppendLine($"canvas.StrokeColor  = Colors.White;");

                if (elipseNode.strokeWeight != 0)
                {
                    var strokeSize = elipseNode.strokeWeight;
                    builder.AppendLine($"canvas.StrokeSize = {strokeSize};");
                }

                builder.AppendLine(string.Format($"canvas.DrawEllipse({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f, {bounds.Width.ToString(nfi)}f, {bounds.Height.ToString(nfi)}f);"));
            }

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