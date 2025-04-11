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

namespace Lab_4.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ICurrencyService> serv;
        public MainWindow(List<ICurrencyService> serv)
        {
            this.serv = serv;
            InitializeComponent();
        }
        private async void TextBoxAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxResult.Text = Convert.ToString(
                await serv[ComboBoxConventor.SelectedIndex].Exchange(Convert.ToString(ComboBoxFrom.SelectedValue),
            Convert.ToString(ComboBoxTo.SelectedValue),
            Convert.ToDecimal(TextBoxAmount.Text)));
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> conventors = new List<string>();
            foreach (var i in serv)
                conventors.Add(i.GetType().Name);
            ComboBoxConventor.ItemsSource = conventors;
            ComboBoxConventor.SelectedIndex = 0;
            ComboBoxConventor_SelectionChanged(null, null);
        }

        private async void ComboBoxConventor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> currencies = await serv[ComboBoxConventor.SelectedIndex].GetCurrencyList();
            ComboBoxFrom.ItemsSource = currencies;
            ComboBoxTo.ItemsSource = currencies;
        }
    }
}
