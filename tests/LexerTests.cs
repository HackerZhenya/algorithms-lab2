using System.Collections.Generic;
using algorithms_lab2.ast;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.utils;
using NUnit.Framework;

namespace algorithms_lab2.tests
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void Test1()
        {
            AssertExpression("2 + 2", new[]
            {
                new Token(TokenType.Number, "2"),
                new Token(TokenType.Plus, '+'),
                new Token(TokenType.Number, "2"),
            });
        }

        static void AssertExpression(string expression, IEnumerable<Token> tokens) => 
            CollectionAssert.AreEqual(tokens, new Lexer(expression.ToStream()).Tokenize());
    }
}