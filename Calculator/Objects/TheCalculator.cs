using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

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

        public int NumOfDigits { get; }
        public long ValueLimit { get; }
        public long AppendLimit { get; }
        public char PaddingChar { get; set; } = '0';

        public event EventHandler InputChanged;

        public TheCalculator(int numOfDigits)
        {
            NumOfDigits = numOfDigits;
            // 4 digits => 10^4 - 1 => 9999
            ValueLimit = (long)Math.Pow(10, NumOfDigits) - 1;
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
        }

        public override string ToString()
        {
            return (Input?.ToString() ?? PaddingChar.ToString())
                .PadLeft(NumOfDigits, PaddingChar);
        }

        protected virtual void OnInputChanged()
        {
            InputChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}