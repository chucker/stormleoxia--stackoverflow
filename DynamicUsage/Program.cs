using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using DynamicUsage.Benchmarks;

namespace DynamicUsage
{
    internal class Program
    {
        private static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                                                                    .Run(args);
    }

    [MemoryDiagnoser]
    public class DynamicUsageBenchmark
    {
        private ClassCall _ClassCall;
        private DelegateCall _DelegateCall;
        private DynamicCall _DynamicCall;
        private DynamicMethodCall _DynamicMethodCall;
        private ExpressionCall _ExpressionCall;
        private ExtensionMethodCall _ExtensionMethodCall;
        private GenericMethodCall _GenericMethodCall;
        private InterfaceCall _InterfaceCall;
        private MethodInfoCall _MethodInfoCall;
        private VisitorCall _VisitorCall;

        [GlobalSetup]
        public void Setup()
        {
            _ClassCall = new ClassCall();
            _DelegateCall = new DelegateCall();
            _DynamicCall = new DynamicCall();
            _DynamicMethodCall = new DynamicMethodCall();
            _ExpressionCall = new ExpressionCall();
            _ExtensionMethodCall = new ExtensionMethodCall();
            _GenericMethodCall = new GenericMethodCall();
            _InterfaceCall = new InterfaceCall();
            _MethodInfoCall = new MethodInfoCall();
            _VisitorCall = new VisitorCall();
        }

        [Benchmark(Baseline = true)]
        public void ClassCall() => _ClassCall.Benchmark();

        [Benchmark]
        public void DelegateCall() => _DelegateCall.Benchmark();

        [Benchmark]
        public void DynamicCall() => _DynamicCall.Benchmark();

        [Benchmark]
        public void DynamicMethodCall() => _DynamicMethodCall.Benchmark();

        [Benchmark]
        public void ExpressionCall() => _ExpressionCall.Benchmark();

        [Benchmark]
        public void ExtensionMethodCall() => _ExtensionMethodCall.Benchmark();

        [Benchmark]
        public void GenericMethodCall() => _GenericMethodCall.Benchmark();

        [Benchmark]
        public void InterfaceCall() => _InterfaceCall.Benchmark();

        [Benchmark]
        public void MethodInfoCall() => _MethodInfoCall.Benchmark();

        [Benchmark]
        public void VisitorCall() => _VisitorCall.Benchmark();
    }
}
