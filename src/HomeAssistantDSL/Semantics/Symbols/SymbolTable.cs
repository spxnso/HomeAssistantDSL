namespace HomeAssistantDSL.Semantics.Symbols;
public sealed class SymbolTable
{
    private readonly Dictionary<string, Symbol> _symbols = new();
    public SymbolTable()
    {
        // TOKEN
        Add(new Symbol("TOKEN", SymbolKind.Directive, typeof(string)));
        Add(new Symbol("BEARER", SymbolKind.Directive, typeof(string)));
        Add(new Symbol("HA_BEARER", SymbolKind.Directive, typeof(string)));

        // URL
        Add(new Symbol("URL", SymbolKind.Directive, typeof(string)));
        Add(new Symbol("HA_URL", SymbolKind.Directive, typeof(string)));
    }

    public void Add(Symbol symbol)
    {
        if (_symbols.ContainsKey(symbol.Name))
            throw new Exception($"Symbol {symbol.Name} is already defined");

        _symbols[symbol.Name] = symbol;
    }

    public bool TryLookup(string name, out Symbol symbol)
    {
        return _symbols.TryGetValue(name, out symbol!);
    }

    public Symbol LookupOrThrow(string name)
    {
        if (!_symbols.TryGetValue(name, out var symbol))
            throw new Exception($"Unknown symbol: {name}");
        return symbol;
    }

    public IEnumerable<Symbol> Symbols => _symbols.Values;  
}