using HomeAssistantDSL.Semantics.BoundAst;
using HomeAssistantDSL.Semantics.Symbols;
using HomeAssistantDSL.Syntax.Ast;
using HomeAssistantDSL.Syntax.Diagnostics;

namespace HomeAssistantDSL.Semantics.Binder;

public class Binder
{
    public DiagnosticBag Diagnostics = new();
    private SymbolTable _symbols;
    public Binder(SymbolTable? symbols = null)
    {
        _symbols = symbols ?? new SymbolTable();
    }

    public BoundTree Bind(SyntaxTree tree)
    {
        if (tree.Diagnostics.Any())
        {
            Diagnostics.AddRange(tree.Diagnostics);
        }

        var boundStatements = new List<BoundStatement>();

        foreach (var stmt in tree.Statements)
        {
            var boundStmt = BindStatement(stmt);
            boundStatements.Add(boundStmt);
        }

        return new BoundTree(boundStatements, Diagnostics);
    }

    private BoundStatement BindStatement(Statement stmt)
    {
        return stmt switch
        {
            DirectiveStatement ds => BindDirectiveStatement(ds),
            ExpressionStatement es => BindExpressionStatement(es),
            DummyStatement => BindDummyStatement(),
            _ => BindUnsupportedStatement(stmt),
        };
    }

    private BoundExpressionStatement BindExpressionStatement(ExpressionStatement stmt)
    {
        return new BoundExpressionStatement(BindExpression(stmt.Expression));
    }



private BoundDirectiveStatement BindDirectiveStatement(DirectiveStatement stmt)
{
    var name = stmt.NameToken.Value;

    if (!_symbols.TryLookup(name, out var symbol))
    {
        // Create a placeholder so the rest of the pipeline can keep working
        symbol = new Symbol(name, SymbolKind.Dummy);
        Diagnostics.Add(DiagnosticSeverity.Error, $"Directive with symbol '{name}' is not defined.");
    }

    // Handle shortcut form: !FLAG  →  !FLAG = true
    if (stmt.EqualsToken is null)
        return new BoundDirectiveStatement(symbol, new BoundLiteralBooleanExpression(true));

    BoundLiteralExpression valueExpr;

    if (stmt.Value is LiteralExpression lit)
    {
        valueExpr = BindLiteralExpression(lit);
    }
    else
    {
        Diagnostics.Add(DiagnosticSeverity.Error, $"Directive '{name}' must have a literal value.");
        return new BoundDirectiveStatement(symbol, new BoundLiteralBooleanExpression(false));
    }

    if (symbol.Type != null && valueExpr.Value != null)
    {
        var valueType = valueExpr.Value.GetType();
        if (valueType != symbol.Type)
        {
            Diagnostics.Add(DiagnosticSeverity.Error, $"Directive '{name}' expected a value of type '{symbol.Type.Name}', but got '{valueType.Name}'.");
        }
    }

    return new BoundDirectiveStatement(symbol, valueExpr);
}


    private BoundDummyStatement BindDummyStatement()
    {
        Diagnostics.Add(DiagnosticSeverity.Error, $"Cannot bind dummy statement");

        return new BoundDummyStatement();
    }

    private BoundStatement BindUnsupportedStatement(Statement stmt)
    {
        Diagnostics.Add(DiagnosticSeverity.Error, $"Binding not implemented for {stmt.Kind}");

        return new BoundDummyStatement();
    }

    private BoundExpression BindExpression(Expression expression)
    {
        return expression switch
        {
            LiteralExpression lit => BindLiteralExpression(lit),
            DummyExpression dummy => BindDummyExpression(dummy),
            _ => BindUnsupportedExpression(expression),
        };
    }


    private BoundLiteralExpression BindLiteralExpression(LiteralExpression expr)
    {
        return expr switch
        {
            LiteralStringExpression s => new BoundLiteralStringExpression(s.Value),
            LiteralBooleanExpression b => new BoundLiteralBooleanExpression(bool.Parse(b.Value)),
            _ => throw Diagnostics.Error<NotImplementedException>(DiagnosticSeverity.Error, $"Binding not implemented for LiteralExpression of kind {expr.Kind}"),
        };
    }

    private BoundDummyExpression BindDummyExpression(Expression expr)
    {
        Diagnostics.Add(DiagnosticSeverity.Error, $"Cannot bind dummy expression");

        return new BoundDummyExpression();
    }

    private BoundExpression BindUnsupportedExpression(Expression expr)
    {
        Diagnostics.Add(DiagnosticSeverity.Error, $"Binding not implemented for {expr.Kind}");

        return new BoundDummyExpression();
    }


}