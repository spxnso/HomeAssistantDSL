using System.Text;
using HomeAssistantDSL.Syntax.Diagnostics;

namespace HomeAssistantDSL.Syntax.Lexer;

public class Lexer
{
    private string _source;
    private int _offset = 0;
    private int _line = 1;
    private int _column = 1;
    
    public DiagnosticBag Diagnostics = new();

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

        char c = _source[_offset++];
        if (c == '\n') {
            _line++;
            _column = 1;
        } else {
            _column++;
        }

        return c;
    }
    private string AdvanceWhile(Func<char, bool> predicate)
    {
        StringBuilder sb = new();

        while (!AtEof())
        {
            char c = CurrentChar;
            if (!predicate(c)) break;
            sb.Append(Advance());
        }
        return sb.ToString();
    }

    private TokenPosition CurrentPosition => new TokenPosition(_offset, _line, _column);
    private char CurrentChar => Peek();
    private char NextChar => Peek(1);

    private Token ReadIdentifier()
    {
        TokenPosition _tokenPosition = CurrentPosition;
        StringBuilder _sb = new();


        _sb.Append(Advance());
        _sb.Append(AdvanceWhile(c => (char.IsLetterOrDigit(CurrentChar) || CurrentChar == '_') && !AtEof()));


        TokenKind kind;


        switch(_sb.ToString()) {
            case "true":
            case "false":
                kind = TokenKind.Boolean;
                break;
            
            default:    
                kind = TokenKind.Identifier;
                break;
        }

        return new Token(kind, _sb.ToString(), _tokenPosition);
    }

    private char EscapeChar()
    {
        switch (CurrentChar)
        {
            case 'n':
                Advance();
                return '\n';
            case 'r':
                Advance();
                if (CurrentChar == 'n')
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
                var unknown = CurrentChar;
                Advance();
                Diagnostics.Add(new Diagnostic(DiagnosticSeverity.Error, $"Unknown escape sequence <\\{unknown}>.", CurrentPosition));
                return unknown;
        }
    }
    private Token ReadString()
    {
        TokenPosition _tokenPosition = CurrentPosition;
        StringBuilder _sb = new();

        char quote = Advance();

        while (!AtEof())
        {
            if (CurrentChar == quote)
            {
                Advance();
                return new Token(TokenKind.String, _sb.ToString(), CurrentPosition);
            }

            switch (CurrentChar)
            {

                case '\r':
                case '\n':
                    _sb.Append(CurrentChar);
                    Advance();
                    CurrentPosition.Line++;
                    CurrentPosition.Column = 0;
                    break;

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

        Diagnostics.Add(new Diagnostic(DiagnosticSeverity.Error, $"Unclosed string literal. Missing closing {quote}.", _tokenPosition));
        return new Token(TokenKind.BadToken, _sb.ToString(), _tokenPosition);
    }

    public Token ReadNext()
    {
        AdvanceWhile(char.IsWhiteSpace);
        TokenPosition _tokenPosition = CurrentPosition;

        if (AtEof()) return new Token(TokenKind.Eof, string.Empty, _tokenPosition);

        if (char.IsLetter(CurrentChar))
        {
            return ReadIdentifier();
        }

        switch (CurrentChar)
        {
            case '!':
                return new Token(TokenKind.Bang, Advance().ToString(), _tokenPosition);
            case '=':
                return new Token(TokenKind.Equals, Advance().ToString(), _tokenPosition);
            case '"':
            case '\'':
                return ReadString();
            default:
                Diagnostics.Add(DiagnosticSeverity.Warning, $"Unexpected character <{CurrentChar}>", _tokenPosition);
                return new Token(TokenKind.Unknown, Advance().ToString(), _tokenPosition);
        }
    }


    public List<Token> Lex()
    {
        List<Token> _tokens = new();


        while (!AtEof())
        {
            _tokens.Add(ReadNext());
        }

        _tokens.Add(new Token(TokenKind.Eof, string.Empty, CurrentPosition));

        return _tokens;
    }
}