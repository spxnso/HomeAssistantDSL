using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class LiteralStringExpression : LiteralExpression
{
    public override SyntaxKind Kind => SyntaxKind.LiteralStringExpression;

    public LiteralStringExpression(Token value)
        : base(value)
    {
    }

    public string Value => ValueToken.Value;
}

