using Microsoft.AspNetCore.Mvc;
using Recount.Core;
using Recount.Core.Lexemes;
using Recount.DataAccess.Providers;

namespace Recount.Api.Controllers
{
    [Route("api/[controller]")]
    public class InterpreterController : Controller
    {
        private readonly MongoFunctionsProvider _functionsProvider;
        private readonly MongoVariablesProvider _variablesProvider;

        public InterpreterController(MongoFunctionsProvider functionsProvider, MongoVariablesProvider variablesProvider)
        {
            _functionsProvider = functionsProvider;
            _variablesProvider = variablesProvider;
        }

        [HttpPost]
        public string Get(string expression)
        {
            var lexemesStack = new CalculationLexemesStack(_variablesProvider, _functionsProvider);
            var interpreter = new Interpreter(lexemesStack);
            return interpreter.Execute(expression).ToString();
        }
    }
}
