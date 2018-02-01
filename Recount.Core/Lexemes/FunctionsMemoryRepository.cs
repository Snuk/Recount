using System.Collections.Generic;
using System.Linq;
using Recount.Core.Functions;

namespace Recount.Core.Lexemes
{
    public class FunctionsMemoryRepository : IFunctionsRepository
    {
        private readonly Dictionary<string, Function> _functions;

        public FunctionsMemoryRepository()
        {
            _functions = new Dictionary<string, Function>();
        }

        public void Add(Function function)
        {
            _functions[function.Name] = function;
        }

        public Function Get(string name)
        {
            return _functions[name];
        }

        public List<Function> GetAll()
        {
            return _functions.Values.ToList();
        }

        public void Delete(string name)
        {
            _functions.Remove(name);
        }
    }
}
