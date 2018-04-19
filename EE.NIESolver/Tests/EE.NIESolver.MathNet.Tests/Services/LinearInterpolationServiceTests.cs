using EE.NIESolver.MathNet.Services;
using EE.NIESolver.MathNet.Tests.Infrastructure;
using EE.NIESolver.MathNet.Tests.Infrastructure.Comparers;
using Moq;
using Xunit;

namespace EE.NIESolver.MathNet.Tests.Services
{
    public class LinearInterpolationServiceTests : TestsBase
    {
        private readonly LinearInterpolationService _service;

        public LinearInterpolationServiceTests()
        {
            _service = new LinearInterpolationService();
        }

        #region Интерполяция по x

        [Theory]
        //Дискретные значения
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, -20)]
        [InlineData(2, 0, 20)]
        [InlineData(1, 1, -10)]
        //Положительные значения
        [InlineData(0.5, 0, 5)]
        [InlineData(0.7, 0, 7)]
        [InlineData(1.7, 0, 17)]
        //Отрицательные значения
        [InlineData(0.1, 1, -19)]
        [InlineData(1.1, 1, -9)]
        [InlineData(1.75, 1, -2.5)]
        public void InterpolateHorizontal_SetNet_TestCases(double x, int j, double expected)
        {
            var net = new MathNet2(2, 1, 1, 1);
            net.Set(0, 0, 0);
            net.Set(1, 0, 10);
            net.Set(2, 0, 20);

            net.Set(0, 1, -20);
            net.Set(1, 1, -10);
            net.Set(2, 1, 0);
            
            var result = _service.InterpolateHorizontal(net, x, j);

            Assert.Equal(expected, result, new EpsilonComparer(5));
        }

        #endregion

        #region Интерполяция по y

        [Theory]
        //Дискретные значения
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, -20)]
        [InlineData(2, 0, 20)]
        [InlineData(1, 1, -10)]
        //Положительные значения
        [InlineData(0.5, 0, 5)]
        [InlineData(0.7, 0, 7)]
        [InlineData(1.7, 0, 17)]
        //Отрицательные значения
        [InlineData(0.1, 1, -19)]
        [InlineData(1.1, 1, -9)]
        [InlineData(1.75, 1, -2.5)]
        public void InterpolateVertical_SetNet_TestCases(double t, int i, double expected)
        {
            var net = new MathNet2(1, 2, 1, 1);
            net.Set(0, 0, 0);
            net.Set(0, 1, 10);
            net.Set(0, 2, 20);

            net.Set(1, 0, -20);
            net.Set(1, 1, -10);
            net.Set(1, 2, 0);
            
            var result = _service.InterpolateVertical(net, i, t);

            Assert.Equal(expected, result, new EpsilonComparer(5));
        }

        #endregion
    }

    public class SymmetrizedDerivativesNetHistoryTests : TestsBase
    {
        private Mock<IInterpolationService> _interpolation;
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

            _interpolation.Setup(x => x.InterpolateVertical(_net, 1, 1.5)).Returns(1.5);
            _interpolation.Setup(x => x.InterpolateVertical(_net, 2, 1.5)).Returns(7.5);
            _interpolation.Setup(x => x.InterpolateHorizontal(_net, 1.5, 2)).Returns(7.5);

            var result = _history.Get(1.5, 2.5);

            Assert.Equal(7.5, result, new EpsilonComparer(5));
        }
    }
}
