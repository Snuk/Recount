using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recount.DataAccess.Providers;

namespace Recount.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VariablesController : Controller
    {
        private readonly MongoVariablesProvider _variablesProvider = new MongoVariablesProvider();

        [HttpGet]
        public Dictionary<string, double> Get()
        {
            return _variablesProvider.GetAll();
        }

        //[HttpGet("{name}", Name = "Get")]
        //public Variable Get(string name)
        //{
        //    return _variablesProvider.Get(name);
        //}

        //[HttpPost]
        //public void Post([FromBody] Variable variable)
        //{
        //    _variablesProvider.Add(variable);
        //}

        //[HttpPut("{name}")]
        //public void Put(int name, [FromBody] Variable variable)
        //{
        //    _variablesProvider.Add(variable);
        //}

        //[HttpDelete("{name}")]
        //public void Delete(string name)
        //{
        //    _variablesProvider.Delete(name);
        //}
    }
}
