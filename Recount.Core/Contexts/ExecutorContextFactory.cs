using Recount.Core.Lexemes;
using Recount.Core.Repositories;

namespace Recount.Core.Contexts
{
    public class ExecutorContextFactory
    {
        private readonly IVariablesRepository _variablesRepository;
        private readonly IFunctionsRepository _functionsRepository;

        public ExecutorContextFactory(IVariablesRepository variablesRepository, IFunctionsRepository functionsRepository)
        {
            _variablesRepository = variablesRepository;
            _functionsRepository = functionsRepository;
        }

        public static ExecutorContext CreateLocalContext()
        {
            return new ExecutorContext(new VariablesMemoryRepository(), new FunctionsMemoryRepository());
        }

        public ExecutorContext CreateGlobalContext()
        {
            return new ExecutorContext(_variablesRepository, _functionsRepository);
        }
    }
}
