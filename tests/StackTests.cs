using System;
using algorithms_lab2.runner;
using NUnit.Framework;

namespace algorithms_lab2.tests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void General()
        {
            IStack<int> stack = new Stack<int>();

            stack.Push(0);
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Pop());
            Assert.AreEqual(1, stack.Top());
            stack.Pop();
            stack.Pop();
            Assert.IsTrue(stack.IsEmpty());
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }
    }
}