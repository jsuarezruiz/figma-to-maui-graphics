using System.Reflection;

namespace FigmaSharp.Maui.Graphics.Sample
{
    public class CompilationResult
    {
        private MethodInfo drawMethod = null;
        private object instance = null;

        public IEnumerable<CompilationMessage> CompilationMessages { get; set; }

        public Type ScriptType { get; set; }

        public bool HasErrors => CompilationMessages.Any(m => m.IsError);

        public IEnumerable<CompilationMessage> Draw(ICanvas canvas, RectF dirtyRect)
        {
            var messages = new List<CompilationMessage>();

            if (ScriptType != null && drawMethod == null)
            {
                drawMethod = ScriptType.GetMethod(
                     "Draw",
                     BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                     null,
                     new[] { typeof(ICanvas), typeof(RectF) },
                     null);

                if (drawMethod == null)
                {
                    messages.Add(new CompilationMessage
                    {
                        Message = "Unable to find entry method 'void Draw(ICanvas canvas, RectF dirtyRect)'.",
                        Severity = CompilationMessageSeverity.Error
                    });
                }
                else if (!drawMethod.IsStatic)
                {
                    instance = Activator.CreateInstance(ScriptType, new[] { new object[2] });
                }
            }

            try
            {
                drawMethod?.Invoke(instance, new object[] { canvas, dirtyRect });
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException tiex)
                    ex = tiex.InnerException;

                messages.Add(new CompilationMessage
                {
                    Message = $"An error occured during execution: {ex.Message}",
                    Severity = CompilationMessageSeverity.Error
                });
            }

            return messages;
        }
    }
}