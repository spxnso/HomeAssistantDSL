namespace HomeAssistantDSL.Semantics.BoundAst;

public enum BoundNodeKind {
    BoundLiteralExpression,
    BoundIdentifierExpression,
    BoundDirectiveStatement,
    BoundExpressionStatement,
    BoundLiteralStringExpression,
    BoundLiteralBooleanExpression,
    BoundDummyExpression,
    BoundDummyStatement,
} 
