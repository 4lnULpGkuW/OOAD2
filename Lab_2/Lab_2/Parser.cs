using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public static class Parser
    {
        public static INode ParsePrefix(string expression)
        {
            expression = expression.Replace(".", ",");
            string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Stack<INode> stack = new();

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];
                if (token == "-")
                {
                    INode left = stack.Pop();
                    if (stack.Count != 0)
                    {
                        INode right = stack.Pop();
                        stack.Push(new OperatorNode(OperatorRegistry.SupportedOperators["-"], left, right));
                    }
                    else
                    {
                        stack.Push(new OperatorNode(OperatorRegistry.SupportedOperators["neg"], left));
                    }
                }
                else if (OperatorRegistry.SupportedOperators.TryGetValue(token, out OperatorInfo operatorInfo))
                {
                    if (operatorInfo.Arity == 1)
                    {
                        INode operand = stack.Pop();
                        stack.Push(new OperatorNode(operatorInfo, operand));
                    }
                    else if (operatorInfo.Arity == 2)
                    {
                        INode left = stack.Pop();
                        INode right = stack.Pop();
                        stack.Push(new OperatorNode(operatorInfo, left, right));
                    }
                }
                else if (double.TryParse(token, out double number))
                    stack.Push(new OperandNode(number));
                else if (char.IsLetter(token[0]))
                    stack.Push(new OperandNode(token));
                else
                    throw new InvalidOperationException($"Unknown token: {token}");
            }
            return stack.Pop();
        }

    }
}
