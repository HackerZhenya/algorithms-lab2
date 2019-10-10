using System.Collections;
using System.Collections.Generic;
using System.Linq;
using algorithms_lab2.utils;

namespace algorithms_lab2.ast.lexer
{
    public class Expression : IEnumerable<Token>
    {
        public Token[] Tokens { get; }

        public Expression(Token[] tokens) => Tokens = tokens;
        public Expression(List<Token> tokens) => Tokens = tokens.ToArray();

        public IEnumerator<Token> GetEnumerator() => ((IEnumerable<Token>) Tokens).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /*public bool ContainsEq() => Tokens.Any(token => token.Type == TokenType.Eq);

        public Expression GetLvalue() =>
            !ContainsEq()
                ? Tokens
                    .ToExpression()
                : Tokens
                    .TakeWhile(token => token.Type != TokenType.Eq)
                    .ToExpression();

        public Expression GetRvalue() => 
            !ContainsEq() 
                ? Tokens
                    .ToExpression() 
                : Tokens
                    .Reverse()
                    .TakeWhile(token => token.Type != TokenType.Eq)
                    .Reverse()
                    .ToExpression();

        public bool IsConstant() =>
            !Tokens.Any(it => new[]
            {
                TokenType.Plus, 
                TokenType.Minus,
                TokenType.Multiply,
                TokenType.Divide
            }.Contains(it.Type));*/
    }
}