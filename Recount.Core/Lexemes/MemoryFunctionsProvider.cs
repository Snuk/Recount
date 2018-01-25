using System.Collections.Generic;
using System.Linq;
using Recount.Core.Functions;
using Recount.Core.Identifiers;

namespace Recount.Core.Lexemes
{
    public class MemoryFunctionsProvider : IFunctionsProvider
    {
        private readonly Dictionary<Variable, Function> _functions;

        public MemoryFunctionsProvider()
        {
            _functions = new Dictionary<Variable, Function>();
        }

        public void Add(Function function)
        {
            _functions[function.Name] = function;
        }

        public Function Get(Variable name)
        {
            return _functions[name];
        }

        public List<Function> GetAll()
        {
            return _functions.Values.ToList();
        }

        public void Delete(Variable name)
        {
            _functions.Remove(name);
        }
    }
}
