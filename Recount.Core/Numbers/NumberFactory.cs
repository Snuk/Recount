using System;
using System.Globalization;
using Recount.Core.Lexemes;

namespace Recount.Core.Numbers
{
    public static class NumberFactory
    {
        public static readonly char Dividor;

        static NumberFactory()
        {
            Dividor = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        public static Number CreateNumber(LexemeBuilder builder)
        {
            try
            {
                return new Number(builder, double.Parse(builder.Body, CultureInfo.CurrentCulture));
            }
            catch (FormatException)
            {
                throw new Exception("ошибка при записи числа");
            }
            catch (OverflowException)
            {
                throw new Exception("слишком большое/маленькое число");
            }
        }

        public static bool CheckSymbol(char symbol)
        {
            return char.IsNumber(symbol) || symbol == Dividor;
        }
    }
}
