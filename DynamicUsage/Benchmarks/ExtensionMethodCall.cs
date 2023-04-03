﻿#if DEBUG
#endif

using System.Collections.Generic;

namespace DynamicUsage.Benchmarks
{
#if DEBUG
    [TestFixture]
#endif
    public sealed class ExtensionMethodCall
    {
        private readonly MyClass _instanceOne;
        private readonly MyAnotherClass _instanceTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ExtensionMethodCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

        public void Benchmark()
        {
            _instanceOne.InvokeMethodEx();
            _instanceTwo.InvokeMethodEx();
        }

#if DEBUG
        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOne.InvokeMethodEx());
            list.Add(_instanceTwo.InvokeMethodEx());
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
#endif

        public string Name
        {
            get { return "Extension Method call"; }
        }
    }

    public static class Invoker
    {
        public static int InvokeMethodEx(this MyAnotherClass c)
        {
            return c.InvokeMethod();
        }

        public static int InvokeMethodEx(this MyClass c)
        {
            return c.InvokeMethod();
        }

    }
}