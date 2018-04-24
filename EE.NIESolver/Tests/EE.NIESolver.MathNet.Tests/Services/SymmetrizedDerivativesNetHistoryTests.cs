using EE.NIESolver.MathNet.Interfaces;
using EE.NIESolver.MathNet.Services;
using EE.NIESolver.MathNet.Tests.Infrastructure;
using EE.NIESolver.MathNet.Tests.Infrastructure.Comparers;
using Moq;
using Xunit;

namespace EE.NIESolver.MathNet.Tests.Services
{
    public class SymmetrizedDerivativesNetHistoryTests : TestsBase
    {
        private readonly Mock<IInterpolationService> _interpolation;
        private readonly MathNet2WithHistory _net;
        private readonly SymmetrizedDerivativesNetHistory _history;

        public SymmetrizedDerivativesNetHistoryTests()
        {
            _interpolation = Setup<IInterpolationService>();
            _net = new MathNet2WithHistory(2, 2, 2, 1, 1);
            _history = new SymmetrizedDerivativesNetHistory(_net, _interpolation.Object);
        }

        /// <summary>
        /// Тестирование двойного оператора интерполяции-экстраполяции - случай из истории
        /// Задана матрица c историей:
        ///  t
        ///  |  [0, 0, 0 ]
        ///  |  [0, 0, 0 ]
        ///  |  [0, 0, 0 ]
        /// 0|--[0, 1, 2 ]--x
        ///  |  [0, 5, 10]
        /// Пытаемся вычислить значение (x,y) = (1.5, -0.5)
        /// </summary>
        [Fact]
        public void Get_SetValueFromHistory_OptimizatedCalculation()
        {
            _net.Set(1, 0, 1);
            _net.Set(2, 0, 2);
            _net.Set(1, -1, 5);
            _net.Set(2, -1, 10);

            _interpolation.Setup(x => x.InterpolateVertical(_net, 1, -0.5)).Returns(1.5);
            _interpolation.Setup(x => x.InterpolateVertical(_net, 2, -0.5)).Returns(7.5);

            var result = _history.Get(1.5, -0.5);

            Assert.Equal(4.5, result, new EpsilonComparer(5));
        }

        /// <summary>
        /// Тестирование двойного оператора интерполяции-экстраполяции
        /// Задана матрица c историей:
        ///  t
        ///  |  [0, 0, 0 ]
        ///  |  [0, 5, 10]
        ///  |  [0, 1, 2 ]
        /// 0|--[0, 0, 0 ]--x
        ///  |  [0, 0, 0 ]
        /// Пытаемся вычислить значение (x,y) = (1.5, 2.5)
        /// </summary>
        [Fact]
        public void Get_SetValueFromNet_FullCalculation()
        {
            _net.Set(1, 1, 1);
            _net.Set(2, 1, 2);
            _net.Set(1, 2, 5);
            _net.Set(2, 2, 10);

            _interpolation.Setup(x => x.InterpolateVertical(_net, 1, 1.5)).Returns(3);
            _interpolation.Setup(x => x.InterpolateVertical(_net, 2, 1.5)).Returns(6);
            _interpolation.Setup(x => x.InterpolateHorizontal(_net, 1.5, 2)).Returns(7.5);

            var result = _history.Get(1.5, 2.5);

            Assert.Equal(10.5, result, new EpsilonComparer(5));
        }

        [Theory]
        [InlineData(0.5, 1.5, 2)]
        [InlineData(0.5, 1.25, 1.75)]
        [InlineData(0.5, 1.75, 2.25)]
        [InlineData(0.5, 1.001, 1.5)]
        [InlineData(0.5, 1.999, 2.5)]
        public void Get_SetInterpolationService_IntegrationTest(double xValue, double yValue, double expected)
        {
            _net.Set(0, 0, 0);
            _net.Set(1, 0, 1);
            _net.Set(0, 1, 1);
            _net.Set(1, 1, 2);

            var interpolation = new LinearInterpolationService();
            _interpolation.Setup(s => s.InterpolateVertical(_net, It.IsAny<int>(), It.IsAny<double>()))
                .Returns((MathNet2 net, int i, double y) => interpolation.InterpolateVertical(net, i, y));
            _interpolation.Setup(s => s.InterpolateHorizontal(_net,  It.IsAny<double>(), It.IsAny<int>()))
                .Returns((MathNet2 net, double x, int j) => interpolation.InterpolateHorizontal(net, x, j));

            var result = _history.Get(xValue, yValue);

            Assert.Equal(expected, result, new EpsilonComparer(1));
        }
    }
}