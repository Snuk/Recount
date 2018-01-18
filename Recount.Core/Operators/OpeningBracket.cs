using System;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// открывающая скобка
    /// </summary>
    internal class OpeningBracket : Operator
    {
        public OpeningBracket(Symbol symbol)
            : base(symbol)
        {
            Priority = OperatorPriority.OpeningBracket;
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
