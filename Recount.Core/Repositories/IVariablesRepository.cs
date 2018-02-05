using System.Collections.Generic;

namespace Recount.Core.Repositories
{
    public interface IVariablesRepository
    {
        void Add(string name, double value);

        double Get(string name);

        Dictionary<string, double> GetAll();

        void Delete(string name);
    }
}
