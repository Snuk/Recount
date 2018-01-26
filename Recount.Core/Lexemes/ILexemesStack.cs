using Recount.Core.Functions;

namespace Recount.Core.Lexemes
{
    public interface ILexemesStack
    {
        double? GetResult();

        void PopOperators();

        void Push(Lexeme lexeme);

        void AddFunction(Function function);

        void AddVariable(string name, double value);

        Function GetFunction(string name);

        CalculationLexemesStack Copy();
    }
}