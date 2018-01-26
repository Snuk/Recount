using System.Collections.Generic;
using System.Linq;

namespace Recount.Core.Functions
{
    public class Function
    {
        public string Name { get; set; }

        public List<string> Parameters { get; set; }

        public string Body { get; set; }

        public Function()
        {
            Parameters = new List<string>();
        }

        public override string ToString()
        {
            return $"{Name}({string.Join(",", Parameters.Select(p => p.ToString()).ToList())})={Body}";
        }
    }
}
