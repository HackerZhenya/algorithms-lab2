using System;
using System.Collections.Generic;
using System.Linq;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.runner;
using algorithms_lab2.utils;

namespace algorithms_lab2.ast.parser
{
    using LexerExpression = List<lexer.Token>;

    public class Parser
    {
        readonly List<LexerExpression> expressions;

        public Parser(List<LexerExpression> expressions) => this.expressions = expressions;

        public Parser(Lexer lexer) : this(lexer.Tokenize())
        {
        }

        public List<Expression> Parse() =>
            expressions
                .Select(expression => ParseExpression(expression).ToExpression())
                .Where(expression => expression.Tokens.Length > 0)
                .ToList();

        IEnumerable<Token> ParseExpression(IReadOnlyList<lexer.Token> tokens)
        {
            for (var idx = 0; idx < tokens.Count; idx++)
                switch (tokens[idx].Type)
                {
                    case lexer.TokenType.Number:
                        yield return new Token(TokenType.Constant, tokens[idx].Value);
                        break;

                    case lexer.TokenType.Plus:
                    case lexer.TokenType.Minus:
                    case lexer.TokenType.Multiply:
                    case lexer.TokenType.Divide:
                    case lexer.TokenType.Power:
                    case lexer.TokenType.Eq:
                    case lexer.TokenType.Lparen:
                    case lexer.TokenType.Rparen:
                        yield return new Token(TokenType.Operator, tokens[idx].Value);
                        break;

                    case lexer.TokenType.Word:
                        if (idx + 1 < tokens.Count && tokens[idx + 1].Type == lexer.TokenType.Lparen)
                        {
                            var name = tokens[idx].Value;
                            var args = new List<lexer.Token>();

                            idx += 2;

                            var brackets = 1;
                            while (true)
                            {
                                switch (tokens[idx].Type)
                                {
                                    case lexer.TokenType.Lparen:
                                        brackets++;
                                        break;
                                    case lexer.TokenType.Rparen:
                                        brackets--;
                                        break;
                                }

                                if (brackets > 0)
                                    tokens[idx++].AddTo(args);
                                else
                                    break;
                            }

                            yield return new Token(TokenType.Function, name, ParseArguments(args));
                        }
                        else yield return new Token(TokenType.Variable, tokens[idx].Value);

                        break;

                    case lexer.TokenType.Comma:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        IEnumerable<Expression> ParseArguments(IEnumerable<lexer.Token> tokens)
        {
            var expression = new List<lexer.Token>();

            foreach (var token in tokens)
            {
                if (token.Type != lexer.TokenType.Comma)
                {
                    expression.Add(token);
                    continue;
                }

                yield return ParseExpression(expression).ToExpression();
            }
            
            yield return ParseExpression(expression).ToExpression();
        }
    }
}