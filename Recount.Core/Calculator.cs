using Recount.Core.AnalyserStates;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Symbols;

namespace Recount.Core
{
    public class Calculator
    {
        private readonly ILexemesStack _stack;

        public Calculator(ILexemesStack stack)
        {
            _stack = stack;
        }

        public Number Calculate(string expression)
        {
            var state = AnalyserState.StartFromInitialState(_stack);

            for (var index = 0; index < expression.Length; index++)
            {
                var symbol = SymbolFactory.CreateSymbol(expression, index);

                state = state.MoveToNextState(symbol, _stack);
                state.Execute(_stack);
            }

            state = state.MoveToFinalState(expression.Length, _stack);
            state.Execute(_stack);
            return _stack.GetResult();
        }
    }
}
