using System.Reflection;
using HomeAssistantDSL.Semantics.Binder;
using HomeAssistantDSL.Syntax.Lexer;
using HomeAssistantDSL.Syntax.Parser;

var input = File.ReadAllText($"./input.ha");

var lexer =  new Lexer(input);

var tokens = lexer.Lex();

foreach (var token in tokens) {
    Console.WriteLine($"{token.Kind}({token.Value})");
}
Console.WriteLine("------------------");
var parser = new Parser(tokens);

var tree = parser.Parse();




PrettyPrinter.Print(tree);
Console.WriteLine("------------------");
var binder = new HomeAssistantDSL.Semantics.Binder.Binder();

var boundTree = binder.Bind(tree);



BoundPrettyPrinter.Print(boundTree);