using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator.Helpers;
using Calculator.Objects;

namespace Calculator
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TheCalculator _calculator;

        public MainWindow()
        {
            InitializeComponent();

            _calculator = new TheCalculator(12);
            _calculator.OutputChanged += CalculatorOnOutputChanged;
        }

        private void CalculatorOnOutputChanged(string output)
        {
            DigitWindow.Text = output;
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;

            _calculator.AppendDigit(int.Parse((string)button.Content));
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _calculator.Reset();
        }

        private void EvaluateButton_Click(object sender, RoutedEventArgs e)
        {
            _calculator.Evaluate();
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;

            char c = ((string)button.Content)[0];
            Operator op = CalculatorHelper.CharToOperator(c);

            _calculator.SetOperator(op);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                _calculator.RemoveDigit();
            }
            else
            {
                Button button = GetButtonFromKey(e.Key);
                if (button is null) return;

                button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                button.Focus();
            }
        }

        private Button GetButtonFromKey(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.NumPad0:
                    return DigitButton0;
                case Key.D1:
                case Key.NumPad1:
                    return DigitButton1;
                case Key.D2:
                case Key.NumPad2:
                    return DigitButton2;
                case Key.D3:
                case Key.NumPad3:
                    return DigitButton3;
                case Key.D4:
                case Key.NumPad4:
                    return DigitButton4;
                case Key.D5:
                case Key.NumPad5:
                    return DigitButton5;
                case Key.D6:
                case Key.NumPad6:
                    return DigitButton6;
                case Key.D7:
                case Key.NumPad7:
                    return DigitButton7;
                case Key.D8:
                case Key.NumPad8:
                    return DigitButton8;
                case Key.D9:
                case Key.NumPad9:
                    return DigitButton9;

                case Key.Add:
                    return ButtonAdd;
                case Key.Subtract:
                    return ButtonSubtract;
                case Key.Multiply:
                    return ButtonMultiply;
                case Key.Divide:
                    return ButtonDivide;

                case Key.Escape:
                case Key.C:
                case Key.Cancel:
                case Key.Clear:
                    return ButtonClear;

                case Key.Enter:
                    return ButtonEvaluate;

                default:
                    return null;
            }
        }
    }
}
