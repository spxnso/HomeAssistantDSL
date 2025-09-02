using HomeAssistantDSL.Syntax.Lexer;

namespace HomeAssistantDSL.Syntax.Ast;

public sealed class EntityDeclarationStatement : Statement {
    public override SyntaxKind Kind => SyntaxKind.EntityDeclarationStatement;

    public Token EntityKeyword { get; }
    public Token NameToken { get; }
    public Token? TypeKeyword { get; }
    public Token TypeToken { get; }

    public EntityDeclarationStatement(Token entityKeyword, Token name, Token type, Token? typeKeyword = null)
    {
        EntityKeyword = entityKeyword;
        NameToken = name;
        TypeKeyword = typeKeyword;
        TypeToken = type;
    }
}