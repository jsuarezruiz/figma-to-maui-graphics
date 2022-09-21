using FigmaSharp.Converters;
using FigmaSharp.PropertyConfigure;
using FigmaSharp.Services;

namespace FigmaSharp.Maui.Graphics.PropertyConfigure
{
    public class CodePropertyConfigure : CodePropertyConfigureBase
    {
        public override string ConvertToCode(string propertyName, CodeNode currentNode, CodeNode parentNode, NodeConverter converter, CodeRenderService rendererService)
        {
            return string.Empty;
        }
    }
}