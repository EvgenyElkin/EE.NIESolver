using EE.NIESolver.MathNet.Services;
using EE.NIESolver.MathNet.Tests.Infrastructure;
using EE.NIESolver.MathNet.Tests.Infrastructure.Comparers;
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
        [InlineData(0, 0, 5, 10, 5)]
        [InlineData(0, 1, 5, 10, 10)]
        [InlineData(-1, -1, 10, 5, 10)]
        //Правая интерполяция
        [InlineData(0, 0.5, 5, 10, 7.5)]
        [InlineData(0, 0.1, 5, 10, 5.5)]
        [InlineData(0, 0.9, 5, 10, 9.5)]
        [InlineData(2, 2.1, 5, 10, 5.5)]
        //Левая интерполяция
        [InlineData(-1, -0.5, 10, 5, 7.5)]
        [InlineData(-1, -0.1, 10, 5, 5.5)]
        [InlineData(-1, -0.9, 10, 5, 9.5)]
        public void InterpolateX_SetNet_TestCases(int i, double dx, double left, double right, double expected)
        {
            var pointer = Setup<I2Pointer>();
            pointer.Setup(x => x.GetValue(i, 0)).Returns(left);
            pointer.Setup(x => x.GetValue(i + 1, 0)).Returns(right);

            var result = _service.InterpolateX(pointer.Object, dx);

            Assert.Equal(expected, result, new EpsilonComparer(5));
        }

        #endregion

        #region Интерполяция по y

        [Theory]
        //Дискретные значения
        [InlineData(0, 0, 5, 10, 5)]
        [InlineData(0, 1, 5, 10, 10)]
        [InlineData(-1, -1, 10, 5, 10)]
        //Верхняя интерполяция
        [InlineData(0, 0.5, 5, 10, 7.5)]
        [InlineData(0, 0.1, 5, 10, 5.5)]
        [InlineData(0, 0.9, 5, 10, 9.5)]
        [InlineData(2, 2.1, 5, 10, 5.5)]
        //Нижняя интерполяция
        [InlineData(-1, -0.5, 10, 5, 7.5)]
        [InlineData(-1, -0.1, 10, 5, 5.5)]
        [InlineData(-1, -0.9, 10, 5, 9.5)]
        public void InterpolateY_SetNet_TestCases(int i, double dx, double bottom, double top, double expected)
        {
            var pointer = Setup<I2Pointer>();
            pointer.Setup(x => x.GetValue(0, i)).Returns(bottom);
            pointer.Setup(x => x.GetValue(0, i + 1)).Returns(top);

            var result = _service.InterpolateY(pointer.Object, dx);

            Assert.Equal(expected, result, new EpsilonComparer(5));
        }

        #endregion
    }
}
