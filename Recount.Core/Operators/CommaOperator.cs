using System;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    internal class CommaOperator : Operator
    {
        public CommaOperator(Symbol symbol)
            : base(symbol)
        {
            Priority = OperatorPriority.Comma;
            Binary = false;
        }

        public override double ExecuteBinary(double firstOperand, double secondOperand)
        {
            throw new NotImplementedException();
        }

        public override double ExecuteUnary(double operand)
        {
            throw new NotImplementedException();
        }
    }
}
