using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public class UnaryOperatorState : InterpreterState
    {
        public override void Execute(ILexemesStack stack)
        {
            stack.PopOperators();
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                    return new NumberReadingState(symbol);

                case SymbolType.Identifier:
                    return new IdentifierReadingState(symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol);
                    stack.Push(@operator);

                    switch (@operator)
                    {
                        case OpeningBracket _:
                            return new OpeningBracketOperatorState();
                        default:
                            return new ErrorState(symbol);
                    }

                default:
                    return new ErrorState(symbol);
            }
        }
    }
}