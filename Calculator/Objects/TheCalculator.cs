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
        private CalculatorState _state;
        public CalculatorState State => _state;

        public int NumOfDigits { get; }
        public long ValueLimitUpper { get; }
        public long ValueLimitLower { get; }
        public long AppendLimit { get; }
        public char PaddingChar { get; set; } = '0';

        public event Action<string> OutputChanged;

        public TheCalculator(int numOfDigits)
        {
            NumOfDigits = numOfDigits;
            // 4 digits limit => 10^4 - 1 = 9999
            ValueLimitUpper = (long)Math.Pow(10, NumOfDigits) - 1;
            // compensate to make room for - sign
            ValueLimitLower = -(long)Math.Pow(10, NumOfDigits - 1) + 1;

            AppendLimit = (long)Math.Pow(10, NumOfDigits - 1) - 1;
        }

        public void AppendDigit(int digit)
        {
            if (!_state.Input.HasValue)
            {
                _state.Input = digit;
            }
            else if (_state.Input <= AppendLimit)
            {
                _state.Input = _state.Input * 10 + digit;
            }

            _state.Operator = null;
            _state.StoredRhs = null;
            SetOutput(_state.Input.ToString());
        }

        public void Reset()
        {
            _state = new CalculatorState();
            SetOutput(PaddingChar.ToString());
        }

        public void SetOperator(Operator op)
        {
            _state.Operator = op;
            _state.EvaluateResult();
            _state.Input = null;

            SetOutput(_state.StoredResult.ToString());
        }

        public void Evaluate()
        {
            if (_state.Input.HasValue)
            {
                _state.StoredRhs = _state.Input.Value;
                _state.Input = null;
            }

            _state.EvaluateResult();

            SetOutput(_state.StoredResult.ToString());
        }

        private long Clamp(long rawResult)
        {
            return Math.Max(Math.Min(rawResult, ValueLimitUpper), ValueLimitLower);
        }

        protected virtual void SetOutput(string output)
        {
            OutputChanged?.Invoke(output);
        }
    }
}