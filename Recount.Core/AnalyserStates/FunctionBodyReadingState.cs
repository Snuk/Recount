using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class FunctionBodyReadingState : AnalyserState
    {
        private readonly Function _function;
        private AnalyserState _validatorState;
        private readonly FunctionValidatorLexemesStack _validatorStack;
        private readonly LexemeBuilder _functionBodyBuilder;

        public FunctionBodyReadingState(Function function)
        {
            _function = function;
            _validatorStack = new FunctionValidatorLexemesStack(_function.Parameters);
            _validatorState = new OpeningBracketOperatorState();
            _functionBodyBuilder = new LexemeBuilder();
        }

        public override AnalyserState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            _validatorState = _validatorState.MoveToNextState(symbol, _validatorStack);
            _validatorState.Execute(_validatorStack);

            if (_validatorState is FunctionSignatureStartState)
            {
                return new ErrorState(symbol);
            }

            _functionBodyBuilder.Append(symbol);

            if (symbol.IsLast)
            {
                if (_validatorStack.IsValid())
                {
                    _function.Body = _functionBodyBuilder.Body;
                    stack.AddFunction(_function);
                    return new ClosingBracketOperatorState();
                }
                else
                {
                    return new ErrorState(symbol);
                }
            }
            
            return this;
        }
    }
}