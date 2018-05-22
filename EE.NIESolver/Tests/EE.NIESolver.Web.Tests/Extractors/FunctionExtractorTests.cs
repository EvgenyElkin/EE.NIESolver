using System;
using EE.NIESolver.Web.Extractions;
using Xunit;

namespace EE.NIESolver.Web.Tests.Extractors
{
    public class FunctionExtractorTests
    {
        private readonly IFunctionExtractor _extractor;

        public FunctionExtractorTests()
        {
            _extractor = new FunctionExtractor();
        }

        #region Извлечение одномерной функции
        
        [Theory]
        [InlineData("f(x)=x", 1.3303, 1.3303)]
        [InlineData("f(x)=x^2", 3, 9)]
        [InlineData("f(x)=sin(x)", Math.PI / 2, 1)]
        [InlineData("f(x)=cos(x)", 0, 1)]
        [InlineData("f(x)=exp(x)", 0, 1d)]
        [InlineData("f(x)=exp(x)", 1, Math.E)]
        [InlineData("f(x)=pi", 1, Math.PI)]
        public void ExtractR1Function_Success(string stringValue, double value, double expected)
        {
            var func = _extractor.ExtractR1Function(stringValue);
            var result = func(value);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("x")]
        [InlineData("f(x,y)=x^2")]
        [InlineData("f(x)=")]
        [InlineData("f()=sin()")]
        public void ExtractR1Function_Fail(string stringValue)
        {
            Assert.Throws<NotSupportedException>(() => _extractor.ExtractR1Function(stringValue));
        }

        #endregion

        #region Извлечение зависимой функции по двум переменным

        [Theory]
        [InlineData("f(x,t)=x+t+u(x,t-10)", 10, 10, 30)]
        [InlineData("f(x,t)=x-t+u(x-10,t-10)", 10, 10, 0)]
        public void ExtractDelayedR2Function_Succes(string stringValue, double x, double t, double expected)
        {
            var function = _extractor.ExtractDelayedR2Function(stringValue);
            var result = function(x, t, (a, b) => a + b);
            Assert.Equal(expected, result);
        }

        #endregion
    }
}
