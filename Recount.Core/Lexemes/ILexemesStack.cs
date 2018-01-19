using System.Collections.Generic;
using Recount.Core.Functions;
using Recount.Core.Identifiers;
using Recount.Core.Numbers;

namespace Recount.Core.Lexemes
{
    public interface ILexemesStack
    {
        Number GetResult();

        void PopOperators();

        void Push(Lexeme lexeme);

        void AddFunction(Function function);

        void AddVariable(Variable name, Number value);

        Dictionary<Variable, Number> GetVariables();

        Function GetFunction(Variable name);
    }
}