using HomeAssistantDSL.Semantics.Symbols;

namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundDirectiveStatement : BoundStatement {
    public BoundDirectiveStatement(Symbol symbol, BoundLiteralExpression value)
    {
        Symbol = symbol;
        Value = value;
    }

    public Symbol Symbol { get; }
    public BoundLiteralExpression Value { get; }

    public override BoundNodeKind Kind => BoundNodeKind.BoundDirectiveStatement;
}

