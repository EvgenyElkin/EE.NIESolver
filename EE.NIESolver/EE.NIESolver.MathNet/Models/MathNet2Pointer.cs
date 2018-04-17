// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public class MathNet2Pointer : I2Pointer
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

        public double GetLeft(uint value = 1) => GetValue(-(int) value, 0);
        public double GetRight(uint value = 1) => GetValue((int) value, 0);
        public double GetTop(uint value = 1) => GetValue(0, (int) value);
        public double GetDown(uint value = 1) => GetValue(0, -(int) value);
        public double X => _net.H * _i;
        public double T => _net.D * _j;

        public MathNet2Pointer To(int dx, int dy)
        {
            Set(_i + dx, _j + dy);
            return this;
        }

        public MathNet2Pointer ToLeft(uint value = 1) => To(-(int) value, 0);
        public MathNet2Pointer ToRight(uint value = 1) => To((int) value, 0);
        public MathNet2Pointer ToTop(uint value = 1) => To(0, (int) value);
        public MathNet2Pointer ToDown(uint value = 1) => To(0, -(int) value);
    }
}