using System;

namespace EE.NIESolver.MathNet.Services
{
    public class LinearInterpolationService : IInterpolationService
    {
        public double InterpolateX(I2Pointer p, double delta)
        {
            var coef = Math.Abs(delta % 1);
            if (coef.CompareTo(0) == 0) return p.GetValue((int)delta, 0);
            var min = (int)Math.Floor(delta);
            var max = min + 1;
            return delta >= 0
                ? p.GetValue(max, 0) * coef + p.GetValue(min, 0) * (1 - coef)
                : p.GetValue(min, 0) * coef + p.GetValue(max, 0) * (1 - coef);
        }

        public double InterpolateY(I2Pointer p, double delta)
        {
            var coef = Math.Abs(delta % 1);
            if (coef.CompareTo(0) == 0) return p.GetValue(0, (int)delta);
            var min = (int)Math.Floor(delta);
            var max = min + 1;
            return delta > 0
                ? p.GetValue(0, max) * coef + p.GetValue(0, min) * (1 - coef)
                : p.GetValue(0, min) * coef + p.GetValue(0, max) * (1 - coef);
        }
    }
}