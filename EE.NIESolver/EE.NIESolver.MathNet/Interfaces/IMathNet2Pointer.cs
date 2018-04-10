// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public interface IMathNet2Pointer
    {
        double GetValue(int di, int dj);
        double GetLeft(int value = 1);
        double GetRight(int value = 1);
        double GetTop(int value = 1);
        double GetDown(int value = 1);
    }
}