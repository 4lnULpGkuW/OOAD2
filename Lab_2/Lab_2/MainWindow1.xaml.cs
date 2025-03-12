using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public class VariableEntry
        {
            public string Variable { get; set; }
            public double Value { get; set; }
        }
        ObservableCollection<VariableEntry> Variables = new();
        public MainWindow1()
        {
            InitializeComponent();

            Variables.Add(new VariableEntry { Variable = "x", Value = 5.0 });
            Variables.Add(new VariableEntry { Variable = "y", Value = 3.2 });

            DataGrid1.ItemsSource = Variables;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string prefixExpression = TextBoxPrefix.Text;
                string infixExpression = ParsePrefixToInfix(prefixExpression);
                var variables = new Dictionary<string, double>
                {
                    { "x", 3 },
                    { "y", 4 }  
                };
                double result = EvaluatePrefix(prefixExpression, variables);
                TextBoxInfix.Text = infixExpression;
                TextBoxEval.Text = result.ToString();
            }
            catch (Exception ex)
            {
                TextBoxInfix.Text = "Error";
                TextBoxEval.Text = ex.Message;
            }
        }
        private string ParsePrefixToInfix(string expression)
        {
            string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Stack<string> stack = new Stack<string>();

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];

                if (IsBinaryOperator(token))
                {
                    if (stack.Count < 2)
                        throw new InvalidOperationException($"Not enough operands for operator: {token}");

                    string operand1 = stack.Pop();
                    string operand2 = stack.Pop();

                    string infixExpression = $"({operand1} {token} {operand2})";
                    stack.Push(infixExpression);
                }
                else if (IsUnaryOperator(token))
                {
                    if (stack.Count < 1)
                        throw new InvalidOperationException($"Not enough operands for operator: {token}");

                    string operand1 = stack.Pop();
                    string infixExpression = $"{token} ({operand1})";
                    stack.Push(infixExpression);
                }
                else if (token == "neg")
                {
                    if (stack.Count < 1)
                        throw new InvalidOperationException("Not enough operands for unary minus");

                    string operand = stack.Pop();
                    stack.Push($"(-{operand})");
                }
                else
                {
                    stack.Push(token);
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException("Invalid expression: too many operands or operators");

            return stack.Pop();
        }
        private double EvaluatePrefix(string expression, Dictionary<string, double> variables)
        {
            string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Stack<double> stack = new Stack<double>();

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];

                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (variables.ContainsKey(token))
                {
                    stack.Push(variables[token]);
                }
                else if (IsBinaryOperator(token))
                {
                    if (stack.Count < 2)
                        throw new InvalidOperationException($"Not enough operands for operator: {token}");

                    double operand1 = stack.Pop();
                    double operand2 = stack.Pop();

                    double result = ApplyOperator(token, operand1, operand2);
                    stack.Push(result);
                }
                else if (IsUnaryOperator(token))
                {
                    if (stack.Count < 1)
                        throw new InvalidOperationException($"Not enough operands for operator: {token}");

                    double operand1 = stack.Pop();
                    double result = ApplyOperator(token, operand1);
                    stack.Push(result);
                }
                else if (token == "neg")
                {
                    if (stack.Count < 1)
                        throw new InvalidOperationException("Not enough operands for unary minus");

                    double operand = stack.Pop();
                    stack.Push(-operand);
                }
                else
                {
                    throw new InvalidOperationException($"Unknown token: {token}");
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException("Invalid expression: too many operands or operators");

            return stack.Pop();
        }
        private bool IsUnaryOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
        private bool IsBinaryOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
        private double ApplyOperator(string op, double operand1, double operand2 = 0)
        {
            switch (op)
            {
                case "+": return operand1 + operand2;
                case "-": return operand1 - operand2;
                case "*": return operand1 * operand2;
                case "/": return operand1 / operand2;
                case "^": return Math.Pow(operand1, operand2);
                case "sqrt": return Math.Sqrt(operand1);
                case "exp": return Math.Exp(operand1);
                case "log": return Math.Log(operand1);
                default:
                    throw new InvalidOperationException($"Unknown operator: {op}");
            }
        }
    }
}
