using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recount.DataAccess.Providers;

namespace Recount.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VariablesController : Controller
    {
        private readonly MongoVariablesProvider _variablesProvider;

        public VariablesController(MongoVariablesProvider variablesProvider)
        {
            _variablesProvider = variablesProvider;
        }

        [HttpGet]
        public Dictionary<string, double> Get()
        {
            return _variablesProvider.GetAll();
        }

        [HttpGet("{name}")]
        public double Get(string name)
        {
            return _variablesProvider.Get(name);
        }

        //[HttpPost]
        //public void Post([FromBody] Variable variable)
        //{
        //    _variablesProvider.Add(variable);
        //}

        //[HttpPut("{name}")]
        //public void Put(string name, [FromBody] Variable variable)
        //{
        //    _variablesProvider.Add(variable);
        //}

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            _variablesProvider.Delete(name);
        }
    }
}
