using System;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public class ErrorState : InterpreterState
    {
        private readonly Symbol _errorSymbol;

        public ErrorState(Symbol errorSymbol)
        {
            _errorSymbol = errorSymbol;
        }

        public override void Execute(ILexemesStack stack)
        {
            throw new Exception($"syntax error, symbol {_errorSymbol.Index}");
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            throw new NotImplementedException();
        }
    }
}
