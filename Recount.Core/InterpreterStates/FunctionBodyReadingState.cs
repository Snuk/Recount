using Recount.Core.Contexts;
using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public class FunctionBodyReadingState : InterpreterState
    {
        private readonly Function _function;
        private readonly LexemeBuilder _functionBodyBuilder;
        private readonly FunctionBodyValidator _functionBodyValidator;

        public FunctionBodyReadingState(Function function)
        {
            _function = function;
            _functionBodyBuilder = new LexemeBuilder();
            _functionBodyValidator = new FunctionBodyValidator(function);
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack, ExecutorContext context)
        {
            var validatorState = _functionBodyValidator.AppendSymbol(symbol);
            if (validatorState is ErrorState)
            {
                return validatorState;
            }

            _functionBodyBuilder.Append(symbol);

            if (symbol.IsLast)
            {
                if (_functionBodyValidator.IsValid())
                {
                    _function.Body = _functionBodyBuilder.Body;
                    context._functionsRepository.Add(_function);
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