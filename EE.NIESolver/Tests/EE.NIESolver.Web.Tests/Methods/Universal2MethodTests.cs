using System;
using EE.NIESolver.MathNet;
using EE.NIESolver.MathNet.Services;
using EE.NIESolver.Solver.Runners;
using EE.NIESolver.Web.Methods;
using Xunit;

namespace EE.NIESolver.Web.Tests.Methods
{
    public class Universal2MethodTests
    {
        [Theory]
        [InlineData(40, 20, 2)]
        [InlineData(40, 40, 2)]
        [InlineData(40, 80, 2)]
        [InlineData(80, 80, 2)]
        [InlineData(160, 80, 3)]
        [InlineData(320, 160, 4)]
        public void OneSpatialVariable_ConstantDelay(int n, int m, int power)
        {
            var net = new MathNetFactory()
                .CreateMathNet2()
                .SetArea(2, 1)
                .SetInitialConditions(x => 0)
                .SetLeftBorder(x => 0)
                .SetHistory(1, (x, t) => t * Math.Sin(Math.PI * x))
                .Build(2d / n, 1d /  n);
            var fExpression = "f(x,t)=sin(pi*x)+pi*t*cos(pi*x)-(t-1)*sin(pi*x)+u(x,t-1)";
            var methodExpression = "(f(x-h/2,t-d/2)*2*h*d+(h-d)*(down-left))/(h+d)+v(-1,-1)";
            var method = new Universal2Method(methodExpression, fExpression, new LinearInterpolationService());

            var solver = new SequentialRunner(method);
            solver.Run(net);
            
            double ExpectedFunc(double x, double t) => t * Math.Sin(Math.PI * x);
            var result = double.MinValue;
            for (var j = 0; j <= net.Height; j++)
            for (var i = 0; i <= net.Width; i++)
            {
                var expected = ExpectedFunc(i * net.H, j * net.D);
                var error = Math.Abs(expected - net.Get(i, j));
                if (error > result)
                {
                    result = error;
                }
            }
            Assert.True(Math.Abs(result) < 0.5 * Math.Pow(10, -power),
                $"Ошибка {result:f10}, не соотвествует порядку {power})");
        }
    }
}
