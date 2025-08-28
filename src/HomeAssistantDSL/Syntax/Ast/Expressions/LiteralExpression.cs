using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public class LiteralExpression : Expression
{
    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

    public Token ValueToken { get; }

    public LiteralExpression(Token value)
    {
        ValueToken = value;
    }
}

public sealed class LiteralStringExpression : LiteralExpression
{
    public override SyntaxKind Kind => SyntaxKind.LiteralStringExpression;

    public LiteralStringExpression(Token value)
        : base(value)
    {
    }

    public string Value => ValueToken.Value;
}

