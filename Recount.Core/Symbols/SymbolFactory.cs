using System;
using Recount.Core.Identifiers;
using Recount.Core.Numbers;
using Recount.Core.Operators;

namespace Recount.Core.Symbols
{
    public static class SymbolFactory
    {
        public static Symbol CreateSymbol(string expression, int index)
        {
            var isLast = index == expression.Length - 1;
            return CreateSymbol(expression[index], index, isLast);
        }

        public static Symbol CreateSymbol(char value, int index, bool isLast = false)
        {
            return new Symbol(GetSymbolType(value), value, index, isLast);
        }

        private static SymbolType GetSymbolType(char value)
        {
            if (NumberFactory.CheckSymbol(value))
            {
                return SymbolType.Number;
            }

            if (OperatorFactory.CheckSymbol(value))
            {
                return SymbolType.Operator;
            }

            if (VariableFactory.CheckSymbol(value))
            {
                return SymbolType.Identifier;
            }

            throw new Exception();
        }
    }
}