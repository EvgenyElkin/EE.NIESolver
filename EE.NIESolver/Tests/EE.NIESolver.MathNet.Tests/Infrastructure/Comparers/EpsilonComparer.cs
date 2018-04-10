using System;
using System.Collections.Generic;

namespace EE.NIESolver.MathNet.Tests.Infrastructure.Comparers
{
    /// <summary>
    /// Определяет равенство с определенной точностью
    /// </summary>
    public class EpsilonComparer : IEqualityComparer<double>
    {
        private readonly double _epsilon;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="power">точность, в кол-вах знаков</param>
        public EpsilonComparer(int power)
        {
            _epsilon = Math.Pow(10, -power);
        }

        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) < 5 * _epsilon;
        }

        public int GetHashCode(double obj)
        {
            return 1;
        }
    }
}