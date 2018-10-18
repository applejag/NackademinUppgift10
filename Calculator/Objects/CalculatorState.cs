using System;
using System.Diagnostics.Contracts;
using Calculator.Helpers;

namespace Calculator.Objects
{
    public struct CalculatorState
    {
        public Operator? Operator { get; set; }
        public long? Input { get; set; }
        public long? StoredRhs { get; set; }
        public long? StoredResult { get; set; }

        public void EvaluateResult()
        {
            StoredResult = GetEvaluatedResult();
        }

        [Pure]
        public long GetEvaluatedResult()
        {
            long lhs = StoredResult ?? Input ?? 0;

            if (Operator is null) return lhs;

            long rhs = StoredRhs ?? Input ?? 0;

            if (StoredResult is null) return rhs;

            return CalculatorHelper.EvaluateExpression(lhs, rhs, Operator.Value);
        }
    }
}