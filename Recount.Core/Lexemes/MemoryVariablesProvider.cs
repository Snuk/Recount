using System.Collections.Generic;

namespace Recount.Core.Lexemes
{
    public class MemoryVariablesProvider : IVariablesProvider
    {
        private readonly Dictionary<string, double> _variables;

        public MemoryVariablesProvider()
        {
            _variables = new Dictionary<string, double>();
        }

        public void Add(string name, double value)
        {
            _variables[name] = value;
        }

        public double Get(string name)
        {
            return _variables[name];
        }

        public Dictionary<string, double> GetAll()
        {
            return _variables;
        }

        public void Delete(string name)
        {
            _variables.Remove(name);
        }
    }
}
