using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class BinaryOperatorState : AnalyserState
    {
        public override void Execute(ILexemesStack stack)
        {
            stack.PopOperators();
        }

        public override AnalyserState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                    return new NumberReadingState(symbol);

                case SymbolType.Identifier:
                    return new IdentifierReadingState(symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol, true);
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