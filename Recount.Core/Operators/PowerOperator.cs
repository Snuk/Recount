using System;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// Оператор ^
    /// </summary>
    public class PowerOperator : Operator
    {
        public PowerOperator(Symbol symbol)
            : base(symbol)
        {
            Priority = OperatorPriority.Power;
            Association = OperatorAssociation.Right;
        }

        public override double ExecuteBinary(double secondOperand, double firstOperand)
        {
            return Math.Pow(firstOperand, secondOperand);
        }

        public override double ExecuteUnary(double operand)
        {
            throw new NotImplementedException();
        }
    }
}
