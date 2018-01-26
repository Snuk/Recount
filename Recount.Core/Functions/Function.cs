using System.Collections.Generic;
using System.Linq;
using Recount.Core.Variables;

namespace Recount.Core.Functions
{
    public class Function
    {
        public Variable Name { get; set; }

        public List<Variable> Parameters { get; set; }

        public string Body { get; set; }

        public Function()
        {
            Parameters = new List<Variable>();
        }

        public override string ToString()
        {
            return $"{Name}({string.Join(",", Parameters.Select(p => p.ToString()).ToList())})={Body}";
        }
    }
}
