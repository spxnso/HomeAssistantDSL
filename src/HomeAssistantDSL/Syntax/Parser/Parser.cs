using HomeAssistantDSL.Syntax.Ast;
using HomeAssistantDSL.Syntax.Diagnostics;
using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Parser;

public class Parser
{
    private List<Token> _tokens;
    private int _position = 0;
    private DiagnosticBag Diagnostics = new();
    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    private Token Peek(int pos = 0)
    {
        var idx = pos + _position;
        if (idx >= _tokens.Count()) return _tokens[^1];

        return _tokens[idx];
    }

    private Token Advance()
    {
        if (_position >= _tokens.Count())
        {
            Diagnostics.Add(new Diagnostic(DiagnosticSeverity.Warning, $"Token index {_position} is out of bounds. Returning the last token.", _tokens[^1].Position));
            return _tokens[^1];
        }
        return _tokens[_position++];
    }

    private bool Match(TokenKind kind) => CurrentToken.Kind == kind;


    private Token MatchAdvance(TokenKind kind)
    {
        if (!Match(kind))
        {
            var token = CurrentToken;
            Diagnostics.Add(DiagnosticSeverity.Error, $"Expected token of kind <{kind}>, but found <{token.Kind}> at {token.Position}", token.Position);
        }
        return Advance();
    }
    private Token CurrentToken => Peek();
    private Token NextToken => Peek(1);



    public SyntaxTree Parse()
    {
        var statements = new List<Statement>();

        while (CurrentToken.Kind != TokenKind.Eof)
        {
            statements.Add(ParseStatement());
        }

        var eof = MatchAdvance(TokenKind.Eof);
        return new SyntaxTree(statements, eof, Diagnostics);
    }

    private Statement ParseStatement()
    {
        if (CurrentToken.Kind == TokenKind.Bang)
        {
            var bangToken = Advance();
            var identifierToken = MatchAdvance(TokenKind.Identifier);

            if (!Match(TokenKind.Equals))
                return new DirectiveStatement(bangToken, identifierToken);
                
            var equalsToken = MatchAdvance(TokenKind.Equals);
            var valueExpr = ParseExpression();

            return new DirectiveStatement(bangToken, identifierToken, equalsToken, valueExpr);
        }
        else
        {
            Diagnostics.Add(DiagnosticSeverity.Error, $"Unexpected token <{CurrentToken.Kind}>, expected a statement", CurrentToken.Position);
            return new DummyStatement(Advance());
        }
    }
    private Expression ParseExpression()
    {
        switch (CurrentToken.Kind)
        {
            case TokenKind.Boolean:
            case TokenKind.String:
                {
                    return ParseLiteralExpression();
                }
            case TokenKind.Identifier:
                {
                    var identifierToken = Advance();
                    return new IdentifierExpression(identifierToken);
                }
            default:
                Diagnostics.Add(DiagnosticSeverity.Error, $"Unexpected token <{CurrentToken.Kind}>, expected an expression", CurrentToken.Position);
                return new DummyExpression(Advance());
        }
    }

    private Expression ParseLiteralExpression() {
        switch(CurrentToken.Kind) {
            case TokenKind.String:
                return new LiteralStringExpression(Advance());  
            case TokenKind.Boolean:
                return new LiteralBooleanExpression(Advance());
            default:
                Diagnostics.Add(DiagnosticSeverity.Error, $"Unexpected token <{CurrentToken.Kind}>, expected a literal expression", CurrentToken.Position);
                return new DummyExpression(CurrentToken);
        }
    }

}