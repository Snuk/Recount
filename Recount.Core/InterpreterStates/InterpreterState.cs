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

        public InterpreterState MoveToFinalState(int expressionLength, ILexemesStack stack)
        {
            var lastSymbol = SymbolFactory.CreateSymbol(')', expressionLength);
            return MoveToNextState(lastSymbol, stack);
        }

        public abstract InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack);

        public virtual void Execute(ILexemesStack stack)
        {
        }
    }
}
