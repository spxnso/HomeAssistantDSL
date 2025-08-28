namespace HomeAssistantDSL.Syntax.Lexer;

public enum TokenKind
{
    Eof,
    Identifier,
    String,
    // Operators
    Bang,
    Equals,

    // Errors
    Unknown,
    BadToken,
}
