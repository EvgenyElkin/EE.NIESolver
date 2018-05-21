using System;
using System.Collections.Generic;
using EE.NIESolver.MathNet.Interfaces;

namespace EE.NIESolver.MathNet.Services
{
    public class LinearInterpolationService : IInterpolationService
    {
        public double InterpolateHorizontal(MathNet2 net, double x, int j)
        {
            var cellRaw = x / net.H;
            var coef = Math.Abs(cellRaw % 1);
            var min = (int)Math.Floor(cellRaw);
            var max = min + 1;
            if (coef.CompareTo(0) == 0) return net.Get(min, j);
            return net.Get(min, j) * (1 - coef) + net.Get(max, j) * coef;
        }

        public double InterpolateVertical(MathNet2 net, int i, double y)
        {
            var cellRaw = y / net.D;
            var coef = Math.Abs(cellRaw % 1);
            var min = (int)Math.Floor(cellRaw);
            var max = min + 1;
            if (coef.CompareTo(0) == 0) return net.Get(i, min);
            return net.Get(i, min) * (1 - coef) + net.Get(i, max) * coef;
        }
    }
}