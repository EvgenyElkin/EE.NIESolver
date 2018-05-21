using EE.NIESolver.MathNet;
using EE.NIESolver.Solver.Methods;

namespace EE.NIESolver.Solver.Runners
{
    public class ClassicRunner : I2Runner
    {
        private readonly I2Method _method;

        public ClassicRunner(I2Method method)
        {
            _method = method;
        }

        public void Run(MathNet2 net)
        {
            var p = new MathNet2Pointer(net);
            for (var j = 1; j <= net.Height; j++)
            {
                p.Set(1, j);
                for (var i = 1; i <= net.Width; i++)
                {
                    var value = _method.Calculate(p);
                    net.Set(i, j, value);
                    p.ToRight();
                }
            }
        }
    }
}
