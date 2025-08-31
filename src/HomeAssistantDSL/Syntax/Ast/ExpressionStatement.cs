namespace HomeAssistantDSL.Syntax.Ast;

public sealed class ExpressionStatement: Statement {
    public ExpressionStatement(Expression expression)
    {
        Expression = expression;
    }

    public Expression Expression { get; }

    public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;
}