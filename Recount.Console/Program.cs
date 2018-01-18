using Recount.Core;
using Recount.Core.Lexemes;

namespace Recount.Console
{
    class Program
    {
        static void Main()
        {
            var analyser = new Calculator(new CalculationLexemesStack());

            while (true)
            {
                try
                {
                    var result = analyser.Calculate(System.Console.ReadLine());
                    System.Console.WriteLine(result.Value);
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            }
        }
    }
}
