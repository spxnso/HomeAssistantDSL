using HomeAssistantDSL.Syntax.Ast;
using HomeAssistantDSL.Syntax.Lexer;
using HomeAssistantDSL.Syntax.Parser;

var input = File.ReadAllText($"./input.ha");

var lexer =  new Lexer(input);

var tokens = lexer.Lex();

var parser = new Parser(tokens);

var tree = parser.Parse();


if (tree.Diagnostics.Any()) {
    foreach(var diagnostic in tree.Diagnostics) {
        Console.WriteLine(diagnostic);
    }
}

PrettyPrinter.Print(tree);