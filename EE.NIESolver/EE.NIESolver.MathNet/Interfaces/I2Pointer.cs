// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public interface I2Pointer
    {
        MathNet2 Net { get; }
        double GetValue(int di, int dj);
        double GetLeft(uint value = 1);
        double GetRight(uint value = 1);
        double GetTop(uint value = 1);
        double GetDown(uint value = 1);
        double X { get; }
        double T { get; }
    }
}