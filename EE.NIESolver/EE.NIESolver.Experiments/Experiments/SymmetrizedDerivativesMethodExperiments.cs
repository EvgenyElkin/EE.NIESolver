using EE.NIESolver.MathNet;
using System;
using EE.NIESolver.MathNet.Services;
using EE.NIESolver.Solver;
using EE.NIESolver.Solver.Methods;
using EE.NIESolver.Solver.Runners;
using Xunit;
using Xunit.Abstractions;

namespace EE.NIESolver.Experiments.Experiments
{
    public class SymmetrizedDerivativesMethodExperiments : ExperimetBase
    {
        public SymmetrizedDerivativesMethodExperiments(ITestOutputHelper output) : base(output)
        {
            Log("ћетод симметризованных производных");
        }

        #region ќдна переменна€ по пространству, с посто€нным запаздыванием

        [Theory]
        [InlineData(40, 20, 2)]
        [InlineData(40, 40, 2)]
        [InlineData(40, 80, 2)]
        [InlineData(80, 80, 2)]
        [InlineData(160, 80, 3)]
        [InlineData(320, 160, 4)]
        //[InlineData(1280, 640, 5)]
        public void OneSpatialVariable_ConstantDelay(int n, int m, int power)
        {
            Log("ќдна переменна€ по пространству, с посто€нным запаздыванием");
            Log("x: (0, 2), t: (-1, 1)");
            Log($"N = {n}; M = {m}");

            var net = Inject<IMathNetFactory>()
                .CreateMathNet2()
                .SetArea(2, 1)
                .SetInitialConditions(x => 0)
                .SetLeftBorder(x => 0)
                .SetHistory(1, (x, t) => t * Math.Sin(Math.PI * x))
                .Build(2d / n, 1d / n);
            
            var method = new SymmetrizedDerivatedMethod(OneSpatialVariable_ConstantDelay_ExperimentFunction, new LinearInterpolationService());

            var solver = new ClassicRunner(method);
            solver.Run(net);

            AssertSolve(net, (x, t) => t * Math.Sin(Math.PI * x), power);
        }

        private static double OneSpatialVariable_ConstantDelay_ExperimentFunction(double x, double t, Func<double, double, double> u) => 
            Math.Sin(Math.PI * x) + Math.PI * t * Math.Cos(Math.PI * x) - (t - 1) * Math.Sin(Math.PI * x) + u(x, t - 1);

        #endregion

        #region ќдна переменна€ по пространству, с переменным запаздыванием

        [Theory]
        [InlineData(40, 20, 1)]
        [InlineData(40, 40, 1)]
        [InlineData(40, 80, 1)]
        [InlineData(80, 80, 1)]
        [InlineData(160, 80, 1)]
        [InlineData(320, 160, 2)]
        [InlineData(1280, 640, 2)]
        public void OneSpatialVariable_VariableDelay(int n, int m, int power)
        {
            Log("ќдна переменна€ по пространству, с переменным запаздыванием");
            Log("x: (0, 2), t: (-1, 1)");
            Log($"N = {n}; M = {m}");

            var net = Inject<IMathNetFactory>()
                .CreateMathNet2()
                .SetArea(2, 1)
                .SetInitialConditions(Math.Exp)
                .SetLeftBorder(t => Math.Exp(-t))
                .SetHistory(1, (x, t) => Math.Exp(x-t))
                .Build(2d / n, 1d / n);

            var method = new SymmetrizedDerivatedMethod(OneSpatialVariable_VariableDelay_ExperimentFunction, new LinearInterpolationService());
            var solver = new ClassicRunner(method);

            solver.Run(net);

            AssertSolve(net, (x, t) => Math.Exp(x - t), power);
        }

        private static double OneSpatialVariable_VariableDelay_ExperimentFunction(double x, double t, Func<double, double, double> u)
            => u(x, t) / u(x, t / 2) - Math.Exp(-t / 2);

        #endregion
    }
}
