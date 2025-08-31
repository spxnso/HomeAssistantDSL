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
            _ => throw new NotImplementedException($"Binding not implemented for {stmt.Kind}"),
        };
    }

    private BoundExpressionStatement BindExpressionStatement(ExpressionStatement stmt)
    {
        return new BoundExpressionStatement(BindExpression(stmt.Expression));
    }



    private BoundDirectiveStatement BindDirectiveStatement(DirectiveStatement stmt)
    {
        var name = stmt.NameToken.Value;

        var symbol = _symbols.LookupOrThrow(name);

        if (stmt.EqualsToken is null)
            return new BoundDirectiveStatement(symbol, new BoundLiteralBooleanExpression(true));


        BoundLiteralExpression valueExpr;

        if (stmt.Value is LiteralExpression lit)
            valueExpr = BindLiteralExpression(lit);
        else
            throw new Exception($"Directive '{name}' with literal shortcut must have a literal value.");

        return new BoundDirectiveStatement(symbol, valueExpr);
    }


    private BoundExpression BindExpression(Expression expression)
    {
        return expression switch
        {
            LiteralExpression lit => BindLiteralExpression(lit),
            _ => throw new NotImplementedException($"Binding not implemented for {expression.Kind}"),
        };
    }

    private BoundLiteralExpression BindLiteralExpression(LiteralExpression expr)
    {
        return expr switch {
            LiteralStringExpression s => new BoundLiteralStringExpression(s.Value),
            _ => throw new NotSupportedException($"Binding for {expr.Kind} is not supported")
        };
    }

}