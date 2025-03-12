using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class OperatorNode : INode
    {
        protected OperatorInfo _operatorInfo { get; }
        private readonly INode _left, _right;
        public OperatorNode(OperatorInfo operatorInfo, INode left, INode right = null)
        {
            _operatorInfo = operatorInfo;
            _left = left;
            _right = right;
        }
        public double Evaluate(Dictionary<string, double> variables)
        {
            double leftVal = _left.Evaluate(variables);
            double rightVal = _right?.Evaluate(variables) ?? 0;
            return _operatorInfo.EvalFunc(leftVal, rightVal);
        }
        public string ToInfix()
        {
            string leftExpr = _left.ToInfix();
            string rightExpr = _right?.ToInfix();

            if (_operatorInfo.Arity == 2)
            {
                if (_left is OperatorNode leftOp && leftOp._operatorInfo.Priority < _operatorInfo.Priority)
                    leftExpr = $"({leftExpr})";
                if (_right is OperatorNode rightOp && rightOp._operatorInfo.Priority <= _operatorInfo.Priority)
                    rightExpr = $"({rightExpr})";

                return $"{leftExpr} {_operatorInfo.Symbol} {rightExpr}";
            }
            else
            {
                if (_operatorInfo.Symbol == "neg" && _left is OperatorNode leftOp && leftOp._operatorInfo.Arity == 1)
                    return $"-{leftExpr}";
                else
                    return _operatorInfo.Symbol == "neg" ?
                        $"-({leftExpr})" :
                        $"{_operatorInfo.Symbol}({leftExpr})";
            }
        }
    }
}
