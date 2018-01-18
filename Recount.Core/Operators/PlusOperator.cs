using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// Оператор +
    /// </summary>
    public class PlusOperator : Operator
    {
        public PlusOperator(Symbol symbol, bool binary)
            : base(symbol)
        {
            Binary = binary;
            Priority = Binary ? OperatorPriority.BinaryPlusMinus : OperatorPriority.UnaryPlusMinus;
        }

        public override double ExecuteBinary(double secondOperand, double firstOperand)
        {
            return firstOperand + secondOperand;
        }

        public override double ExecuteUnary(double operand)
        {
            return operand;
        }
    }
}
