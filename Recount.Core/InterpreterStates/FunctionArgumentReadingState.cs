using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public class FunctionArgumentReadingState : InterpreterState
    {
        private readonly FunctionSignature _functionSignature;
        private readonly LexemeBuilder _argumentBuilder;
        private int _bracketsBalance;

        public FunctionArgumentReadingState(FunctionSignature functionSignature, Symbol symbol)
        {
            _functionSignature = functionSignature;
            _bracketsBalance = 1;
            _argumentBuilder = new LexemeBuilder(symbol);
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                case SymbolType.Identifier:
                    _argumentBuilder.Append(symbol);
                    return this;

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol);

                    switch (@operator)
                    {
                        case OpeningBracket _:
                            _bracketsBalance++;
                            _argumentBuilder.Append(symbol);
                            return this;

                        case ClosingBracket _:
                            _bracketsBalance--;
                            if (_bracketsBalance == 0)
                            {
                                _functionSignature.AppendArgument(_argumentBuilder);
                                return new FunctionSignatureEndState(_functionSignature);
                            }

                            _argumentBuilder.Append(symbol);
                            return this;

                        case CommaOperator _:
                            if (_bracketsBalance != 1)
                            {
                                return new ErrorState(symbol);
                            }

                            if (_argumentBuilder.IsEmpty)
                            {
                                return new ErrorState(symbol);
                            }

                            _functionSignature.AppendArgument(_argumentBuilder);
                            return new FunctionArgumentReadingState(_functionSignature, null);

                        default:
                            _argumentBuilder.Append(symbol);
                            return this;
                    }

                default:
                    return new ErrorState(symbol);
            }
        }
    }
}