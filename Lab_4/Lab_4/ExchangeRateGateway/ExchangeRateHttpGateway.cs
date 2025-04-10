using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab_4.ExchangeRateGateway
{
    public class ExchangeRateHttpGateway : GUI.ICurrencyService
    {
        string apiKey;
        HttpClient httpClient;
        public ExchangeRateHttpGateway(string apiKey) 
        { 
            this.apiKey = apiKey;
            httpClient = new HttpClient();
        }
        public async Task<List<string>> GetCurrencyList() 
        {
            string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/USD";
            var response = await httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<ApiResponse>(response);

            if (data.result == "success")
            {
                List<string> currencies = new List<string>();
                foreach (var rate in data.conversionRates)
                {
                    currencies.Add(rate.Key);
                }
                return currencies;
            }
            return new List<string>();
        }

        public async Task<decimal> Exchange(string cur1, string cur2, decimal amt) 
        {
            string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/pair/{cur1}/{cur2}/{amt}";
            var response = await httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<ExchangeResponse>(response);

            if (data.result == "success")
                return data.conversionResult;
            else
                return -1;
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
