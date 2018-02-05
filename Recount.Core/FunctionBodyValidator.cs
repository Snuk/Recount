using Recount.Core.Contexts;
using Recount.Core.Functions;
using Recount.Core.InterpreterStates;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core
{
    public class FunctionBodyValidator
    {
        private InterpreterState _validatorState;
        private readonly ValidatorLexemesStack _validatorStack;
        private readonly ExecutorContext _localContext;

        public FunctionBodyValidator(Function function)
        {
            _localContext = ExecutorContextFactory.CreateLocalContext();
            foreach (var variable in function.Parameters)
            {
                _localContext._variablesRepository.Add(variable, 0);
            }

            _validatorStack = new ValidatorLexemesStack();
            _validatorState = new OpeningBracketOperatorState();
        }

        public InterpreterState AppendSymbol(Symbol symbol)
        {
            _validatorState = _validatorState.MoveToNextState(symbol, _validatorStack, _localContext);
            _validatorState.Execute(_validatorStack, _localContext);

            if (_validatorState is FunctionSignatureStartState || _validatorState is AssignmentOperatorState)
            {
                return new ErrorState(symbol);
            }

            return _validatorState;
        }

        public bool IsValid()
        {
            return _validatorStack.IsValid();
        }
    }
}