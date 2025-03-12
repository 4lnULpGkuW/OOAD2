using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public interface INode
    {
        public abstract double Evaluate(Dictionary<string, double> variables);
        public abstract string ToInfix();
    }
}
