using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class OperatorRegistry
    {
        public static readonly Dictionary<string, OperatorInfo> SupportedOperators = new();
        static OperatorRegistry()
        {
            RegisterOperator("+", 2, 1, (a, b) => a + b);
            RegisterOperator("-", 2, 1, (a, b) => a - b);
            RegisterOperator("*", 2, 2, (a, b) => a * b);
            RegisterOperator("/", 2, 2, (a, b) => a / b);
            RegisterOperator("^", 2, 3, (a, b) => Math.Pow(a, b));

            RegisterOperator("neg", 1, 4, (a, _) => -a);
            RegisterOperator("sqrt", 1, 3, (a, _) => Math.Sqrt(a));
            RegisterOperator("exp", 1, 3, (a, _) => Math.Exp(a));
            RegisterOperator("log", 1, 3, (a, _) => Math.Log(a));
        }
        public static void RegisterOperator(string symbol, int arity, int priority, Func<double, double, double> evalFunc)
        {
            SupportedOperators[symbol] = new OperatorInfo(symbol, arity, priority, evalFunc);
        }
    }
}
