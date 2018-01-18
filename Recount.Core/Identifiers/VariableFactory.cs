using Recount.Core.Lexemes;

namespace Recount.Core.Identifiers
{
    public static class VariableFactory
    {
        public static bool CheckSymbol(char symbol)
        {
            return char.IsLetter(symbol) || symbol.Equals('_');
        }

        public static Variable CreateVariable(LexemeBuilder builder)
        {
            return new Variable(builder);
        }
    }
}
