#if DEBUG
#endif

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicUsage.Benchmarks
{
#if DEBUG
    [TestFixture]
#endif
    public sealed class DynamicMethodCall
    {
        private readonly MyClass _instanceOne;
        private readonly MyAnotherClass _instanceTwo;
        private readonly DynamicMethodCaller<MyClass> _instanceOneCaller = new DynamicMethodCaller<MyClass>();
        private readonly DynamicMethodCaller<MyAnotherClass> _instanceTwoCaller = new DynamicMethodCaller<MyAnotherClass>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DynamicMethodCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

        public string Name
        {
            get { return "DynamicMethod.Emit call"; }
        }

        public void Benchmark()
        {
            _instanceOneCaller.InvokeMethod(_instanceOne);
            _instanceTwoCaller.InvokeMethod(_instanceTwo);
        }

#if DEBUG
        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOneCaller.InvokeMethod(_instanceOne));
            list.Add(_instanceTwoCaller.InvokeMethod(_instanceTwo));
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
#endif
    }

    internal class DynamicMethodCaller<T>
    {
        private readonly MethodInfo _method;
        private readonly Delegate _delegate;
        private readonly Func<T, int> _func;
        private DynamicMethod _dynamicMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DynamicMethodCaller()
        {
            _method = typeof(T).GetMethod("InvokeMethod");
            _dynamicMethod = new DynamicMethod("InvokeMethod", typeof(int), new[] { typeof(T) }, GetType().Module);
            ILGenerator il = _dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, _method);
            il.Emit(OpCodes.Ret);
            _func = (Func<T, int>)_dynamicMethod.CreateDelegate(typeof(Func<T, int>));
        }

        public int InvokeMethod(T instance)
        {
            return _func.Invoke(instance);
        }

        public int DynamicInvokeMethod(T instance)
        {
            return (int)_delegate.DynamicInvoke(instance);
        }
    }
}
