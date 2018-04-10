namespace EE.NIESolver.MathNet.Services
{
    public interface IInterpolationService
    {
        double InterpolateX(I2Pointer p, double dx);
        double InterpolateY(I2Pointer p, double dy);
    }
}
