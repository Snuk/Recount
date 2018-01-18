using System;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// закрывающая скобка
    /// </summary>
    internal class ClosingBracket : Operator
    {
        public ClosingBracket(Symbol symbol)
            : base(symbol)
        {
            Priority = OperatorPriority.ClosingBracket;
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
