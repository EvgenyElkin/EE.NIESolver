// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public class MathNet2Pointer : IMathNet2Pointer
    {
        private readonly MathNet2 _net;
        private int _i;
        private int _j;

        public MathNet2Pointer(MathNet2 net)
        {
            _net = net;
        }

        public void Set(int i, int j)
        {
            _i = i;
            _j = j;
        }

        public void SetValue(double value)
        {
            _net.Set(_i, _j, value);
        }

        public double GetValue(int di, int dj)
        {
            return _net.Get(_i + di, _j + dj);
        }

        public double GetLeft(int value = 1) => GetValue(-value, 0);
        public double GetRight(int value = 1) => GetValue(value, 0);
        public double GetTop(int value = 1) => GetValue(0, value);
        public double GetDown(int value = 1) => GetValue(0, -value);

        public MathNet2Pointer To(int dx, int dy)
        {
            Set(_i+dx, _j + dy);
            return this;
        }

        public MathNet2Pointer ToLeft(int value = 1) => To(-value, 0);
        public MathNet2Pointer ToRight(int value = 1) => To(value, 0);
        public MathNet2Pointer ToTop(int value = 1) => To(0, value);
        public MathNet2Pointer ToDown(int value = 1) => To(0, -value);
    }
}