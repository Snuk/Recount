﻿using System.Collections.Generic;

namespace Recount.Core.Lexemes
{
    public interface IVariablesProvider
    {
        void Add(string name, double value);

        double Get(string name);

        Dictionary<string, double> GetAll();

        void Delete(string name);
    }
}
