using EE.NIESolver.Web.Extractions.FunctionExtensions;
using org.mariuszgromada.math.mxparser;
using Xunit;

namespace EE.NIESolver.Web.Tests.Extractors.FunctionExtensions
{
    public class R2FunctionTests
    {

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(1,1,2)]
        [InlineData(1,-1,0)]
        [InlineData(100,-10,90)]
        public void Calculate_Succes(double x, double t, double expected)
        {
            var function = new Function("test", new R2Function((a, b) => a + b));
            function.setArgumentValue(0, x);
            function.setArgumentValue(1, t);
            var result = function.calculate();

            Assert.Equal(expected, result);
        }
    }
}
