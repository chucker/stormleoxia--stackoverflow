#if DEBUG
#endif

using System.Collections.Generic;

namespace DynamicUsage.Benchmarks
{
#if DEBUG
    [TestFixture]
#endif
    public sealed class GenericMethodCall
    {
        private readonly MyClass _instanceOne;
        private readonly MyAnotherClass _instanceTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public GenericMethodCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

        public string Name
        {
            get { return "Generic Method call"; }
        }

        public void Benchmark()
        {
            Caller.Invoke(_instanceOne);
            Caller.Invoke(_instanceTwo);
        }

#if DEBUG
        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(Caller.Invoke(_instanceOne));
            list.Add(Caller.Invoke(_instanceTwo));
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
#endif
    }

    public static class Caller
    {
        public static int Invoke<T>(T instance) where T : IInvoker
        {
            return instance.InvokeMethod();
        }
    }
}