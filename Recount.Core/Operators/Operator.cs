using Recount.Core.Lexemes;
using Recount.Core.Symbols;

namespace Recount.Core.Operators
{
    /// <summary>
    /// базовый класс для всех операторов
    /// </summary>
    public abstract class Operator : Lexeme
    {
        /// <summary>
        /// приоритет опреатора
        /// </summary>
        protected OperatorPriority Priority { private get; set; }

        /// <summary>
        /// ассоциированность оператора
        /// </summary>
        protected OperatorAssociation Association { private get; set; }

        /// <summary>
        /// бинарный/унарный оператор, true - бинарный, false - унарный
        /// </summary>
        public bool Binary { get; protected set; }

        protected Operator(Symbol symbol)
            : base(new LexemeBuilder(symbol))
        {
            Binary = true;
            Association = OperatorAssociation.Left;
        }

        /// <summary>
        /// вычистление бинарного оператора
        /// </summary>
        /// <param name="firstOperand">первый операнд</param>
        /// <param name="secondOperand">второй опреанд</param>
        /// <returns>результат вычислений</returns>
        public abstract double ExecuteBinary(double secondOperand, double firstOperand);

        /// <summary>
        /// вычисление унарного оператора
        /// </summary>
        /// <param name="operand">операнд</param>
        /// <returns>результат вычислений</returns>
        public abstract double ExecuteUnary(double operand);

        /// <summary>
        /// опреатор сравнения больше с учётом ассоциативности второго оператора
        /// для преобразования в ОПН
        /// </summary>
        /// <param name="first">первый оператор</param>
        /// <param name="second">второй оператор</param>
        /// <returns>результат сравнения приоритетов</returns>
        public static bool operator >(Operator first, Operator second)
        {
            return second.Association == OperatorAssociation.Right ? first.Priority > second.Priority : first.Priority >= second.Priority;
        }

        /// <summary>
        /// опреатор сравнения меньше с учётом ассоциативности первого оператора
        /// для преобразования в ОПН
        /// </summary>
        /// <param name="first">первый оператор</param>
        /// <param name="second">второй оператор</param>
        /// <returns>результат сравнения приоритетов</returns>
        public static bool operator <(Operator first, Operator second)
        {
            return first.Association == OperatorAssociation.Right ? first.Priority < second.Priority : first.Priority <= second.Priority;
        }
    }
}