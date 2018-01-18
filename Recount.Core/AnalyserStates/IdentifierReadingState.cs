using Recount.Core.Identifiers;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class IdentifierReadingState : AnalyserState
    {
        private readonly LexemeBuilder _variableBuilder;

        public IdentifierReadingState(Symbol symbol)
        {
            _variableBuilder = new LexemeBuilder(symbol);
        }

        public override AnalyserState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                case SymbolType.Identifier:
                    if (symbol.Value.Equals(NumberFactory.Dividor))
                    {
                        return new ErrorState(symbol);
                    }

                    _variableBuilder.Append(symbol);
                    return this;

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol, true);
                    var identifier = VariableFactory.CreateVariable(_variableBuilder);

                    if (@operator is OpeningBracket)
                    {
                        return new FunctionSignatureStartState(identifier);
                    }
                    
                    stack.Push(identifier);
                    stack.Push(@operator);

                    switch (@operator)
                    {
                        case AssignmentOperator _:
                            return new AssignmentOperatorState();
                        case ClosingBracket _:
                            return new ClosingBracketOperatorState();
                        case CommaOperator _:
                            return new ErrorState(symbol);
                        default:
                            return new BinaryOperatorState();
                    }

                default:
                    return new ErrorState(symbol);
            }
        }
    }
}