using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;

namespace FigmaSharp.Maui.Graphics.Converters
{
    public class FrameConverter : FrameConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            return string.Empty;
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
