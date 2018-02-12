using Recount.Core;
using Recount.Core.Contexts;
using Recount.Core.Repositories;
using Xunit;

namespace Recount.Tests
{
    public class InterpreterTester
    {
        [Fact]
        public void Test()
        {
            var interpreter = new Interpreter(new ExecutorContext(new VariablesMemoryRepository(), new FunctionsMemoryRepository()));

            var result = interpreter.Execute("2+2");

            Assert.Equal(4, result.Value);
        }
    }
}
