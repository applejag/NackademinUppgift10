using System;
using System.Diagnostics.Contracts;
using Calculator.Helpers;

namespace Calculator.Objects
{
    public struct CalculatorState
    {
        public Operator? Operator { get; set; }
        public long? Input { get; set; }
        public long? StoredResult { get; set; }
        public bool JustEvaluated { get; set; }

        public void EvaluateResult()
        {
            StoredResult = GetEvaluatedResult();
        }

        [Pure]
        public long GetEvaluatedResult()
        {
            long lhs = StoredResult ?? 0;
            long rhs = Input ?? 0;

            if (StoredResult is null) return rhs;
            if (Operator is null) return rhs;

            return CalculatorHelper.EvaluateExpression(lhs, rhs, Operator.Value);
        }

        public override string ToString()
        {
            return $"e:{JustEvaluated} : {StoredResult?.ToString() ?? "null"} : {Operator?.ToString() ?? "null"} : {Input?.ToString() ?? "null"}";
        }
    }
}