using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.InterpreterStates
{
    public class FunctionSignatureEndState : InterpreterState
    {
        private readonly FunctionSignature _functionSignature;

        public FunctionSignatureEndState(FunctionSignature functionSignature)
        {
            _functionSignature = functionSignature;
        }

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack)
        {
            switch (symbol.Type)
            {
                case SymbolType.Number:
                case SymbolType.Identifier:
                    return new ErrorState(symbol);

                case SymbolType.Operator:
                    var @operator = OperatorFactory.CreateOperator(symbol);
                    if (@operator is OpeningBracket || @operator is CommaOperator)
                    {
                        return new ErrorState(symbol);
                    }

                    if (@operator is AssignmentOperator)
                    {
                        if (_functionSignature.IsValidFunctionDeclaration())
                        {
                            return new FunctionBodyReadingState(_functionSignature.ConvertToFunction());
                        }

                        return new ErrorState(symbol);
                    }

                    var function = stack.GetFunction(_functionSignature.Name.Body);
                    var result = EvaluateFunction(_functionSignature, function, stack);

                    stack.Push(result);
                    stack.Push(@operator);

                    if (@operator is ClosingBracket)
                    {
                        return new ClosingBracketOperatorState();
                    }

                    return new BinaryOperatorState();

                default:
                    return new ErrorState(symbol);
            }
        }

        private static Number EvaluateFunction(FunctionSignature signature, Function function, ILexemesStack stack)
        {
            var bodyCalculationStack = new CalculationLexemesStack(new MemoryVariablesProvider(), new MemoryFunctionsProvider());

            for (var index = 0; index < signature.Arguments.Count; index++)
            {
                var argument = signature.Arguments[index];
                var parameter = function.Parameters[index];

                var argumentCalculatorStack = stack.Copy();
                var argumentCalculator = new Interpreter(argumentCalculatorStack);
                var argumentValue = argumentCalculator.Execute(argument.Body);

                bodyCalculationStack.AddVariable(parameter, argumentValue.Value);
            }

            var bodyCalculator = new Interpreter(bodyCalculationStack);
            var functionValue = bodyCalculator.Execute(function.Body);

            return new Number(functionValue.Value);
        }
    }
}