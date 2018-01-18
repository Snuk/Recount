namespace Recount.Core.Symbols
{
    public class Symbol
    {
        public char Value { get; }

        public int Index { get; }

        public SymbolType Type { get; }

        public bool IsLast { get; set; }

        public Symbol(SymbolType type, char value, int index, bool isLast = false)
        {
            Type = type;
            Value = value;
            Index = index;
            IsLast = isLast;
        }
    }
}
