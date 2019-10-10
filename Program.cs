using System;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.ast.parser;
using algorithms_lab2.runner;

namespace algorithms_lab2
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Console.WriteLine("Hello world!");

            var lexer = new Lexer("./input.txt");
            var expressions = lexer.Tokenize();

            Console.WriteLine("Lexems:");
            foreach (var expression in expressions)
            {
                foreach (var token in expression) 
                    Console.Write(token.ToString() + ' ');

                Console.WriteLine();
            }
            
            var parser = new Parser(expressions);
            var runner = new Runner(parser);
        }

        public static void StackRunner(string input) => throw new NotImplementedException();

        public static void ParserRunner(string input)
        {
            
        }
    }
}