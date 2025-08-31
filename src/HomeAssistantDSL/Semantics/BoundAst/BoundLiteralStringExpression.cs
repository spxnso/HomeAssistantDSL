namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundLiteralStringExpression : BoundLiteralExpression
{
    public new string Value { get; }
    public BoundLiteralStringExpression(string value) : base(value) => Value = value;
    public override Type Type => typeof(string);
    public override BoundNodeKind Kind => BoundNodeKind.BoundLiteralStringExpression;
}
