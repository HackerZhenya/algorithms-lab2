using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using algorithms_lab2.ast;
using algorithms_lab2.ast.lexer;

namespace algorithms_lab2.utils
{
    public static class Extensions
    {
        public static Stream ToStream(this string str, Encoding enc = null)
        {
            enc = enc ?? Encoding.UTF8;
            return new MemoryStream(enc.GetBytes(str ?? ""), false);
        }

        public static T AddTo<T>(this T self, ICollection<T> collection)
        {
            collection.Add(self);
            return self;
        }
        
        public static Expression ToExpression(this IEnumerable<Token> tokens) => 
            new Expression(tokens.ToArray());
    }
}