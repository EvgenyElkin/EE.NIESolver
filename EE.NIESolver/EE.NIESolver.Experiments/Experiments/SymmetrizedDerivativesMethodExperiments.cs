using EE.NIESolver.MathNet;
using System;
using EE.NIESolver.MathNet.Services;
using EE.NIESolver.Solver;
using Xunit;
using Xunit.Abstractions;

namespace EE.NIESolver.Experiments.Experiments
{
    public class SymmetrizedDerivativesMethodExperiments : ExperimetBase
    {
        public SymmetrizedDerivativesMethodExperiments(ITestOutputHelper output) : base(output)
        {
            Log("Метод симметризованных производных");
        }

        #region Одна переменная по пространству, с постоянным запаздыванием

        [Theory]
        [InlineData(40, 20, 2)]
        [InlineData(40, 40, 2)]
        [InlineData(40, 80, 2)]
        [InlineData(80, 80, 2)]
        [InlineData(160, 80, 3)]
        [InlineData(320, 160, 4)]
        public void OneSpatialVariable_ConstantDelay(int n, int m, int power)
        {
            Log("Одна переменная по пространству, с постоянным запаздыванием");
            Log("x: (0, 2), t: (-1, 1)");
            Log($"N = {n}; M = {m}");

            var net = Inject<IMathNetFactory>()
                .CreateMathNet2()
                .SetArea(2, 1)
                .SetInitialConditions(x => 0)
                .SetLeftBorder(x => 0)
                .SetHistory(1, (x, t) => t * Math.Sin(Math.PI * x))
                .Build(2d / n, 1d / n);

            var solver = new MathSolver(OneSpatialVariable_ConstantDelay_ExperimentFunction);
            var history = new SymmetrizedDerivativesNetHistory(net, new LinearInterpolationService());

            solver.Solve(net, history);

            AssertSolve(net, (x, t) => t * Math.Sin(Math.PI * x), power);
        }

        private static double OneSpatialVariable_ConstantDelay_ExperimentFunction(double x, double t, INetHistory u) => 
            Math.Sin(Math.PI * x) + Math.PI * t * Math.Cos(Math.PI * x) - (t - 1) * Math.Sin(Math.PI * x) + u.Get(x, t - 1);

        #endregion

        #region Одна переменная по пространству, с переменным запаздыванием

        [Theory]
        [InlineData(40, 20, 1)]
        [InlineData(40, 40, 1)]
        [InlineData(40, 80, 1)]
        [InlineData(80, 80, 1)]
        [InlineData(160, 80, 1)]
        [InlineData(320, 160, 2)]
        public void OneSpatialVariable_VariableDelay(int n, int m, int power)
        {
            Log("Одна переменная по пространству, с переменным запаздыванием");
            Log("x: (0, 2), t: (-1, 1)");
            Log($"N = {n}; M = {m}");

            var net = Inject<IMathNetFactory>()
                .CreateMathNet2()
                .SetArea(2, 1)
                .SetInitialConditions(Math.Exp)
                .SetLeftBorder(t => Math.Exp(-t))
                .SetHistory(1, (x, t) => Math.Exp(x-t))
                .Build(2d / n, 1d / n);

            var solver = new MathSolver(OneSpatialVariable_VariableDelay_ExperimentFunction);
            var history = new SymmetrizedDerivativesNetHistory(net, new LinearInterpolationService());

            solver.Solve(net, history);

            AssertSolve(net, (x, t) => Math.Exp(x - t), power);
        }

        private static double OneSpatialVariable_VariableDelay_ExperimentFunction(double x, double t, INetHistory u) =>
            u.Get(x,t) / u.Get(x, t/2) - Math.Exp(-t/2);

        #endregion

        #region Одна переменная по пространству, с интегральным запаздыванием

        [Theory]
        [InlineData(40, 20, 1)]
        [InlineData(40, 40, 1)]
        [InlineData(40, 80, 1)]
        [InlineData(80, 80, 1)]
        [InlineData(160, 80, 1)]
        [InlineData(320, 160, 2)]
        public void OneSpatialVariable_IntegralDelay(int n, int m, int power)
        {
            Log("Одна переменная по пространству, с интегральным запаздыванием");
            Log("x: (0, 2), t: (-1, 1)");
            Log($"N = {n}; M = {m}");

            var net = Inject<IMathNetFactory>()
                .CreateMathNet2()
                .SetArea(2, 1)
                .SetInitialConditions(x => 0)
                .SetLeftBorder(t => Math.Sin(Math.PI * t))
                .SetHistory(0.5, (x, t) => Math.Exp(x) * Math.Sin(Math.PI * t))
                .Build(2d / n, 1d / n);

            var solver = new MathSolver(OneSpatialVariable_IntegralDelay_ExperimentFunction);
            var history = new SymmetrizedDerivativesNetHistory(net, new LinearInterpolationService());

            solver.Solve(net, history, 1d / Math.PI);

            AssertSolve(net, (x, t) => Math.Exp(x) * Math.Sin(Math.PI * t), power);
        }

        private static double OneSpatialVariable_IntegralDelay_ExperimentFunction(double x, double t, INetHistory u) =>
            u.Integral(x, t-0.5, t) + Math.Exp(x) * (Math.PI + 1 / Math.PI) * Math.Cos(Math.PI * t);


        #endregion
    }
}
