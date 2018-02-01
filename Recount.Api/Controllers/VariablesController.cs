using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recount.DataAccess.Repositories;

namespace Recount.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VariablesController : Controller
    {
        private readonly VariablesMongoRepository _variablesRepository;

        public VariablesController(VariablesMongoRepository variablesRepository)
        {
            _variablesRepository = variablesRepository;
        }

        [HttpGet]
        public Dictionary<string, double> Get()
        {
            return _variablesRepository.GetAll();
        }

        [HttpGet("{name}")]
        public double Get(string name)
        {
            return _variablesRepository.Get(name);
        }

        //[HttpPost]
        //public void Post([FromBody] Variable variable)
        //{
        //    _variablesRepository.Add(variable);
        //}

        //[HttpPut("{name}")]
        //public void Put(string name, [FromBody] Variable variable)
        //{
        //    _variablesRepository.Add(variable);
        //}

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            _variablesRepository.Delete(name);
        }
    }
}
