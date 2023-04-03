#if DEBUG
#endif

using System.Collections.Generic;

namespace DynamicUsage.Benchmarks
{
#if DEBUG
    [TestFixture]
#endif
    public sealed class InterfaceCall
    {
        private readonly IInvoker _instanceOne;
        private readonly IInvoker _instanceTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public InterfaceCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

        public string Name
        {
            get { return "Interface virtual call"; }
        }

        public void Benchmark()
        {
            _instanceOne.InvokeMethod();
            _instanceTwo.InvokeMethod();
        }

#if DEBUG
        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOne.InvokeMethod());
            list.Add(_instanceTwo.InvokeMethod());
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
#endif
    }
}