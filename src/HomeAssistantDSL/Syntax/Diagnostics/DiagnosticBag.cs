using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Diagnostics;

public class DiagnosticBag : List<Diagnostic>
{
    public void Add(DiagnosticSeverity severity, string message, TokenPosition position)
    {
        Add(new Diagnostic(severity, message, position));
    }
}