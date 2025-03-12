using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class OperatorInfo
    {
        public string Symbol { get; }
        public int Arity { get; }
        public int Priority { get; }
        public Func<double, double, double> EvalFunc { get; }
        public OperatorInfo(string symbol, int arity, int priority, Func<double, double, double> evalFunc)
        {
            Symbol = symbol;
            Arity = arity;
            Priority = priority;
            EvalFunc = evalFunc;
        }
    }
}
