using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using algorithms_lab2.ast.parser;

namespace algorithms_lab2.utils
{
    public static class Utils
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

        public static T Apply<T>(this T self, Action<T> fn)
        {
            fn(self);
            return self;
        }

        public static void Colored(this string text, ConsoleColor color)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = current;
        }

        public static void Performance(Action fn, Action<TimeSpan> print)
        {
            var sw = Stopwatch.StartNew();
            fn();
            sw.Stop();
            print(sw.Elapsed);
        }
    }
}