namespace Recount.Core.Operators
{
    public enum OperatorPriority
    {
        Comma = 0,
        OpeningBracket = 1,
        ClosingBracket = 2,
        Assignment = 3,
        BinaryPlusMinus = 4,
        MultiplicationDivision = 5,
        UnaryPlusMinus = 6,
        Power = 7
    }
}