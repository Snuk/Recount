using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Symbols;

namespace Recount.Core.AnalyserStates
{
    public class FunctionSignatureEndState : AnalyserState
    {
        private readonly FunctionSignature _functionSignature;

        public FunctionSignatureEndState(FunctionSignature functionSignature)
        {
            _functionSignature = functionSignature;
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

                    var function = stack.GetFunction(_functionSignature.Name);
                    var result = EvaluateFunction(_functionSignature, function);

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

        private static Number EvaluateFunction(FunctionSignature signature, Function function)
        {
            var bodyCalculationStack = new CalculationLexemesStack();

            for (var index = 0; index < signature.Arguments.Count; index++)
            {
                var argument = signature.Arguments[index];
                var parameter = function.Parameters[index];

                var argumentCalculator = new Calculator(new CalculationLexemesStack());
                var argumentValue = argumentCalculator.Calculate(argument.Body);

                bodyCalculationStack.AddVariable(parameter, argumentValue);
            }

            var bodyCalculator = new Calculator(bodyCalculationStack);
            var functionValue = bodyCalculator.Calculate(function.Body);

            return functionValue;
        }
    }
}