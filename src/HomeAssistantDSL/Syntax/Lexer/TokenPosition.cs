namespace HomeAssistantDSL.Syntax.Lexer;

public class TokenPosition
{

    public TokenPosition(int offset, int line, int column)
    {
        Offset = offset;
        Line = line;
        Column = column;
    }
    public int Offset;
    public int Line;
    public int Column;

    public override string ToString()
    {
        return $"Line {Line}, Col {Column} (Offset {Offset})";
    }
}