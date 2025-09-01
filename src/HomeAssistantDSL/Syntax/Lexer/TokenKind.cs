namespace HomeAssistantDSL.Syntax.Lexer;

public enum TokenKind
{
    Eof,
    Identifier,
    String,
    Bang,
    Equals,

    // Errors
    Unknown,
    BadToken,
    Boolean,
}
