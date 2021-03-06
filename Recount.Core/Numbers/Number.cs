﻿using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.Numbers
{
    public class Number : Lexeme
    {
        public double Value { get; set; }

#warning sefsgrsdgrgd
        public Number(double value)
            : base(new LexemeBuilder(new Symbol(SymbolType.Number, '@', 0)))
        {
            Value = value;
        }

        public Number(LexemeBuilder builder, double value)
            : base(builder)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString(NumberFactory.NumberFormatInfo);
        }
    }
}
