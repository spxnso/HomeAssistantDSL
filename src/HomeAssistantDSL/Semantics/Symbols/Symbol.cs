namespace HomeAssistantDSL.Semantics.Symbols;
public sealed class Symbol
{
    public Symbol(string name, SymbolKind kind, Type? type = null, object? value = null)
    {
        Name = name;
        Kind = kind;
        Type = type;
        Value = value;
    }
    public string Name { get; }

    public SymbolKind Kind { get; }
    public Type? Type { get; }

    public object? Value { get; }
}
