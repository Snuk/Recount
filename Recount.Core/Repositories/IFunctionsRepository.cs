using System.Collections.Generic;
using Recount.Core.Functions;

namespace Recount.Core.Repositories
{
    public interface IFunctionsRepository
    {
        void Add(Function function);

        Function Get(string name);

        List<Function> GetAll();

        void Delete(string name);
    }
}
