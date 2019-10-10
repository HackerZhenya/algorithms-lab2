using System.Collections.Generic;

namespace algorithms_lab2.ast.parser
{
    public class ArgumentBag
    {
        public List<Token> Bag { get; } = new List<Token>();

        Context context;

        public void AttachContext(Context ctx) => context = ctx;

        public double this[int index] => Bag[index].Evaluate(context);
    }
}