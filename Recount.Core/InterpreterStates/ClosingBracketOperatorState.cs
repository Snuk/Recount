using Recount.Core.Contexts;
using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public class ClosingBracketOperatorState : InterpreterState
    {
        public override void Execute(ILexemesStack stack, ExecutorContext context)
        {
            stack.PopOperators(context);
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack, ExecutorContext context)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                case SymbolType.Identifier:
                    return new ErrorState(symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol);
                    stack.Push(@operator);

                    switch (@operator)
                    {
                        case ClosingBracket _:
                            return new ClosingBracketOperatorState();
                        case CommaOperator _:
                        case OpeningBracket _:
                        case AssignmentOperator _:
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