namespace HomeAssistantDSL.Semantics.BoundAst;

public abstract class BoundExpression : BoundNode {
    public abstract Type Type { get; }
}
