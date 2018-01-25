using System.Collections.Generic;
using Recount.Core.Functions;
using Recount.Core.Identifiers;

namespace Recount.Core.Lexemes
{
    public interface IFunctionsProvider
    {
        void Add(Function function);

        Function Get(Variable name);

        List<Function> GetAll();

        void Delete(Variable name);
    }
}
