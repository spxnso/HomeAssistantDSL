using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class IdentifierExpression : Expression
{
    public override SyntaxKind Kind => SyntaxKind.IdentifierExpression;

    public Token IdentifierToken { get; }

    public IdentifierExpression(Token identifierToken)
    {
        IdentifierToken = identifierToken;
    }

    public string Name => IdentifierToken.Value;
}