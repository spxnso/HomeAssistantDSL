// made using chatgpt, gonna write one later
using HomeAssistantDSL.Semantics.BoundAst;
using HomeAssistantDSL.Syntax.Ast;

public static class PrettyPrinter
{
    public static void Print(SyntaxTree tree)
    {
        foreach (var stmt in tree.Statements)
        {
            PrintStatement(stmt, indent: 0);
        }
    }

    private static void PrintStatement(Statement stmt, int indent)
    {
        var pad = new string(' ', indent * 2);

        switch (stmt)
        {
            case DirectiveStatement directive:
                Console.WriteLine($"{pad}DirectiveStatement:");
                Console.WriteLine($"{pad}  BangToken: {directive.BangToken.Value}");
                Console.WriteLine($"{pad}  Identifier: {directive.NameToken.Value}");
                if (directive.EqualsToken != null)
                    Console.WriteLine($"{pad}  EqualToken: {directive.EqualsToken.Value}");

                if (directive.Value != null)
                {
                    Console.WriteLine($"{pad}  Value:");
                    PrintExpression(directive.Value, indent + 2);
                }



                break;

            default:
                Console.WriteLine($"{pad}Unknown Statement: {stmt.Kind}");
                break;
        }
    }

    private static void PrintExpression(Expression expr, int indent)
    {
        var pad = new string(' ', indent * 2);

        switch (expr)
        {

            case IdentifierExpression ident:
                Console.WriteLine($"{pad}IdentifierExpression: {ident.IdentifierToken.Value}");
                break;

            case LiteralExpression literal:
                Console.WriteLine($"{pad}LiteralExpression: {literal.ValueToken.Value}");
                break;

            default:
                Console.WriteLine($"{pad}Unknown Expression: {expr.Kind}");
                break;
        }
    }
}

public static class BoundPrettyPrinter
{
    public static void Print(BoundTree tree)
    {
        foreach (var stmt in tree.Statements)
        {
            PrintStatement(stmt, indent: 0);
        }
    }

    private static void PrintStatement(BoundStatement stmt, int indent)
    {
        var pad = new string(' ', indent * 2);

        switch (stmt)
        {
            case BoundDirectiveStatement dir:
                Console.WriteLine($"{pad}BoundDirectiveStatement:");
                Console.WriteLine($"{pad}  Name: {dir.Symbol.Name}");
                Console.WriteLine($"{pad}  Value:");
                PrintExpression(dir.Value, indent + 2);
                break;

            case BoundExpressionStatement exprStmt:
                Console.WriteLine($"{pad}BoundExpressionStatement:");
                PrintExpression(exprStmt.Expression, indent + 1);
                break;

            default:
                Console.WriteLine($"{pad}Unknown BoundStatement: {stmt.Kind}");
                break;
        }
    }

    private static void PrintExpression(BoundExpression expr, int indent)
    {
        var pad = new string(' ', indent * 2);

        switch (expr)
        {
            case BoundLiteralStringExpression litS:
                Console.WriteLine($"{pad}BoundLiteralStringExpression: {litS.Value}");
                break;
            case BoundLiteralExpression lit:
                Console.WriteLine($"{pad}{lit.Kind}: {lit.Value}");
                break;

            case BoundIdentifierExpression ident:
                Console.WriteLine($"{pad}BoundIdentifierExpression: {ident.Symbol.Name}");
                break;

            default:
                Console.WriteLine($"{pad}Unknown BoundExpression: {expr.Kind}");
                break;
        }
    }
}
