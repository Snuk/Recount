using Microsoft.AspNetCore.Mvc;
using Recount.Core;
using Recount.Core.Lexemes;

namespace Recount.Web.Controllers
{
    [Route("api/[controller]")]
    public class RecountController : Controller
    {
        [HttpGet]
        public string Get(string expression)
        {
            var interpreter = new Interpreter(new CalculationLexemesStack());
            return interpreter.Execute(expression).ToString();
        }
    }
}
