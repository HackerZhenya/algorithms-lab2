using System;
using System.Collections.Generic;
using System.Linq;

namespace algorithms_lab2.ast.parser
{
    using Fn = Func<double[], double>;

    public class Context
    {
        public Dictionary<string, double> Variables { get; } = new Dictionary<string, double>
        {
            {"PI", Math.PI},
            {"E", Math.E}
        };

        public Dictionary<string, Fn> Functions { get; } = new Dictionary<string, Fn>
        {
            {"sin", args => Math.Sin(args[0])},
            {"cos", args => Math.Cos(args[0])},
            {"sqrt", args => Math.Sqrt(args[0])},
            {"ln", args => Math.Log(args[0])}
        };

        public double InvokeFn(string name, IEnumerable<Expression> arguments) =>
            Functions.ContainsKey(name)
                ? Functions[name].Invoke(arguments.Select(arg => arg.Evaluate(this)).ToArray())
                : throw new ArgumentException($"Undefined function \"{name}\"");
    }
}