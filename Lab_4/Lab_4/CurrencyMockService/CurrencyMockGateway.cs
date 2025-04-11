using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_4.CurrencyMockService
{
    public class CurrencyMockGateway : GUI.ICurrencyService
    {
        public Task<List<string>> GetCurrencyList()
        {
            return Task.FromResult(new List<string> { "AAA", "BBB", "CCC" });
        }
        public Task<decimal> Exchange(string cur1, string cur2, decimal amt)
        { 
            switch(cur1)
            {
                case "AAA":
                    amt *= 0.33m;
                    break;
                case "BBB":
                    amt *= 43;
                    break;
                case "CCC":
                    amt *= 97;
                    break;
            }
            switch (cur2)
            {
                case "AAA":
                    amt /= 0.33m;
                    break;
                case "BBB":
                    amt /= 43;
                    break;
                case "CCC":
                    amt /= 97;
                    break;
            }
            return Task.FromResult(amt);
        }
    }
}
