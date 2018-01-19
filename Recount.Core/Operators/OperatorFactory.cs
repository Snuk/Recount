using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    public static class OperatorFactory
    {
        internal static Operator CreateOperator(Symbol symbol, bool binary = true)
        {
            switch (symbol.Value)
            {
                case '(':
                    return new OpeningBracket(symbol);

                case ')':
                    return new ClosingBracket(symbol);

                case '=':
                    return new AssignmentOperator(symbol);

                case '+':
                    return new PlusOperator(symbol, binary);

                case '-':
                    return new MinusOperator(symbol, binary);

                case '*':
                    return new MultiplicationOperator(symbol);

                case '/':
                    return new DivisionOperator(symbol);

                case '^':
                    return new PowerOperator(symbol);

                case ',':
                    return new CommaOperator(symbol);

                default:
                    return null;
            }
        }

        public static bool CheckSymbol(char symbol)
        {
            switch (symbol)
            {
                case '=':
                case '(':
                case ')':
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case ',':
                    return true;

                default:
                    return false;
            }
        }
    }
}
