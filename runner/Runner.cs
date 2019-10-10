using System;
using System.Collections.Generic;
using System.Linq;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.ast.parser;
using Token = algorithms_lab2.ast.parser.Token;

namespace algorithms_lab2.runner
{
    using Expression = List<Token>;
    
    public class Runner
    {
        public Runner(string pathToSource) : this(new Parser(new Lexer(pathToSource))) {}

        public Runner(Parser parser) : this(parser.Parse().ToList()) {}

        public Runner(List<Expression> expressions)
        {
            Console.WriteLine("\n\n\nParser:");
            foreach (var expression in expressions)
            {
                foreach (var token in expression) 
                    Console.Write(token.ToString() + ' ');

                Console.WriteLine();
            }
            
            
        }
    }
}