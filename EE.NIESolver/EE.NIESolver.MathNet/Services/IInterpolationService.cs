namespace EE.NIESolver.MathNet.Services
{
    public interface IInterpolationService
    {
        double InterpolateHorizontal(MathNet2 net, double x, int j);
        double InterpolateVertical(MathNet2 net, int i, double y);
    }
}
