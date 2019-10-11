using System;
using System.Collections.Generic;
using System.Linq;

namespace algorithms_lab2.runner
{
    public class Stack<T> : IStack<T>
    {
        readonly List<T> items = new List<T>();

        public void Push(T elem) => items.Add(elem);

        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack empty");

            var top = Top();
            items.RemoveAt(items.Count - 1);
            return top;
        }

        public T Pop(T def)
        {
            if (IsEmpty()) return def;

            var top = Top();
            items.RemoveAt(items.Count - 1);
            return top;
        }

        public T Top() =>
            !IsEmpty()
                ? items[items.Count - 1]
                : throw new InvalidOperationException("Stack empty");

        public bool IsEmpty() => items.Count == 0;

        public void Print() => Console.WriteLine(items.Aggregate("Print:", (a, b) => $"{a} {b}"));
    }
}