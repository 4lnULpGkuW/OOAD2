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
    public partial class MainWindow : Window
    {
        public class VariableEntry
        {
            public string Variable { get; set; }
            public double Value { get; set; }
        }
        ObservableCollection<VariableEntry> Variables = new();
        Dictionary<string, INode> History = new();
        public MainWindow()
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
                INode node;
                if (History.TryGetValue(TextBoxPrefix.Text, out node)) { }
                else
                {
                    node = Parser.ParsePrefix(TextBoxPrefix.Text);
                    History.Add(TextBoxPrefix.Text, node);
                }
                TextBoxInfix.Text = node.ToInfix();
                TextBoxEval.Text = node.Evaluate(Variables.ToDictionary(t => t.Variable, t => t.Value)).ToString();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}