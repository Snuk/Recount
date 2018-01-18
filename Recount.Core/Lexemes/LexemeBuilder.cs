using System.Text;
using Recount.Core.Symbols;

namespace Recount.Core.Lexemes
{
    public class LexemeBuilder
    {
        private readonly StringBuilder _builder;

        public int StartIndex { get; private set; }

        public int EndIndex { get; private set; }

        public string Body => _builder.ToString();

        public bool IsEmpty => _builder.Length == 0;

        public LexemeBuilder()
            : this(null)
        {
        }

        public LexemeBuilder(Symbol symbol)
        {
            _builder = new StringBuilder();
            if (symbol != null)
            {
                Start(symbol);
            }
        }

        public void Append(Symbol symbol)
        {
            if (_builder.Length == 0)
            {
                Start(symbol);
                return;
            }

            _builder.Append(symbol.Value);
            EndIndex++;
        }
        
        private void Start(Symbol symbol)
        {
            _builder.Append(symbol.Value);
            StartIndex = symbol.Index;
            EndIndex = symbol.Index;
        }
    }
}
