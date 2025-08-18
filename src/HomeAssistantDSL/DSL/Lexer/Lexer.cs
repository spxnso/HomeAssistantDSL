using System.Text;

namespace HomeAssistantDSL.DSL.Lexer;

public class Lexer
{
    private string _source;
    private int _offset = 0;
    public Lexer(string source)
    {
        _source = source;
    }

    private bool AtEof(int offset = 0)
    {
        return _offset + offset >= _source.Length;
    }

    private char Peek(int offset = 0)
    {
        if (AtEof(offset)) return '\0';
        return _source[_offset + offset];
    }

    private char Advance()
    {
        if (AtEof()) return '\0';
        return _source[_offset++]; ;
    }
    private string AdvanceWhile(Func<char, bool> predicate)
    {
        StringBuilder sb = new();

        while (!AtEof())
        {
            char c = Current;
            if (!predicate(c)) break;
            sb.Append(Advance());
        }
        return sb.ToString();
    }

    private char Current => _source[_offset];
    private char Next => _source[_offset + 1];

    private Token ReadIdentifier()
    {
        int _tokenOffset = _offset;
        StringBuilder _sb = new();


        _sb.Append(Advance());
        _sb.Append(AdvanceWhile(c => char.IsLetterOrDigit(Current) && !AtEof()));

        return new Token(TokenKind.Identifier, _sb.ToString(), _tokenOffset);
    }

    private char EscapeChar()
    {
        switch (Current)
        {
            case 'n':
                Advance();
                return '\n';
            case 'r':
                Advance();
                if (Current == 'n')
                {
                    Advance();
                    return '\n';
                }
                return '\r';
            case 't':
                Advance();
                return '\t';
            case '\\':
                Advance();
                return '\\';
            case '"':
                Advance();
                return '"';

            case '\'':
                Advance();
                return '\'';
            
            case '0':   
                return '\0';

            default:
                throw new NotSupportedException($"Unknown escape sequence '\\{Current}' at offset {_offset}");
        }
    }
    private Token ReadString()
    {
        int _tokenOffset = _offset;
        StringBuilder _sb = new();

        char quote = Advance();

        while (!AtEof())
        {
            if (Current == quote)
            {
                Advance();
                return new Token(TokenKind.String, _sb.ToString(), _tokenOffset);
            }

            switch (Current)
            {
                case '\0':
                case '\r':
                case '\n':
                    throw new Exception($"Unexpected character <{Current}> in string starting at offset {_tokenOffset}");

                case '\\':
                    Advance();
                    char escaped = EscapeChar();
                    _sb.Append(escaped);
                    break;
                
                default:
                    _sb.Append(Advance());
                    break;
            }
        }

        throw new Exception($"Unclosed string literal starting at offset {_tokenOffset}." + $"Missing closing {quote}");
    }

    public Token ReadNext()
    {
        AdvanceWhile(char.IsWhiteSpace);
        int _tokenOffset = _offset;

        if (AtEof()) return new Token(TokenKind.Eof, string.Empty, _tokenOffset);

        if (char.IsLetter(Current))
        {
            return ReadIdentifier();
        }

        switch (Current)
        {
            case '!':
                return new Token(TokenKind.Bang, Advance().ToString(), _tokenOffset);
            case '=':
                return new Token(TokenKind.Equals, Advance().ToString(), _tokenOffset);
            case '"':
            case '\'':
                return ReadString();
            default:
                return new Token(TokenKind.Unknown, Advance().ToString(), _tokenOffset);
        }
    }


    public List<Token> Lex()
    {
        List<Token> _tokens = new();
        while (!AtEof())
        {
            _tokens.Add(ReadNext());
        }

        return _tokens;
    }
}