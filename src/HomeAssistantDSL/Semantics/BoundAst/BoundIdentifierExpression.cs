
using HomeAssistantDSL.Semantics.Symbols;

namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundIdentifierExpression : BoundExpression
{
    public BoundIdentifierExpression(Symbol symbol)
    {
        Symbol = symbol;
    }

    public Symbol Symbol { get; }

    public override Type Type => Symbol.Type ?? typeof(object);

    public override BoundNodeKind Kind => BoundNodeKind.BoundIdentifierExpression;
}