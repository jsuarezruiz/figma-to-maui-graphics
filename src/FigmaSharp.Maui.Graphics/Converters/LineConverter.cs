using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
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

            builder.AppendLine("canvas.SaveState();");

            builder.AppendLine("canvas.RestoreState();");

            return builder.ToString();
        }

        public override Views.IView ConvertToView(FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService)
        {
            throw new NotImplementedException();
        }

        public override Type GetControlType(FigmaNode currentNode)
        {
            throw new NotImplementedException();
        }
    }
}
