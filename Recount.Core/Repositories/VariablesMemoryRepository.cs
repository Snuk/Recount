using System.Collections.Generic;
using Recount.Core.Lexemes;

namespace Recount.Core.Repositories
{
    public class VariablesMemoryRepository : IVariablesRepository
    {
        private readonly Dictionary<string, double> _variables;

        public VariablesMemoryRepository()
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
