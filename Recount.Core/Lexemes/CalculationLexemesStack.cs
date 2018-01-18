using System;
using System.Collections.Generic;
using System.Linq;
using Recount.Core.Functions;
using Recount.Core.Identifiers;
using Recount.Core.Numbers;
using Recount.Core.Operators;

namespace Recount.Core.Lexemes
{
    public class CalculationLexemesStack : ILexemesStack
    {
        private readonly List<Operator> _operators;
        private readonly List<Lexeme> _operands;
        private readonly List<Variable> _variables;
        private readonly Dictionary<Variable, Number> _variablesMap;
        private readonly Dictionary<Variable, Function> _functionsMap;

        private int _bracketsBalance;

        public CalculationLexemesStack()
        {
            _functionsMap = new Dictionary<Variable, Function>();
            _variablesMap = new Dictionary<Variable, Number>();
            _operators = new List<Operator>();
            _operands = new List<Lexeme>();
            _variables = new List<Variable>();
        }

        public Number GetResult()
        {
            try
            {
                if (_bracketsBalance != 0)
                {
#warning
                    throw new Exception( /*lexeme*/);
                }

                var result = PopNumber();

                var validState = _operators.Count == 0 && _variables.Count == 0 && _operands.Count == 0;
                if (validState)
                {
                    return result;
                }

                throw new Exception();
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
                    _operands.Add(number);
                    break;
                case Variable variable:
                    _operands.Add(variable);
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
#warning
                        throw new Exception( /*lexeme*/);
                    }

                    _operators.Add(@operator);
                    break;
            }
        }

        public void AddFunction(Function function)
        {
            _functionsMap[function.Name] = function;
        }

        public void AddVariable(Variable name, Number value)
        {
            _variablesMap[name] = value;
        }

        public Function GetFunction(Variable name)
        {
            return _functionsMap[name];
        }

        public void PopOperators()
        {
            var @operator = PopOperator();

            while (PeekOperator() != null && PeekOperator() > @operator)
            {
                var previousOperator = PopOperator();

                if (previousOperator is AssignmentOperator)
                {
                    var number = PopNumber();
                    if (PopOperand() is Variable variable)
                    {
                        _variablesMap[variable] = number;
                        Push(number);
                        continue;
                    }

                    throw new Exception();
                }

                var result = previousOperator.Binary
                                 ? previousOperator.ExecuteBinary(PopNumber().Value, PopNumber().Value)
                                 : previousOperator.ExecuteUnary(PopNumber().Value);

                var lexeme = new Number(result);
                _operands.Add(lexeme);
            }

            if (@operator is ClosingBracket)
            {
                if (!(PopOperator() is OpeningBracket))
                {
                    throw new Exception();
                }
            }
            else
            {
                Push(@operator);
            }
        }

        private void Clear()
        {
            _bracketsBalance = 0;
            _operators.Clear();
            _operands.Clear();
            _variables.Clear();
        }

        private Operator PeekOperator()
        {
            return _operators.LastOrDefault();
        }

        private Operator PopOperator()
        {
            var @operator = _operators.LastOrDefault();

            switch (@operator)
            {
                case OpeningBracket _:
                    _bracketsBalance--;
                    break;
                case ClosingBracket _:
                    _bracketsBalance++;
                    break;
            }

            _operators.RemoveAt(_operators.Count - 1);
            return @operator;
        }

        private Lexeme PopOperand()
        {
            var lastNumber = _operands.LastOrDefault();
            _operands.RemoveAt(_operands.Count - 1);
            return lastNumber;
        }

        private Number PopNumber()
        {
            var operand = PopOperand();
            switch (operand)
            {
                case Number number:
                    return number;

                case Variable variable when _variablesMap.ContainsKey(variable):
                    return _variablesMap[variable];
            }

            throw new Exception();
        }
    }
}
