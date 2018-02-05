using Recount.Core.Contexts;
using Recount.Core.InterpreterStates;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core
{
    public class Interpreter
    {
        private readonly ILexemesStack _stack;
        private readonly ExecutorContext _context;

        public Interpreter(ExecutorContext context)
        {
            _stack = new CalculationLexemesStack();
            _context = context;
        }

        public double? Execute(string expression)
        {
            var state = InterpreterState.StartFromInitialState(_stack);

            for (var index = 0; index < expression.Length; index++)
            {
                var symbol = SymbolFactory.CreateSymbol(expression, index);

                state = state.MoveToNextState(symbol, _stack, _context);
                state.Execute(_stack, _context);
            }

            state = state.MoveToFinalState(expression.Length, _stack, _context);
            state.Execute(_stack, _context);
            return _stack.GetResult(_context);
        }
    }
}
