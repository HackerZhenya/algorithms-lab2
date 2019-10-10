using System;
using System.Collections.Generic;
using System.Linq;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.utils;

namespace algorithms_lab2.ast.parser
{
    using ParserExpression = List<Token>;
    
    public class Parser
    {
        List<Expression> expressions;
        
        public Parser(List<Expression> expressions) => this.expressions = expressions;

        public Parser(Lexer lexer) : this(lexer.Tokenize()) {}

        public List<ParserExpression> Parse() => 
            expressions.Select(expression => ParseExpression(expression.Tokens).ToList()).ToList();

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

                            bool nextComma = false;
                            for (++idx, ++idx; tokens[idx].Type != lexer.TokenType.Rparen; ++idx)
                            {
                                if (nextComma)
                                {
                                    if (tokens[idx].Type != lexer.TokenType.Comma)
                                        throw new ArgumentException($"Expected comma, {tokens[idx]} given");

                                    nextComma = false;
                                    continue;
                                }

                                if (tokens[idx].Type == lexer.TokenType.Comma)
                                    throw new ArgumentException($"Expected argument, {tokens[idx]} given");

                                tokens[idx].AddTo(args);
                                nextComma = true;
                            }

                            yield return new Token(TokenType.Function, name, ParseExpression(args.ToArray()));
                        }
                        else yield return new Token(TokenType.Variable, tokens[idx].Value);

                        break;

                    case lexer.TokenType.Comma:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

            // return null;
        }
    }
}