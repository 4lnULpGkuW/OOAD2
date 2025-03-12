using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class OperandNode : INode
    {
        private double? _value;
        private string _variable;
        public OperandNode(double value) { _value = value; }
        public OperandNode(string variable) { _variable = variable; }
        public double Evaluate(Dictionary<string, double> variables)
        {
            if (_value.HasValue) return _value.Value;
            if (variables.TryGetValue(_variable, out double val)) return val;
            throw new KeyNotFoundException($"Variable {_variable} has no value assigned");
        }
        public string ToInfix() { return _value.HasValue ? _value.Value.ToString() : _variable; }
    }
}
