using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.Operations;
using System;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private TextBox _resultBox;
        private string _currentValue = "";
        private char _currentOperator = ' ';
        private double _previousValue = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.CanResize = false;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _resultBox = this.Find<TextBox>("ResultBox");
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentValue.Length > 0)
            {
                _currentValue = _currentValue.Substring(0, _currentValue.Length - 1);
                _resultBox.Text = _currentValue;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _currentValue = "";
            _currentOperator = ' ';
            _previousValue = 0;
            _resultBox.Text = "";
        }


        private void AdvancedFunctionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string function = button.Content.ToString();

                switch(function)
                    {
                    case "x²":
                        try
                        {
                            _currentValue = Math.Pow(double.Parse(_currentValue), 2).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'x²': {ex.Message}";
                        }
                        break;
                    case "√x":
                        try
                        {
                            _currentValue = Math.Sqrt(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in '√x': {ex.Message}";
                        }
                        break;
                    case "sin":
                        try
                        {
                            _currentValue = Math.Sin(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'sin': {ex.Message}";
                        }
                        break;
                    case "cos":
                        try
                        {
                            _currentValue = Math.Cos(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'cos': {ex.Message}";
                        }
                        break;
                    case "abs":
                        try
                        {
                            _currentValue = Math.Abs(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'abs': {ex.Message}";
                        }
                        break;
                    case "!":
                        try
                        {
                            _currentValue = CalculateFactorial(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in '!': {ex.Message}";
                        }
                        break;
                    case "pi":
                        _currentValue = Math.PI.ToString();
                        break;
                    case "e":
                        _currentValue = Math.E.ToString();
                        break;
                    case "log10":
                        try
                        {
                            _currentValue = Math.Log10(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'log10': {ex.Message}";
                        }
                        break;
                    case "ln":
                        try
                        {
                            _currentValue = Math.Log(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'ln': {ex.Message}";
                        }
                        break;
                    case "tan":
                        try
                        {
                            _currentValue = Math.Tan(double.Parse(_currentValue)).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'tan': {ex.Message}";
                        }
                        break;
                    case "cot":
                        try
                        {
                            _currentValue = (1.0 / Math.Tan(double.Parse(_currentValue))).ToString();
                        }
                        catch (Exception ex)
                        {
                            _currentValue = $"Error in 'cot': {ex.Message}";
                        }
                        break;
                    default:
                        _currentValue = "Invalid function.";
                        break;
                    }
                    _resultBox.Text = _currentValue;
            }
        }

        static double CalculateFactorial(double n)
        {
            if (n < 0)
            {
                throw new ArgumentException();
            }

            if (n == 0 || n == 1)
            {
                return 1;
            }

            double result = 1;
            for (double i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                _currentValue += button.Content.ToString();
                _resultBox.Text = _currentValue;
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                try
                {
                    _previousValue = double.Parse(_currentValue);
                    _currentValue = "";
                    _currentOperator = button.Content.ToString()[0];
                } catch (Exception ex)
                {
                    _resultBox.Text = ex.Message;
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOperator != ' ')
            {
                double currentValue = double.Parse(_currentValue);
                double result = Calculate(_previousValue, currentValue, _currentOperator);
                _resultBox.Text = result.ToString();
                _currentValue = result.ToString();
                _currentOperator = ' ';
            }
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentValue.Contains("."))
            {
                _currentValue += ".";
                _resultBox.Text = _currentValue;
            }
        }

        private double Calculate(double firstValue, double secondValue, char operation)
        {
            switch (operation)
            {
                case '+':
                    return firstValue + secondValue;
                case '-':
                    return firstValue - secondValue;
                case '*':
                    return firstValue * secondValue;
                case '/':
                    if (secondValue != 0)
                        return firstValue / secondValue;
                    else
                        return double.NaN; // Handle division by zero
                default:
                    return 0;
            }
        }
    }
}