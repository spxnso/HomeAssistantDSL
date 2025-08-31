namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundLiteralBooleanExpression : BoundLiteralExpression
{
    public new bool Value { get; }
    public BoundLiteralBooleanExpression(bool value) : base(value) => Value = value;
    public override Type Type => typeof(bool);
    public override BoundNodeKind Kind => BoundNodeKind.BoundLiteralBooleanExpression;
}