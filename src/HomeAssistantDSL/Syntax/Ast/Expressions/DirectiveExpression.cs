using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class DirectiveExpression : Expression {
    public override SyntaxKind Kind => SyntaxKind.DirectiveExpression;

    public Token BangToken { get; }
    public Token NameToken { get; }

    public DirectiveExpression(Token bang, Token name)
    {
        BangToken = bang;
        NameToken = name;
    }
}