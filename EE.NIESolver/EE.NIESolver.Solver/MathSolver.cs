using EE.NIESolver.MathNet;
using EE.NIESolver.MathNet.Services;

namespace EE.NIESolver.Solver
{
    public delegate double OneVariableFunction(double x, double t, INetHistory u);

    public class MathSolver
    {
        private readonly OneVariableFunction _func;

        public MathSolver(OneVariableFunction function)
        {
            _func = function;
        }

        public void Solve(MathNet2WithHistory net, INetHistory history)
        {
            history = new CachedNetHistoryDecorator(net, history);
            var p = new MathNet2Pointer(net);
            for (var j = 1; j <= net.Height; j++)
            {
                for (var i = 1; i <= net.Width; i++)
                {
                    p.Set(i, j);

                    var value = (_func(p.X + net.H/2, p.T + net.D / 2, history) * 2 * net.H * net.D - (net.H - net.D) * (p.GetDown() - p.GetLeft())) /
                                (net.H + net.D) + p.GetValue(-1, -1);
                    net.Set(i, j, value);
                }
            }
        }
    }
}
