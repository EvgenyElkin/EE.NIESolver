using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public class MathNet2Point : IEquatable<MathNet2Point>
    {
        public int I;
        public int J;
        public double X { get; set; }
        public double T { get; set; }

        public bool Equals(MathNet2Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X.Equals(other.I) && T.Equals(other.J);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MathNet2Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (I.GetHashCode() * 397) ^ J.GetHashCode();
            }
        }
    }

    public class MathNet2
    {
        protected readonly double[,] _net;

        /// <summary>
        /// Максимальное значение по пространству
        /// </summary>
        public double MaxX { get; }

        /// <summary>
        /// Максимальное значение по времени
        /// </summary>
        public double MaxT { get; }

        /// <summary>
        /// Шаг по пространству
        /// </summary>
        public double H { get; }

        /// <summary>
        /// Шаг по времени
        /// </summary>
        public double D { get; }

        /// <summary>
        /// Размер сетки по пространству
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Размер сетки по времени
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Конструктор, инициализирующий сетку по основным параметрам
        /// </summary>
        /// <param name="maxX">максимум по пространству</param>
        /// <param name="maxT">максимум по времени</param>
        /// <param name="h">шаг по пространству</param>
        /// <param name="d">шаг по времени</param>
        public MathNet2(double maxX, double maxT, double h, double d)
        {
            MaxX = maxX;
            MaxT = maxT;
            H = h;
            D = d;
            Width = (int) (maxX / h);
            Height = (int) (maxT / d);
            _net = new double[Width + 1, Height + 1];
        }

        /// <summary>
        /// Установить значение в ячейку сетки
        /// </summary>
        /// <param name="i">Номер ячейки по пространству</param>
        /// <param name="j">Номер ячейки по времени</param>
        /// <param name="value">Значение</param>
        public virtual void Set(int i, int j, double value)
        {
            if (i < 0 || i > Width || j < 0 || j > Height)
            {
                throw new ArgumentOutOfRangeException($"Элемент ({i},{j})  выходит за границу сетки {Width}x{Height}");
            }

            _net[i, j] = value;
        }

        /// <summary>
        /// Получить значение ячейки из сетки
        /// </summary>
        /// <param name="i">Номер ячейки по пространству</param>
        /// <param name="j">Номер ячейки по времени</param>
        /// <returns>Значение сетки в i,j</returns>
        public virtual double Get(int i, int j)
        {
            if (i < 0 || i > Width || j < 0 || j > Height)
            {
                throw new ArgumentOutOfRangeException($"Элемент ({i},{j})  выходит за границу сетки {Width}x{Height}");
            }

            return _net[i, j];
        }

        public virtual IEnumerable<MathNet2Point> GetPoints()
        {
            for (var j = 0; j <= Height; j++)
            for (var i = 0; i <= Width; i++)
            {
                yield return new MathNet2Point {X = i * H, T = j * D, I = i, J = j};
            }
        }

        public virtual double[,] ToMatrix()
        {
            return _net;
        }
    }
}