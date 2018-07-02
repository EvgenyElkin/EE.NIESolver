using EE.NIESolver.Solver.Methods;
using EE.NIESolver.Solver.Runners;

namespace EE.NIESolver.Web.Factories
{
    public class RunnerFactory
    {
        public I2Runner Create2Runner(int runnerType, I2Method method)
        {
            return new SequentialRunner(method);
        }
    }
}