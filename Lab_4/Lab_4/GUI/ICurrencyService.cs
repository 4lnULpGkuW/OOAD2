using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.GUI
{
    public interface ICurrencyService
    {
        Task<List<string>> GetCurrencyList();
        Task<decimal> Exchange(string cur1, string cur2, decimal amt);
    }
}
