using Recount.Core.Contexts;

namespace Recount.Core.Lexemes
{
    public interface ILexemesStack
    {
        double? GetResult(ExecutorContext context);

        void PopOperators(ExecutorContext context);

        void Push(Lexeme lexeme);
    }
}