using System;
using System.Collections.Generic;
using Recount.Core.Exceptions;
using Recount.Core.Functions;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Variables;

namespace Recount.Core.Lexemes
{
    public class FunctionValidatorLexemesStack : ILexemesStack
    {
        private readonly List<Variable> _functionParameters;
        private int _bracketsBalance;

        public FunctionValidatorLexemesStack(List<Variable> functionParameters)
        {
            _functionParameters = functionParameters;
            _bracketsBalance = 0;
        }

        public bool IsValid()
        {
            return _bracketsBalance == 0;
        }

        public Number GetResult()
        {
            throw new NotImplementedException();
        }

        public void PopOperators()
        {
        }

        public void Push(Lexeme lexeme)
        {
            switch (lexeme)
            {
                case Variable variable:
                    if (!_functionParameters.Contains(variable))
                    {
                        throw new SyntaxException(lexeme);
                    }
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

        public void AddFunction(Function function)
        {
            throw new NotImplementedException();
        }

        public void AddVariable(Variable name, Number value)
        {
            throw new NotImplementedException();
        }

        public Function GetFunction(Variable name)
        {
            throw new NotImplementedException();
        }

        public CalculationLexemesStack Copy()
        {
            throw new NotImplementedException();
        }
    }
}