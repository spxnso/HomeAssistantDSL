namespace HomeAssistantDSL.Semantics.BoundAst;

public abstract class BoundLiteralExpression : BoundExpression
{
    public object Value { get; }
    public override Type Type => Value?.GetType() ?? typeof(object);
    public BoundLiteralExpression(object value)
    {
        Value = value;
    }
    public override BoundNodeKind Kind => BoundNodeKind.BoundLiteralExpression;
}
