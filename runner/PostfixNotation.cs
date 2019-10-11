using System;
using System.Collections.Generic;
using algorithms_lab2.ast.parser;

namespace algorithms_lab2.runner
{
    public class PostfixNotation
    {
        static readonly Dictionary<string, int> priorities = new Dictionary<string, int>
        {
            {"+", 0}, {"-", 0},
            {"*", 1}, {"/", 1}, {":", 1},
            {"^", 2},
            {"(", -1}, {")", -1}
        };

        static int GetPriority(Token token) => priorities[token.Value];

        public static IEnumerable<Token> ToPostfixNotation(IEnumerable<Token> tokens)
        {
            var operations = new Stack<Token>();

            foreach (var token in tokens)
                switch (token.Type)
                {
                    case TokenType.Constant:
                    case TokenType.Variable:
                    case TokenType.Function:
                        yield return token;
                        break;

                    case TokenType.Operator:

                        if (token.Value == ")")
                        {
                            while (!operations.IsEmpty() && operations.Top().Value != "(")
                                yield return operations.Pop();

                            operations.Pop(); // (
                            break;
                        }

                        if (operations.IsEmpty() || token.Value == "(" ||
                            GetPriority(token) > GetPriority(operations.Top()))
                            operations.Push(token);
                        else if (GetPriority(token) <= GetPriority(operations.Top()))
                        {
                            while (!operations.IsEmpty() && operations.Top().Value != "(")
                                yield return operations.Pop();

                            operations.Push(token);
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

            while (!operations.IsEmpty())
                yield return operations.Pop();
        }

        public static double Evaluate(Context ctx, IEnumerable<Token> tokens)
        {
            var operands = new Stack<double>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Variable:
                    case TokenType.Constant:
                    case TokenType.Function:
                        operands.Push(token.Evaluate(ctx));
                        break;

                    case TokenType.Operator:
                        double b = operands.Pop(0),
                            a = operands.Pop(0);

                        switch (token.Value)
                        {
                            case "+":
                                operands.Push(a + b);
                                break;

                            case "-":
                                operands.Push(a - b);
                                break;

                            case "*":
                                operands.Push(a * b);
                                break;

                            case "/":
                            case ":":
                                operands.Push(a / b);
                                break;

                            case "^":
                                operands.Push(Math.Pow(a, b));
                                break;

                            default:
                                throw new InvalidOperationException();
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return operands.Pop();
        }
    }
}