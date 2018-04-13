using System;

// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public class MathNet2WithHistory : MathNet2
    {
        private readonly double[,] _history;

        public int HistorySize { get; set; }

        public MathNet2WithHistory(double maxX, double maxT, double historySize, double h, double d) : base(maxX, maxT, h, d)
        {
            HistorySize = (int) (historySize / d);
            _history = new double[Width + 1, HistorySize];
        }

        public override void Set(int i, int j, double value)
        {
            if (j >= 0)
            {
                base.Set(i, j, value);
                return;
            }
            if (-j > HistorySize || i < 0 || i > Width)
            {
                throw new ArgumentOutOfRangeException($"Элемент ({i},{j})  выходит за границу сетки {Width}x{Height}");
            }


            _history[i, -j - 1] = value;
        }

        public override double Get(int i, int j)
        {
            if (j >= 0)
            {
                return base.Get(i, j);
            }
            if (-j > HistorySize || i < 0 || i > Width)
            {
                throw new ArgumentOutOfRangeException($"Элемент ({i},{j})  выходит за границу сетки {Width}x{Height}");
            }


            return _history[i, -j - 1];
        }

        public override double[,] ToMatrix()
        {
            var result = new double[Width + 1, Height + HistorySize + 1];
            for (var j = 0; j < HistorySize; j++)
            for (var i = 0; i <= Width; i++)
            {
                result[i, HistorySize - j - 1] = _history[i, j];
            }
            for (var j = 0; j <= Height; j++)
            for (var i = 0; i <= Width; i++)
            {
                result[i, HistorySize + j] = _net[i, j];
            }
            return result;
        }
    }
}