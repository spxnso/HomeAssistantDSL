using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class LiteralBooleanExpression : LiteralExpression
{
    public override SyntaxKind Kind => SyntaxKind.LiteralBooleanExpression;

    public LiteralBooleanExpression(Token value)
        : base(value)
    {
    }

    public string Value => ValueToken.Value;
}

