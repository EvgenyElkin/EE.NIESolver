using Moq;

namespace EE.NIESolver.MathNet.Tests.Infrastructure
{
    public abstract class TestsBase
    {
        protected Mock<TMock> Setup<TMock>()
            where TMock : class
        {
            return new Mock<TMock>();
        }
    }
}
