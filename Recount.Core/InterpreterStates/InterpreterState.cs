using Recount.Core.Contexts;
using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public abstract class InterpreterState
    {
        public static InterpreterState StartFromInitialState(ILexemesStack stack)
        {
            var openingBracket = new OpeningBracket(SymbolFactory.CreateSymbol('(', -1));
            stack.Push(openingBracket);

            return new OpeningBracketOperatorState();
        }

        public InterpreterState MoveToFinalState(int lastSymbolIndex, ILexemesStack stack, ExecutorContext context)
        {
            var lastSymbol = SymbolFactory.CreateSymbol(')', lastSymbolIndex);
            return MoveToNextState(lastSymbol, stack, context);
        }

        public abstract InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack, ExecutorContext context);

        public virtual void Execute(ILexemesStack stack, ExecutorContext context)
        {
        }
    }
}
