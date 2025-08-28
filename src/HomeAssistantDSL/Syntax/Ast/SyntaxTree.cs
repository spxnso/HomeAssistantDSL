using HomeAssistantDSL.Syntax.Diagnostics;
using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class SyntaxTree {
    public SyntaxTree(List<Statement> statements, Token eof, DiagnosticBag diagnostics)
    {
        Statements = statements;
        Eof = eof;
        Diagnostics = diagnostics;
    }

    public List<Statement> Statements { get; }
    public Token Eof { get; }
    public DiagnosticBag Diagnostics { get; }
}