using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class DummyStatement : Statement {
    public DummyStatement(Token token)
    {
        Token = token;
    }

    public Token Token { get; }

    public override SyntaxKind Kind => SyntaxKind.DummyStatement;
}

