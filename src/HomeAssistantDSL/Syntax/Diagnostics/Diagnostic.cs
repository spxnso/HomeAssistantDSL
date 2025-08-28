using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Diagnostics;

public class Diagnostic {
    public Diagnostic(DiagnosticSeverity severity, string message, TokenPosition position)
    {
        Severity = severity;
        Message = message;
        Position = position;
    }

    public DiagnosticSeverity Severity { get; }
    public string Message { get; }
    public TokenPosition Position { get; }

    public override string ToString()
    {
        return $"[{Severity}] {Message}";
    }
}
