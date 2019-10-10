using System.Linq;

namespace algorithms_lab2.ast.lexer
{
    public class Utils
    {
        public static bool IsOperator(char ch) =>
            new[] {'+', '-', '*', ':', '/', '^', '=', '(', ')', ','}.Contains(ch);
    }
}