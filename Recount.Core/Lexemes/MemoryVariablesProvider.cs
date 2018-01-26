using System.Collections.Generic;
using Recount.Core.Numbers;
using Recount.Core.Variables;

namespace Recount.Core.Lexemes
{
    public class MemoryVariablesProvider : IVariablesProvider
    {
        private readonly Dictionary<Variable, Number> _variables;

        public MemoryVariablesProvider()
        {
            _variables = new Dictionary<Variable, Number>();
        }

        public void Add(Variable name, Number value)
        {
            _variables[name] = value;
        }

        public Number Get(Variable name)
        {
            return _variables[name];
        }

        Dictionary<Variable, Number> IVariablesProvider.GetAll()
        {
            return _variables;
        }

        public void Delete(Variable name)
        {
            _variables.Remove(name);
        }
    }
}
