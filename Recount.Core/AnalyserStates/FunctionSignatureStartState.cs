using Recount.Core.Functions;
using Recount.Core.Identifiers;
using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class FunctionSignatureStartState : AnalyserState
    {
        private readonly FunctionSignature _functionSignature;

        public FunctionSignatureStartState(Variable functionName)
        {
            _functionSignature = new FunctionSignature(functionName);
        }

        public override AnalyserState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                case SymbolType.Identifier:
                    return new FunctionArgumentReadingState(_functionSignature, symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol, false);

                    switch (@operator)
                    {
                        case OpeningBracket _:
                        case PlusOperator _:
                        case MinusOperator _:
                            return new FunctionArgumentReadingState(_functionSignature, symbol);
                        default:
                            return new ErrorState(symbol);
                    }

                default:
                    return new ErrorState(symbol);
            }
        }
    }
}