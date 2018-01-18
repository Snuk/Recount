using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class NumberReadingState : AnalyserState
    {
        private readonly LexemeBuilder _numberBuilder;

        public NumberReadingState(Symbol symbol)
        {
            _numberBuilder = new LexemeBuilder(symbol);
        }

        public override AnalyserState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                    _numberBuilder.Append(symbol);
                    return this;

                case SymbolType.Identifier:
                    return new ErrorState(symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol, true);
                    var number = NumberFactory.CreateNumber(_numberBuilder);

                    stack.Push(number);
                    stack.Push(@operator);

                    switch (@operator)
                    {
                        case OpeningBracket _:
                        case AssignmentOperator _:
                        case CommaOperator _:
                            return new ErrorState(symbol);
                        case ClosingBracket _:
                            return new ClosingBracketOperatorState();
                        default:
                            return new BinaryOperatorState();
                    }

                default:
                    return new ErrorState(symbol);
            }
        }
    }
}