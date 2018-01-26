using Recount.Core.Exceptions;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Variables;

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
            var type = GetSymbolType(value);
            if (type == SymbolType.Undefined)
            {
                throw new SyntaxException(index, value);
            }

            return new Symbol(type, value, index, isLast);
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

            return SymbolType.Undefined;
        }
    }
}