using System;
using System.Collections.Generic;
using System.Linq;
using Recount.Core.Exceptions;
using Recount.Core.Functions;
using Recount.Core.Numbers;
using Recount.Core.Operators;
using Recount.Core.Variables;

namespace Recount.Core.Lexemes
{
    public class CalculationLexemesStack : ILexemesStack
    {
        private readonly IVariablesProvider _variablesProvider;
        private readonly IFunctionsProvider _functionsProvider;
        private readonly Stack<Operator> _operators;
        private readonly Stack<Lexeme> _operands;

        private int _bracketsBalance;

        public CalculationLexemesStack(IVariablesProvider variablesProvider, IFunctionsProvider functionsProvider)
        {
            _variablesProvider = variablesProvider;
            _functionsProvider = functionsProvider;
            _operators = new Stack<Operator>();
            _operands = new Stack<Lexeme>();
        }

        public Number GetResult()
        {
            try
            {
                if (_bracketsBalance != 0)
                {
#warning fsdfds
                    throw new Exception( /*lexeme*/);
                }

                var result = PopNumber();

                var validState = _operators.Count == 0 && _operands.Count == 0;

#warning sdfgsdgs
                return validState ? result : throw new Exception();
            }
            finally
            {
                Clear();
            }
        }

        public void Push(Lexeme lexeme)
        {
            switch (lexeme)
            {
                case Number number:
                    _operands.Push(number);
                    break;
                case Variable variable:
                    _operands.Push(variable);
                    break;
                case Operator @operator:

                    switch (@operator)
                    {
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

                    _operators.Push(@operator);
                    break;
            }
        }

        public void AddFunction(Function function)
        {
            _functionsProvider.Add(function);
        }

        public void AddVariable(Variable name, Number value)
        {
            _variablesProvider.Add(name, value);
        }

        public Function GetFunction(Variable name)
        {
            return _functionsProvider.Get(name);
        }

        public void PopOperators()
        {
            var lastOperator = PopOperator();

            while (PeekOperator() != null && PeekOperator() > lastOperator)
            {
                var previousOperator = PopOperator();

                if (previousOperator is AssignmentOperator)
                {
                    var number = PopNumber();
                    var secondOperand = PopOperand();
                    if (secondOperand is Variable variable)
                    {
                        _variablesProvider.Add(variable, number);
                        Push(number);
                        continue;
                    }
                    else
                    {
                        throw new SyntaxException(secondOperand);
                    }
                }

                var result = previousOperator.Binary
                                 ? previousOperator.ExecuteBinary(PopNumber().Value, PopNumber().Value)
                                 : previousOperator.ExecuteUnary(PopNumber().Value);

                var lexeme = new Number(result);
                _operands.Push(lexeme);
            }

            if (lastOperator is ClosingBracket)
            {
                var previousOperator = PopOperator();
                if (!(previousOperator is OpeningBracket))
                {
                    throw new SyntaxException(previousOperator);
                }
            }
            else
            {
                Push(lastOperator);
            }
        }

        public CalculationLexemesStack Copy()
        {
            return new CalculationLexemesStack(_variablesProvider, _functionsProvider);
        }

        private void Clear()
        {
            _bracketsBalance = 0;
            _operators.Clear();
            _operands.Clear();
        }

        private Operator PeekOperator()
        {
            return _operators.Peek();
        }

        private Operator PopOperator()
        {
            var lastOperator = _operators.Pop();

            switch (lastOperator)
            {
                case OpeningBracket _:
                    _bracketsBalance--;
                    break;
                case ClosingBracket _:
                    _bracketsBalance++;
                    break;
            }

            return lastOperator;
        }

        private Lexeme PopOperand()
        {
            return _operands.Pop();
        }

        private Number PopNumber()
        {
            var operand = PopOperand();
            switch (operand)
            {
                case Number number:
                    return number;

                case Variable variable:
                    return _variablesProvider.Get(variable);
            }

            throw new SyntaxException(operand);
        }
    }
}
