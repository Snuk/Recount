namespace Recount.Core.Lexemes
{
    public class Lexeme
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public string Body { get; set; }

        protected Lexeme(LexemeBuilder builder)
        {
            Body = builder.Body;
            StartIndex = builder.StartIndex;
            EndIndex = builder.EndIndex;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Lexeme lexeme)
            {
                return Body.Equals(lexeme.Body);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Body.GetHashCode();
        }

        public override string ToString()
        {
            return Body;
        }
    }
}