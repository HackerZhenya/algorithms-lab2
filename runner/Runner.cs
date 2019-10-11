using System;
using System.Collections.Generic;
using System.Linq;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.ast.parser;

namespace algorithms_lab2.runner
{
    public class Runner
    {
        readonly Context context = new Context();
        readonly List<Expression> expressions;
        readonly List<Expression> deferred = new List<Expression>();

        public Runner(string pathToSource) : this(new Parser(new Lexer(pathToSource)))
        {
        }

        public Runner(Parser parser) : this(parser.Parse())
        {
        }

        public Runner(List<Expression> expressions) => this.expressions = expressions;

        public void Run()
        {
            Console.WriteLine("\nRunner:");
            foreach (var expression in expressions)
            {
                if (expression.ContainsEq())
                {
                    var rvalue = expression.GetRvalue();

                    if (rvalue.CanBeEvaluated(context))
                    {
                        context.Variables[expression.GetLvalue().Value] = rvalue.Evaluate(context);
                        Console.WriteLine(
                            $"Variable \"{expression.GetLvalue().Value}\" set to {rvalue.Evaluate(context)}");
                    }
                    else
                        deferred.Add(expression);
                }
                else
                {
                    if (expression.CanBeEvaluated(context))
                        Console.WriteLine($"Expression \"{expression}\" is {expression.Evaluate(context)}");
                    else
                        deferred.Add(expression);
                }
            }

            int last = deferred.Count;
            while (deferred.Count > 0)
            {
                for (var idx = 0; idx < deferred.Count; idx++)
                {
                    var expression = deferred[idx];
                    if (expression.ContainsEq())
                    {
                        var rvalue = expression.GetRvalue();

                        if (!rvalue.CanBeEvaluated(context)) continue;

                        context.Variables[expression.GetLvalue().Value] = rvalue.Evaluate(context);
                        Console.WriteLine(
                            $"Variable \"{expression.GetLvalue().Value}\" set to {rvalue.Evaluate(context)}");
                        deferred.RemoveAt(idx);
                    }
                    else
                    {
                        if (!expression.CanBeEvaluated(context)) continue;

                        Console.WriteLine($"Expression \"{expression}\" is {expression.Evaluate(context)}");
                        deferred.RemoveAt(idx);
                    }
                }

                if (deferred.Count == last)
                    throw new Exception("Instructions cannot be followed!");

                last = deferred.Count;
            }
        }
    }
}