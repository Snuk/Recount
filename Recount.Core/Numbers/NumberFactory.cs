using System;
using System.Globalization;
using Recount.Core.Exceptions;
using Recount.Core.Lexemes;

namespace Recount.Core.Numbers
{
    public static class NumberFactory
    {
        public static readonly char DecimalSeparator;
        public static readonly NumberFormatInfo NumberFormatInfo;

        static NumberFactory()
        {
            DecimalSeparator = '.';
            NumberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = DecimalSeparator.ToString() };
        }

        public static Number CreateNumber(LexemeBuilder builder)
        {
            try
            {
                return new Number(builder, double.Parse(builder.Body, NumberFormatInfo));
            }
            catch (Exception)
            {
                throw new SyntaxException(builder);
            }
        }

        public static bool CheckSymbol(char symbol)
        {
            return char.IsNumber(symbol) || symbol == DecimalSeparator;
        }
    }
}
