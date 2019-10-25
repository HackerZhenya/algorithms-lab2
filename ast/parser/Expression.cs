using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using algorithms_lab2.runner;
using algorithms_lab2.utils;

namespace algorithms_lab2.ast.parser
{
    public class Expression : IEnumerable<Token>
    {
        public Token[] Tokens { get; }

        public Expression(Token[] tokens) => Tokens = tokens;
        public Expression(List<Token> tokens) => Tokens = tokens.ToArray();

        public IEnumerator<Token> GetEnumerator() => ((IEnumerable<Token>) Tokens).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool ContainsEq() => Tokens.Any(token => token.Value == "=");

        public Token GetLvalue()
        {
            var expr = !ContainsEq()
                ? Tokens
                    .ToExpression()
                : Tokens
                    .TakeWhile(token => token.Value != "=")
                    .ToExpression();

            if (expr.Tokens.Length == 1 && expr.Tokens[0].Type == TokenType.Variable)
                return expr.Tokens[0];

            throw new InvalidOperationException("Wrong LValue! It must be a variable");
        }

        public Expression GetRvalue() =>
            !ContainsEq()
                ? Tokens
                    .ToExpression()
                : Tokens
                    .Reverse()
                    .TakeWhile(token => token.Value != "=")
                    .Reverse()
                    .ToExpression();

        public bool CanBeEvaluated(Context ctx) =>
            Tokens.All(token => token.CanBeEvaluated(ctx));

        public double Evaluate(Context ctx) =>
            PostfixNotation.Evaluate(ctx, PostfixNotation.ToPostfixNotation(Tokens));

        public override string ToString()
        {
            var tokens = PostfixNotation.ToPostfixNotation(Tokens);
            var sb = new StringBuilder();

            foreach (var token in tokens) sb.Append(token).Append(' ');

            return sb.ToString().Replace(" )", ")");
        }
    }
}