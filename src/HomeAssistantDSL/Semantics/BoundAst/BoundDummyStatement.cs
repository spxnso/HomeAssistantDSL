namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundDummyStatement : BoundStatement
{    public override BoundNodeKind Kind => BoundNodeKind.BoundDummyStatement;
}