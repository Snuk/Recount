using System.Collections.Generic;
using Recount.Core.Identifiers;
using Recount.Core.Numbers;

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
