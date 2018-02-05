using Microsoft.AspNetCore.Mvc;
using Recount.Core;
using Recount.Core.Contexts;
using Recount.DataAccess.Repositories;

namespace Recount.Api.Controllers
{
    [Route("api/[controller]")]
    public class InterpreterController : Controller
    {
        private readonly FunctionsMongoRepository _functionsRepository;
        private readonly VariablesMongoRepository _variablesRepository;

        public InterpreterController(FunctionsMongoRepository repository, VariablesMongoRepository variablesRepository)
        {
            _functionsRepository = repository;
            _variablesRepository = variablesRepository;
        }

        [HttpPost]
        public string Get(string expression)
        {
            var interpreter = new Interpreter(new ExecutorContext(_variablesRepository, _functionsRepository));
            return interpreter.Execute(expression).ToString();
        }
    }
}
