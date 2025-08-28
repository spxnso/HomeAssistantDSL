// made using chatgpt, gonna write one later
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
                Console.WriteLine($"{pad}  EqualToken: {directive.EqualsToken.Value}");
                Console.WriteLine($"{pad}  Value:");
                PrintExpression(directive.Value, indent + 2);
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
            case DirectiveExpression directiveExpr:
                Console.WriteLine($"{pad}DirectiveExpression:");
                Console.WriteLine($"{pad}  BangToken: {directiveExpr.BangToken.Value}");
                Console.WriteLine($"{pad}  Identifier: {directiveExpr.NameToken.Value}");
                break;

            case LiteralStringExpression literalStr:
                Console.WriteLine($"{pad}LiteralStringExpression: \"{literalStr.ValueToken.Value}\"");
                break;

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
