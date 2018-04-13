using System.Collections.Generic;

namespace EE.NIESolver.MathNet.Tests.Infrastructure.Comparers
{
    /// <summary>
    /// Определяет равенство матриц с определенной точностью, с возможностью транспонирования
    /// </summary>
    public class MatrixComparer : IEqualityComparer<double[,]>
    {
        private readonly EpsilonComparer _comparer;

        public MatrixComparer(int power)
        {
            _comparer = new EpsilonComparer(power);
        }

        public bool Equals(double[,] x, double[,] y)
        {
            if (x.GetLength(0) == y.GetLength(0) && x.GetLength(1) == y.GetLength(1))
            {
                for (var i = 0; i < x.GetLength(0); i++)
                for (var j = 0; j < x.GetLength(1); j++)
                {
                    if (!_comparer.Equals(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
                return true;
            }
            if (x.GetLength(0) == y.GetLength(1) && x.GetLength(0) == y.GetLength(1))
            {
                for (var i = 0; i < x.GetLength(0); i++)
                for (var j = 0; j < x.GetLength(1); j++)
                {
                    if (!_comparer.Equals(x[i, j], y[j, i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public int GetHashCode(double[,] obj)
        {
            var result = 0d;
            foreach (var item in obj)
            {
                result += item;
            }
            return (int)result;
        }
    }
}