using System;
using System.Collections.Generic;

namespace algorithms_lab2.ast.parser
{
    public class Context
    {
        public Dictionary<string, double> Variables { get; } = new Dictionary<string, double>
        {
            {"PI", Math.PI},
            {"E", Math.E}
        };

        public Dictionary<string, Func<ArgumentBag, double>> Functions { get; } =
            new Dictionary<string, Func<ArgumentBag, double>>
            {
                
            };
    }
}