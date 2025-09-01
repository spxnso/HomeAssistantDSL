namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundDummyExpression : BoundExpression
{
    public override Type Type => typeof(object);

    public override BoundNodeKind Kind => BoundNodeKind.BoundDummyExpression;
}
