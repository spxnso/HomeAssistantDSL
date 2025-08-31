using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class DirectiveStatement : Statement {
    public override SyntaxKind Kind => SyntaxKind.DirectiveStatement;

    public Token BangToken { get; }
    public Token NameToken { get; }
    public Token? EqualsToken { get; }

    public Expression? Value { get; }

    public DirectiveStatement(Token bang, Token name, Token? equals = null, Expression? value = null)
    {
        BangToken = bang;
        NameToken = name;
        EqualsToken = equals;
        Value = value;
    }
}