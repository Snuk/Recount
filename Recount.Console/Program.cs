using Recount.Core;
using Recount.Core.Lexemes;

namespace Recount.Console
{
    class Program
    {
        static void Main()
        {
            var analyser = new Interpreter(new CalculationLexemesStack());

            while (true)
            {
                try
                {
                    var result = analyser.Execute(System.Console.ReadLine());
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
