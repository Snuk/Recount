using System;
using System.Collections.Generic;
using Recount.Core.Functions;
using Recount.Core.Identifiers;
using Recount.Core.Numbers;
using Recount.Core.Operators;

namespace Recount.Core.Lexemes
{
    public class FunctionValidatorLexemesStack : ILexemesStack
    {
        private readonly List<Variable> _fiunctionParameters;
        private int _bracketsBalance;

        public FunctionValidatorLexemesStack(List<Variable> fiunctionParameters)
        {
            _fiunctionParameters = fiunctionParameters;
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
                    if (!_fiunctionParameters.Contains(variable))
                    {
                        throw new Exception();
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
                throw new Exception( /*lexeme*/);
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

        public Dictionary<Variable, Number> GetVariables()
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