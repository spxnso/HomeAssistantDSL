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

