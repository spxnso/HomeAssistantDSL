using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Diagnostics;

public class DiagnosticBag : List<Diagnostic>
{
    public void Add(DiagnosticSeverity severity, string message, TokenPosition? position = null)
    {
        Add(new Diagnostic(severity, message, position));
    }

    public T Error<T>(DiagnosticSeverity severity, string message, TokenPosition? position = null) where T: Exception {
        Add(severity, message, position);
        return (T)Activator.CreateInstance(typeof(T), message)!;
    }
}