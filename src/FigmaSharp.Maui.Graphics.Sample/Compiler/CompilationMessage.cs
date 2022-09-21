namespace FigmaSharp.Maui.Graphics.Sample
{
    public class CompilationMessage
    {
        public bool IsError =>
            Severity == CompilationMessageSeverity.Error;

        public string Message { get; set; }

        public string DisplayMessage =>
            $"{(LineNumber != 0 ? $"[{LineNumber}]" : string.Empty)}{Severity.ToString().ToLowerInvariant()}: {Message}";

        public int StartOffset { get; set; }

        public int EndOffset { get; set; }

        public int LineNumber { get; set; }

        public CompilationMessageSeverity Severity { get; set; }
    }
}
