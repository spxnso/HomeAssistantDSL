using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class DummyExpression : Expression {
    public DummyExpression(Token token)
    {
        Token = token;
    }

    public Token Token { get; }

    public override SyntaxKind Kind => SyntaxKind.DummyExpression;
}

