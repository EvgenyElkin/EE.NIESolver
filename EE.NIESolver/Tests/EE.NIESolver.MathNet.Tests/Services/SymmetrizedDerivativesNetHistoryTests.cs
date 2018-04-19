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

            _interpolation.Setup(x => x.InterpolateVertical(_net, 1, 1.5)).Returns(1.5);
            _interpolation.Setup(x => x.InterpolateVertical(_net, 2, 1.5)).Returns(7.5);
            _interpolation.Setup(x => x.InterpolateHorizontal(_net, 1.5, 2)).Returns(7.5);

            var result = _history.Get(1.5, 2.5);

            Assert.Equal(7.5, result, new EpsilonComparer(5));
        }
    }
}