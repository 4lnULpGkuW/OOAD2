using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for AnotherWindow.xaml
    /// </summary>
    /// 

    public partial class AnotherWindow : Window
    {
        private const string ApiKey = "0b382a9c9e91763dad6b63ae";
        private HttpClient httpClient = new HttpClient();

        public AnotherWindow()
        {
            InitializeComponent();
        }
        private async void TextBoxAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ComboBoxFrom.SelectedItem == null || ComboBoxTo.SelectedItem == null || string.IsNullOrWhiteSpace(TextBoxAmount.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            try
            {
                string fromCurrency = ComboBoxFrom.SelectedItem.ToString();
                string toCurrency = ComboBoxTo.SelectedItem.ToString();
                decimal amount = Convert.ToDecimal(TextBoxAmount.Text);

                string url = $"https://v6.exchangerate-api.com/v6/{ApiKey}/pair/{fromCurrency}/{toCurrency}/{amount}";
                var response = await httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<ExchangeResponse>(response);

                if (data.result == "success")
                {
                    TextBoxResult.Text = data.conversionResult.ToString("N2");
                }
                else
                {
                    MessageBox.Show($"Conversion failed: {data.errorType}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during conversion: {ex.Message}");
            }
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = $"https://v6.exchangerate-api.com/v6/{ApiKey}/latest/USD";
                var response = await httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<ApiResponse>(response);

                if (data.result == "success")
                {
                    List<string> currencies = new List<string>();
                    foreach (var rate in data.conversionRates)
                    {
                        currencies.Add(rate.Key);
                    }

                    ComboBoxFrom.ItemsSource = currencies;
                    ComboBoxTo.ItemsSource = currencies;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading currencies: {ex.Message}");
            }
        }
        public class ApiResponse
        {
            [JsonProperty("result")]
            public string result { get; set; }
            [JsonProperty("conversion_rates")]
            public Dictionary<string, decimal> conversionRates { get; set; }
        }
        public class ExchangeResponse
        {
            [JsonProperty("result")]
            public string result { get; set; }
            [JsonProperty("error-type")]
            public string errorType { get; set; }
            [JsonProperty("conversion_result")]
            public decimal conversionResult { get; set; }
        }
    }
}
