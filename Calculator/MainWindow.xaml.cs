using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            _calculator = new TheCalculator(DigitWindow.Text.Length);
            _calculator.InputChanged += CalculatorOnInputChanged;
        }

        private void CalculatorOnInputChanged(object sender, EventArgs e)
        {
            DigitWindow.Text = _calculator.ToString();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            
            _calculator.AppendDigit(int.Parse((string) button.Content));
        }
    }
}
