using System.Collections.Generic;
using System.Threading.Tasks;
using EE.NIESolver.MathNet;
using EE.NIESolver.Solver.Methods;

namespace EE.NIESolver.Solver.Runners
{
    public class ParallelRunner : I2Runner
    {
        private readonly I2Method _method;

        public ParallelRunner(I2Method method)
        {
            _method = method;
        }

        public void Run(MathNet2 net)
        {
            for (var iteration = 1; iteration <= net.Width + net.Height - 1; iteration++)
            {
                var row = iteration <= net.Width ? 1 : iteration - net.Width + 1;
                var column = iteration <= net.Width ? iteration : net.Width;
                var tasks = new List<Task>();
                while (row <= net.Height && column >= 1)
                {
                    var column1 = column;
                    var row1 = row;
                    var task = Task.Run(() => CalculateNode(net, column1, row1));
                    tasks.Add(task);
                    column--;
                    row++;
                }

                Task.WaitAll(tasks.ToArray());
            }
        }

        private void CalculateNode(MathNet2 net, int row, int column)
        {
            var p = new MathNet2Pointer(net);
            p.Set(column, row);
            var value = _method.Calculate(p);
            net.Set(column, row, value);
        }
    }
}