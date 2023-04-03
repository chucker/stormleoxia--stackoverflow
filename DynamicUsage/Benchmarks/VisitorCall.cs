#if DEBUG
#endif

using System.Collections.Generic;

namespace DynamicUsage.Benchmarks
{
#if DEBUG
    [TestFixture]
#endif
    public sealed class VisitorCall
    {
        private readonly MyClass _instanceOne;
        private readonly MyAnotherClass _instanceTwo;
        private readonly Visitor _visitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public VisitorCall()
        {
            _visitor = new Visitor();
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

        public string Name
        {
            get { return "Visitor Accept/Visit call"; }
        }

        public void Benchmark()
        {
            _instanceOne.Accept(_visitor);
            _instanceTwo.Accept(_visitor);
        }

#if DEBUG
        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOne.Accept(_visitor));
            list.Add(_instanceTwo.Accept(_visitor));
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
#endif
    }



    public class Visitor : IVisitor
    {
        public int Visit(MyAnotherClass c)
        {
            return c.InvokeMethod();
        }

        public int Visit(MyClass c)
        {
            return c.InvokeMethod();
        }
    }

    public interface IVisitor
    {
        int Visit(MyClass c);
        int Visit(MyAnotherClass c);
    }
}