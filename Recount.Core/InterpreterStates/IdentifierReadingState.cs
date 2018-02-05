using Recount.Core.Contexts;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Symbols;
using Recount.Core.Variables;

namespace Recount.Core.InterpreterStates
{
    public class IdentifierReadingState : InterpreterState
    {
        private readonly LexemeBuilder _variableBuilder;

        public IdentifierReadingState(Symbol symbol)
        {
            _variableBuilder = new LexemeBuilder(symbol);
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack, ExecutorContext context)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                case SymbolType.Identifier:
                    if (symbol.Value.Equals(NumberFactory.DecimalSeparator))
                    {
                        return new ErrorState(symbol);
                    }

                    _variableBuilder.Append(symbol);
                    return this;

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol);
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