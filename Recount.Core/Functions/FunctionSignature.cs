using System.Collections.Generic;
using System.Linq;
using Recount.Core.Lexemes;
using Recount.Core.Variables;

namespace Recount.Core.Functions
{
    public class FunctionSignature
    {
        public Variable Name { get; }

        public List<FunctionArgument> Arguments { get; }

        public FunctionSignature(Variable name)
        {
            Name = name;
            Arguments = new List<FunctionArgument>();
        }

        public void AppendArgument(LexemeBuilder builder)
        {
            Arguments.Add(new FunctionArgument(builder));
        }

        public bool IsValidFunctionDeclaration()
        {
            return Name.StartIndex == 0 && Arguments.All(argument => argument.IsIdentifier());
        }

        public Function ConvertToFunction()
        {
            return new Function { Name = Name.Body, Parameters = Arguments.Select(a => a.ToString()).ToList() };
        }
    }
}