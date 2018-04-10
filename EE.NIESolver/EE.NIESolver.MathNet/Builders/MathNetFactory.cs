// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public class MathNetFactory : IMathNetFactory
    {
        public IMathNet2BuidlderEmpty CreateMathNet2()
        {
            return new MathNet2Builder();
        }
    }
}
