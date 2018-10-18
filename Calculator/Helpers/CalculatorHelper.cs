using System;
using Calculator.Objects;

namespace Calculator.Helpers
{
    public static class CalculatorHelper
    {

        public static Operator CharToOperator(char c)
        {
            switch (c)
            {
                case '+': return Operator.Add;
                case '-': return Operator.Subtract;
                case '*': return Operator.Multiply;
                case '/': return Operator.Divide;
            }

            throw new NotSupportedException($"Unknown operator `{c}`.");
        }

        public static long EvaluateExpression(long lhs, long rhs, Operator op)
        {
            switch (op)
            {
                case Operator.Add: return lhs + rhs;
                case Operator.Subtract: return lhs - rhs;
                case Operator.Multiply: return lhs * rhs;
                case Operator.Divide: return lhs / rhs;
            }

            throw new NotSupportedException($"Unknown operator `{op}`.");
        }
    }
}