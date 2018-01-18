﻿using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class ClosingBracketOperatorState : AnalyserState
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
                case SymbolType.Identifier:
                    return new ErrorState(symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol, false);
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