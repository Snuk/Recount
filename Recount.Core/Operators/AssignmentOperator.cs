using System;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// Оператор =
    /// </summary>
    internal class AssignmentOperator : Operator
    {
        public AssignmentOperator(Symbol symbol)
            : base(symbol)
        {
            Priority = OperatorPriority.Assignment;
            Binary = false;
        }

        public override double ExecuteBinary(double firstOperand, double secondOperand)
        {
            throw new NotImplementedException();
        }

        public override double ExecuteUnary(double operand)
        {
            return operand;
        }
    }
}
