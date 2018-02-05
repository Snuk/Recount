using System.Collections.Generic;
using Recount.Core.Contexts;
using Recount.Core.Exceptions;
using Recount.Core.Operators;
using Recount.Core.Variables;

namespace Recount.Core.Lexemes
{
    public class ValidatorLexemesStack : ILexemesStack
    {
        private int _bracketsBalance;
        private readonly Stack<Variable> _variables;

        public ValidatorLexemesStack()
        {
            _variables = new Stack<Variable>();
            _bracketsBalance = 0;
        }

        public bool IsValid()
        {
            return _bracketsBalance == 0;
        }

        public double? GetResult(ExecutorContext context)
        {
            foreach (var variable in _variables)
            {
                if (context._variablesRepository.Get(variable.Body) == null)
                {
                    throw new SyntaxException(variable);
                }
            }

            return null;
        }

        public void PopOperators(ExecutorContext context)
        {
        }

        public void Push(Lexeme lexeme)
        {
            switch (lexeme)
            {
                case Variable variable:
                    _variables.Push(variable);
                    break;

                case OpeningBracket _:
                    _bracketsBalance++;
                    break;

                case ClosingBracket _:
                    _bracketsBalance--;
                    break;
            }

            if (_bracketsBalance < 0)
            {
                throw new SyntaxException(lexeme);
            }
        }
    }
}