using HomeAssistantDSL.Syntax.Diagnostics;
namespace HomeAssistantDSL.Semantics.BoundAst;

public sealed class BoundTree
{
    public IEnumerable<BoundStatement> Statements { get; }
    public DiagnosticBag Diagnostics { get; }

    public BoundTree(IEnumerable<BoundStatement> statements, DiagnosticBag diagnostics)
    {
        Statements = statements.ToList();
        Diagnostics = diagnostics;
    }
}