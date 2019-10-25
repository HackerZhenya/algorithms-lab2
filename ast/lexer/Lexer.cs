using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using algorithms_lab2.utils;

namespace algorithms_lab2.ast.lexer
{
    using Expression = List<Token>;

    public class Lexer
    {
        readonly StreamReader stream;

        public Lexer(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read))
        {
        }

        public Lexer(Stream stream) => this.stream = new StreamReader(stream);

        public List<Expression> Tokenize()
        {
            var expressions = new List<Expression>();

            while (!stream.EndOfStream)
                TokenizeExpression().AddTo(expressions);

            return expressions;
        }

        Expression TokenizeExpression()
        {
            var tokens = new List<Token>();

            while (stream.Peek() != '\n' && !stream.EndOfStream)
            {
                char ch = (char) stream.Peek();

                if (char.IsDigit(ch))
                    TokenizeNumber().AddTo(tokens);

                else if (Utils.IsOperator(ch))
                    TokenizeOperator().AddTo(tokens);

                else if (char.IsLetterOrDigit(ch))
                    TokenizeWord().AddTo(tokens);

                else
                    stream.Read();
            }

            stream.Read();
            return tokens;
        }

        Token TokenizeNumber()
        {
            var sb = new StringBuilder();
            bool dot = false;
            while (char.IsDigit((char) stream.Peek()) || stream.Peek() == '.')
            {
                if (stream.Peek() == '.')
                    dot = !dot ? true : throw new ArgumentException("Invalid number");

                sb.Append((char) stream.Read());
            }

            return new Token(TokenType.Number, sb.ToString());
        }

        Token TokenizeOperator()
        {
            char op = (char) stream.Read();

            switch (op)
            {
                case '+':
                    return new Token(TokenType.Plus, op);

                case '-':
                    return new Token(TokenType.Minus, op);

                case '*':
                    return new Token(TokenType.Multiply, op);

                case '/':
                case ':':
                    return new Token(TokenType.Divide, op);

                case '^':
                    return new Token(TokenType.Power, op);

                case '(':
                    return new Token(TokenType.Lparen, op);

                case ')':
                    return new Token(TokenType.Rparen, op);

                case ',':
                    return new Token(TokenType.Comma, op);

                case '=':
                    return new Token(TokenType.Eq, op);

                default:
                    throw new ArgumentOutOfRangeException($"Undefined operator: \"{op}\"");
            }
        }

        Token TokenizeWord()
        {
            var sb = new StringBuilder();
            while (char.IsLetterOrDigit((char) stream.Peek()))
                sb.Append((char) stream.Read());

            return new Token(TokenType.Word, sb.ToString());
        }
    }
}