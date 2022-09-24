using FigmaSharp.Converters;
using FigmaSharp.Maui.Graphics.Extensions;
using FigmaSharp.Models;
using FigmaSharp.Services;
using Microsoft.Maui;
using System.Globalization;
using System.Text;

namespace FigmaSharp.Maui.Graphics.Converters
{
    internal class ImageConverter : NodeConverter
    {
        int _figmaVectorCount;

        public override bool CanConvert(FigmaNode currentNode)
            => currentNode.GetType() == typeof(FigmaVector);

        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaVector figmaVector)
            {
                return string.Empty;
            }
            
            if (figmaVector.fillGeometry == null || figmaVector.fillGeometry.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            var bounds = figmaVector.absoluteBoundingBox;

            builder.AppendLine("canvas.SaveState();");

            if (figmaVector.HasFills)
            {
                var backgroundPaint = figmaVector.fills.FirstOrDefault();

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

                    foreach (var geometry in figmaVector.fillGeometry)
                    {
                        builder.AppendLine($"canvas.Translate({bounds.X.ToString(nfi)}f, {bounds.Y.ToString(nfi)}f);");

                        string name = $"vector{_figmaVectorCount}";

                        builder.AppendLine($"var {name}Builder = new PathBuilder();");
                        builder.AppendLine($"var {name}path = {name}Builder.BuildPath(\"{geometry.path}\");");
                        builder.AppendLine($"canvas.FillPath({name}path);");

                        _figmaVectorCount++;
                    }
                }
            }

            if (figmaVector.HasStrokes)
            {
                var strokePaint = figmaVector.fills.FirstOrDefault();

                if (strokePaint != null && strokePaint.visible)
                {
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
                }

                var strokeSize = figmaVector.strokeWeight;
                builder.AppendLine($"canvas.StrokeSize  = {strokeSize};");

                foreach (var geometry in figmaVector.fillGeometry)
                {
                    builder.AppendLine($"canvas.Translate({figmaVector.absoluteBoundingBox.X.ToString(nfi)}f, {figmaVector.absoluteBoundingBox.Y.ToString(nfi)}f);");

                    string name = $"vector{_figmaVectorCount}";

                    builder.AppendLine($"var {name}Builder = new PathBuilder();");
                    builder.AppendLine($"var {name}path = {name}Builder.BuildPath(\"{geometry.path}\");");
                    builder.AppendLine($"canvas.DrawPath({name}path);");

                    _figmaVectorCount++;
                }
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
