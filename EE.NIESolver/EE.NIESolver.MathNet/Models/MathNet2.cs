// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public class MathNet2
    {
        private readonly double[,] _net;

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
            Width = (int)(maxX / h);
            Height = (int)(maxT / d);
            _net = new double[Width + 1, Height + 1];
        }

        /// <summary>
        /// Установить значение в ячейку сетки
        /// </summary>
        /// <param name="i">Номер ячейки по пространству</param>
        /// <param name="j">Номер ячейки по времени</param>
        /// <param name="value">Значение</param>
        public void Set(int i, int j, double value)
        {
            _net[i, j] = value;
        }

        /// <summary>
        /// Получить значение ячейки из сетки
        /// </summary>
        /// <param name="i">Номер ячейки по пространству</param>
        /// <param name="j">Номер ячейки по времени</param>
        /// <returns>Значение сетки в i,j</returns>
        public double Get(int i, int j)
        {
            return _net[i, j];
        }
    }
}