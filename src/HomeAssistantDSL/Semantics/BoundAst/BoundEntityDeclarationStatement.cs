using HomeAssistantDSL.Semantics.Symbols;

namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundEntityDeclarationStatement : BoundStatement {
    public override BoundNodeKind Kind => BoundNodeKind.BoundEntityDeclarationStatement;

    public Symbol Name { get; }
    public Symbol Type { get; }

    public BoundEntityDeclarationStatement(Symbol name, Symbol type)
    {
        Name = name;
        Type = type;
    }
}

