using System;
using System.Collections.Generic;
using EE.NIESolver.MathNet;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace EE.NIESolver.Experiments
{
    public abstract class ExperimetBase
    {
        private readonly IServiceProvider _container;
        private readonly ITestOutputHelper _output;

        protected ExperimetBase(ITestOutputHelper output)
        {
            _output = output;
            _container = InitServices();
        }

        private IServiceProvider InitServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped<IMathNetFactory, MathNetFactory>();

            ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        #region Зависимости

        protected virtual IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        protected TDependency Inject<TDependency>()
        {
            return _container.GetService<TDependency>();
        }

        protected IEnumerable<TDependency> InjectAll<TDependency>()
        {
            return _container.GetServices<TDependency>();
        }

        #endregion

        #region Проверка значений

        protected void AssertSolve(MathNet2 net, Func<double, double, double> expectedFunc, double expectedError, int power = 5)
        {
            var result = double.MinValue;
            for (var j = 0; j <= net.Height; j++)
            for (var i = 0; i <= net.Width; i++)
            {
                var expected = expectedFunc(i * net.H, j * net.D);
                var error = Math.Abs(expected - net.Get(i, j));
                if (error > result)
                {
                    result = error;
                }
            }
            Assert.True(Math.Abs(result - expectedError) < 0.5 * Math.Pow(10, -power), 
                $"Ошибка {result}, не соотвествует {expectedError}( c точностью {power})");
        }

        protected void Log(string message)
        {
            _output.WriteLine(message);
        }

        #endregion
    }
}
