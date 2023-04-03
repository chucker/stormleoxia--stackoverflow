#if DEBUG
#endif

using System.Collections.Generic;

namespace DynamicUsage.Benchmarks
{
#if DEBUG
    [TestFixture]
#endif
    public sealed class DynamicCall
    {
        private readonly dynamic _instanceOne;
        private readonly dynamic _instanceTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DynamicCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
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

        public string Name { get { return "Keyword dynamic call"; } }
    }
}
