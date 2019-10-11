using System;
using algorithms_lab2.ast.lexer;
using algorithms_lab2.ast.parser;
using algorithms_lab2.runner;
using algorithms_lab2.utils;
using Utils = algorithms_lab2.utils.Utils;

namespace algorithms_lab2
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            StackRunner();
        }

        public static void StackRunner()
        {
            var rnd = new Random();
            var sizes = new[] {10, 100, 200, 300, 400};
            var tasks = "3 4 1,56 1,7 1,91 2 5 4".Split(' ');

            foreach (var size in sizes)
            {
                Utils.Performance(() =>
                {
                    var stack = new Stack<int>();
                    for (int sz = 0; sz < size; ++sz) stack.Push(rnd.Next(100));
                    foreach (var task in tasks)
                        switch (task)
                        {
                            case "2":
                                Console.WriteLine("Pop: " + stack.Pop());
                                break;

                            case "3":
                                Console.WriteLine("Top: " + stack.Top());
                                break;

                            case "4":
                                Console.WriteLine("IsEmpty: " + (stack.IsEmpty() ? "True" : "False"));
                                break;

                            case "5":
                                stack.Print();
                                break;

                            default: // 1
                                var n = task.Substring(2);
                                Console.WriteLine("Push: " + n);
                                stack.Push(int.Parse(n));
                                break;
                        }
                }, time => $"Collection size: {size} | Time: {time.Ticks} ticks".Colored(ConsoleColor.DarkYellow));

                Utils.Performance(
                    () => GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true),
                    time => $"GC Time: {time.Ticks} ticks\n\n".Colored(ConsoleColor.DarkCyan)
                );
            }
        }

        public static void PostfixNotationRunner()
        {
            try
            {
                var lexer = new Lexer("./input.txt");
                var parser = new Parser(lexer);
                var runner = new Runner(parser);

                runner.Run();
            }
            catch (Exception e)
            {
                $"\n{e.GetType().Name}: {e.Message}".Colored(ConsoleColor.DarkRed);
            }
        }
    }
}