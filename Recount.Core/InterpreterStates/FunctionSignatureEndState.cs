using Recount.Core.Contexts;
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

        public override InterpreterState MoveToNextState(Symbol symbol, ILexemesStack stack, ExecutorContext context)
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

                    var function = context._functionsRepository.Get(_functionSignature.Name.Body);
                    var result = FunctionExecutor.Execute(_functionSignature, function, context);

                    stack.Push(new Number(result.Value));
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
    }
}