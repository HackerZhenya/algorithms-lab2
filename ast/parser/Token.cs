using System;
using System.Collections.Generic;
using System.Linq;

namespace algorithms_lab2.ast.parser
{
    public class Token
    {
        public TokenType Type { get; }

        public string Value { get; }

        public List<Token> Arguments { get; }

        public Token(TokenType type, string value, IEnumerable<Token> argumants = null)
        {
            Type = type;
            Value = value;

            if (argumants != null)
                Arguments = argumants.ToList();
        }

        public bool CanBeEvaluated(Context ctx)
        {
            switch (Type)
            {
                case TokenType.Constant:
                case TokenType.Operator:
                    return true;

                case TokenType.Variable:
                    return ctx.Variables.ContainsKey(Value);

                case TokenType.Function:
                    return Arguments.All(token => token.CanBeEvaluated(ctx));

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public double Evaluate(Context ctx = null)
        {
            switch (Type)
            {
                case TokenType.Variable:
                    return ctx?.Variables[Value] ?? throw new InvalidOperationException();

                case TokenType.Constant:
                    return double.Parse(Value);

                case TokenType.Operator:
                    throw new InvalidOperationException();

                case TokenType.Function:
                    return ctx?.InvokeFn(Value, Arguments) ?? throw new InvalidOperationException();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString() =>
            Value == null
                ? $"[{Type.ToString()}]"
                : Type == TokenType.Function
                    ? $"[{Type.ToString()} \"{Value}\" of{Arguments.Aggregate("", (a, b) => $"{a} {b}")}]"
                    : $"[{Type.ToString()} \"{Value}\"]";
    }
}