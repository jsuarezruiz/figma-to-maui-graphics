using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Text;

namespace FigmaSharp.Maui.Graphics.Converters
{
    internal class ImageConverter : NodeConverter
    {
        public override bool CanConvert(FigmaNode currentNode)
            => currentNode.GetType() == typeof(FigmaVector);

        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not IFigmaImage figmaImage)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            if (figmaImage.HasImage())
            {
                builder.AppendLine("canvas.SaveState();");

                // TODO: Render Images

                builder.AppendLine("canvas.RestoreState();");
            }

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
