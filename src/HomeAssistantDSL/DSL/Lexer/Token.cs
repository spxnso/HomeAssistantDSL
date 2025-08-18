namespace HomeAssistantDSL.DSL.Lexer;

public class Token {
    public Token(TokenKind kind, string lexeme, int offset)
    {
        Kind = kind;
        Lexeme = lexeme;
        Offset = offset;
    }

    public TokenKind Kind { get; }
    public string Lexeme { get; }
    public int Offset { get; }
}