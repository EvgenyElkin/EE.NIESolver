using EE.NIESolver.MathNet.Tests.Infrastructure.Comparers;
using Xunit;

namespace EE.NIESolver.MathNet.Tests.Examples
{
    public class MathNet2Examples
    {
        private readonly IMathNetFactory _factory;

        public MathNet2Examples()
        {
            _factory = new MathNetFactory();
        }

        [Fact]
        public void Example_SimpleForAllConditions()
        {
            var net = _factory.CreateMathNet2()
                .SetArea(1, 1)
                .SetInitialConditions(x => x)
                .SetLeftBorder(x => x)
                .SetRightBorder(x => 1 + x)
                .Build(0.2d, 0.5d);

            var expected = new[,]
            {
                {0, 0.2, 0.4, 0.6, 0.8, 1},
                {0.5, 0, 0, 0, 0, 1.5},
                {1, 0, 0, 0, 0, 2}
            };

            Assert.Equal(expected, net.ToMatrix(), new MatrixComparer(10));
        }

        [Fact]
        public void Example_WithHistoryForAllConditions()
        {
            var net = _factory.CreateMathNet2()
                .SetArea(2, 2)
                .SetInitialConditions(x => -x)
                .SetLeftBorder(x => x)
                .SetRightBorder(x => 1 + x)
                .SetHistory(2, (x, y) =>  y * 10 - x)
                .Build(1, 1);

            var expected = new double[,]
            {
                { -20, -21, -22 },
                { -10, -11, -12 },
                { 0, -1, 1 },
                { 1, 0, 2 },
                { 2, 0, 3 },
            };

            Assert.Equal(expected, net.ToMatrix(), new MatrixComparer(10));
        }
    }
}
