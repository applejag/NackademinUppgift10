using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using Calculator.Helpers;

namespace Calculator.Objects
{
    public class TheCalculator
    {
        private long? _input;

        public long? Input {
            get => _input;
            private set {
                if (_input == value) return;
                _input = value;
                OnInputChanged();
            }
        }

        public Operator? Operator { get; private set; }
        public long? PreviousInput { get; private set; }

        public int NumOfDigits { get; }
        public long ValueLimitUpper { get; }
        public long ValueLimitLower { get; }
        public long AppendLimit { get; }
        public char PaddingChar { get; set; } = '0';

        public event EventHandler InputChanged;

        public TheCalculator(int numOfDigits)
        {
            NumOfDigits = numOfDigits;
            // 4 digits => 10^4 - 1 => 9999
            ValueLimitUpper = (long)Math.Pow(10, NumOfDigits) - 1;
            ValueLimitLower = -(long)Math.Pow(10, NumOfDigits - 1) + 1;
            AppendLimit = (long)Math.Pow(10, NumOfDigits - 1) - 1;
        }

        public void AppendDigit(int digit)
        {
            if (!Input.HasValue)
            {
                Input = digit;
            }
            else if (Input <= AppendLimit)
            {
                Input = Input * 10 + digit;
            }
        }

        public void Reset()
        {
            Input = null;
            PreviousInput = null;
            Operator = null;
        }

        public void SetOperator(Operator op)
        {
            PreviousInput = GetEvaluatedResult();
            Operator = op;
            Input = null;
        }

        public void Evaluate()
        {
            PreviousInput = GetEvaluatedResult();
            Input = null;
            Operator = null;
        }

        [Pure]
        public long GetEvaluatedResult()
        {
            if (PreviousInput is null) return Input ?? 0;
            if (Input is null) return PreviousInput ?? 0;
            if (Operator is null) return Input ?? 0;

            long rawResult = CalculatorHelper.EvaluateExpression(PreviousInput.Value, Input.Value, Operator.Value);

            // Clamp
            return Math.Max(Math.Min(rawResult, ValueLimitUpper), ValueLimitLower);
        }

        public override string ToString()
        {
            long? value = Operator.HasValue && !Input.HasValue
                ? PreviousInput
                : Input;

            return value?.ToString() ?? PaddingChar.ToString();
        }

        protected virtual void OnInputChanged()
        {
            InputChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}