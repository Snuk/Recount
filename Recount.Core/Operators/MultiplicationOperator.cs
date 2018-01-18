using System;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// Оператор *
    /// </summary>
    internal class MultiplicationOperator : Operator
    {
        public MultiplicationOperator(Symbol symbol)
            : base(symbol)
        {
            Priority = OperatorPriority.MultiplicationDivision;
        }

        public override double ExecuteBinary(double secondOperand, double firstOperand)
        {
            return firstOperand * secondOperand;
        }

        public override double ExecuteUnary(double operand)
        {
            throw new NotImplementedException();
        }
    }
}
