using Recount.Core.Repositories;

namespace Recount.Core.Contexts
{
    public class ExecutorContext
    {
        public readonly IVariablesRepository _variablesRepository;
        public readonly IFunctionsRepository _functionsRepository;

        public ExecutorContext(IVariablesRepository variablesRepository, IFunctionsRepository functionsRepository)
        {
            _variablesRepository = variablesRepository;
            _functionsRepository = functionsRepository;
        }
    }
}
