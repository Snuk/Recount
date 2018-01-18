using System;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class ErrorState : AnalyserState
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

        public override AnalyserState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            throw new NotImplementedException();
        }
    }
}
