using HomeAssistantDSL.DSL.Lexer;

var input = File.ReadAllText($"./input.ha");

var lexer =  new Lexer(input);

foreach (var token in lexer.Lex()) {
    Console.WriteLine($"[{token.Kind} '{token.Lexeme}' at {token.Offset}]");
}