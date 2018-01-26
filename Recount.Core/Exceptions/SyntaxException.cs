using System;
using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.Exceptions
{
    public class SyntaxException : Exception
    {
        public SyntaxException(int index, char symbol)
            : base($"syntax error at {index} char, bad value - {symbol}")
        {
            StartIndex = index;
            EndIndex = index;
            InvalidText = symbol.ToString();
        }

        public SyntaxException(Lexeme lexeme)
            : base($"syntax error in {lexeme.StartIndex}-{lexeme.EndIndex} chars, bad value - {lexeme.Body}")
        {
            StartIndex = lexeme.StartIndex;
            EndIndex = lexeme.EndIndex;
            InvalidText = lexeme.Body;
        }

        public SyntaxException(Symbol symbol)
            : base($"syntax error at {symbol.Index} char, bad value - {symbol.Value}")
        {
            StartIndex = symbol.Index;
            EndIndex = symbol.Index;
            InvalidText = symbol.Value.ToString();
        }

        public SyntaxException(LexemeBuilder builder)
            : base($"syntax error in {builder.StartIndex}-{builder.EndIndex} chars, bad value - {builder.Body}")
        {
            StartIndex = builder.StartIndex;
            EndIndex = builder.EndIndex;
            InvalidText = builder.Body;
        }

        public int StartIndex { get; }

        public int EndIndex { get; }

        public string InvalidText { get; }
    }
}
