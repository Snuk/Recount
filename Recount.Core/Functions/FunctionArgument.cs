using Recount.Core.Identifiers;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;

namespace Recount.Core.Functions
{
    public class FunctionArgument
    {
        private readonly LexemeBuilder _builder;

        public string Body => _builder.Body;

        public FunctionArgument(LexemeBuilder builder)
        {
            _builder = builder;
        }

        public bool IsIdentifier()
        {
            if (_builder.IsEmpty)
            {
                return false;
            }

            var body = _builder.Body;

            if (!VariableFactory.CheckSymbol(body[0]))
            {
                return false;
            }

            for (var i = 1; i < body.Length; i++)
            {
                if (!VariableFactory.CheckSymbol(body[i])
                    && (!NumberFactory.CheckSymbol(body[i]) || body[i].Equals(NumberFactory.Dividor)))
                {
                    return false;
                }
            }

            return true;
        }

        public Variable ConvertToParameter()
        {
            return VariableFactory.CreateVariable(_builder);
        }
    }
}