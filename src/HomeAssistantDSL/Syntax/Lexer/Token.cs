namespace HomeAssistantDSL.Syntax.Lexer;

public class Token {
    public Token(TokenKind kind, string value, TokenPosition position)
    {
        Kind = kind;
        Value = value;
        Position = position;
    }


    public TokenKind Kind { get; }
    public string Value { get; }
    public TokenPosition Position;
}
