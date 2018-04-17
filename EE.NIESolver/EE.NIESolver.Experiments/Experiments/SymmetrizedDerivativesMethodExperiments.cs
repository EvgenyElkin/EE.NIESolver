using EE.NIESolver.MathNet;
using System;
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
        [InlineData(40, 20, 0.06128)]
        [InlineData(40, 40, 0.03349)]
        [InlineData(40, 80, 0.0195)]
        [InlineData(80, 80, 0.01576)]
        [InlineData(160, 80, 0.01474)]
        [InlineData(320, 160, 0.00732)]
        public void OneSpatialVariable_ConstantDelay(int n, int m, double expected)
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
                .Build((double) 1 / n, (double) 1 / n);

            var solver = new MathSolver(OneSpatialVariable_ConstantDelay_ExperimentFunction);

            solver.Solve(net);

            AssertSolve(net, (x, t) => t * Math.Sin(Math.PI * x), expected);
        }

        private static double OneSpatialVariable_ConstantDelay_ExperimentFunction(I2Pointer p) => 
            Math.Sin(Math.PI * p.X) + Math.PI * p.T * Math.Cos(p.X) - (p.T - 1) * Math.Sin(Math.PI * p.X) + p.GetDown();

        #endregion
    }
}
