using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recount.Core.Functions;
using Recount.DataAccess.Repositories;

namespace Recount.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FunctionsController : Controller
    {
        private readonly FunctionsMongoRepository _functionsRepository;

        public FunctionsController(FunctionsMongoRepository repository)
        {
            _functionsRepository = repository;
        }

        [HttpGet]
        public List<Function> Get()
        {
            return _functionsRepository.GetAll();
        }

        [HttpGet("{name}")]
        public Function Get(string name)
        {
            return _functionsRepository.Get(name);
        }

        [HttpPost]
        public void Post([FromBody] Function function)
        {
            _functionsRepository.Add(function);
        }

        [HttpPut]
        public void Put([FromBody] Function function)
        {
            _functionsRepository.Add(function);
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            _functionsRepository.Delete(name);
        }
    }
}
