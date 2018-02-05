using Recount.Core.Contexts;
using Recount.Core.Functions;

namespace Recount.Core
{
    public static class FunctionExecutor
    {
        public static double? Execute(FunctionSignature signature, Function function, ExecutorContext context)
        {
            var localContext = ExecutorContextFactory.CreateLocalContext();

            for (var index = 0; index < signature.Arguments.Count; index++)
            {
                var argument = signature.Arguments[index];
                var parameter = function.Parameters[index];

                var argumentCalculator = new Interpreter(context);
                var argumentValue = argumentCalculator.Execute(argument.Body);

                localContext._variablesRepository.Add(parameter, argumentValue.Value);
            }

            var bodyCalculator = new Interpreter(localContext);
            var functionValue = bodyCalculator.Execute(function.Body);

            return functionValue.Value;
        }
    }
}
