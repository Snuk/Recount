using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recount.Core.Functions;
using Recount.DataAccess.Providers;

namespace Recount.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FunctionsController : Controller
    {
        private readonly MongoFunctionsProvider _functionsProvider;

        public FunctionsController(MongoFunctionsProvider functionsProvider)
        {
            _functionsProvider = functionsProvider;
        }

        [HttpGet]
        public List<Function> Get()
        {
            return _functionsProvider.GetAll();
        }

        [HttpGet("{name}")]
        public Function Get(string name)
        {
            return _functionsProvider.Get(name);
        }

        [HttpPost]
        public void Post([FromBody] Function function)
        {
            _functionsProvider.Add(function);
        }

        [HttpPut]
        public void Put([FromBody] Function function)
        {
            _functionsProvider.Add(function);
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            _functionsProvider.Delete(name);
        }
    }
}
