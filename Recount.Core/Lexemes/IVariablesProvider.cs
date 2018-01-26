using System.Collections.Generic;
using Recount.Core.Numbers;
using Recount.Core.Variables;

namespace Recount.Core.Lexemes
{
    public interface IVariablesProvider
    {
        void Add(Variable name, Number value);

        Number Get(Variable name);

        Dictionary<Variable, Number> GetAll();

        void Delete(Variable name);
    }
}
