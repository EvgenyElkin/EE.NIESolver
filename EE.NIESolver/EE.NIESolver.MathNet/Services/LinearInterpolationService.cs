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

    public interface INetHistory
    {
        double Get(double x, double t);
    }

    public class SymmetrizedDerivativesNetHistory : INetHistory
    {
        private readonly IInterpolationService _interpolationService;
        private readonly MathNet2 _net;

        public SymmetrizedDerivativesNetHistory(MathNet2 net, IInterpolationService interpolation)
        {
            _interpolationService = interpolation;
            _net = net;
        }

        public double Get(double x, double t)
        {
            var n = (int) Math.Ceiling(x / _net.H);
            var m = (int) Math.Ceiling(t / _net.D);
            if (t < 0)
            {
                return (_interpolationService.InterpolateVertical(_net, n, t)
                        + _interpolationService.InterpolateVertical(_net, n - 1, t)) / 2;
            }
            var left = _interpolationService.InterpolateVertical(_net, n, t - _net.D);
            var right = _interpolationService.InterpolateVertical(_net, n + 1, t - _net.D);
            var down = (left + right) / 2;
            var top = _interpolationService.InterpolateHorizontal(_net, x, m);
            return 2 * down - top;
        }
    }

    public class CachedNetHistoryDecorator : INetHistory
    {
        private readonly INetHistory _decoratee;
        private readonly Dictionary<Tuple<double, double>, double> _cache;

        public CachedNetHistoryDecorator(MathNet2 net, INetHistory decoratee)
        {
            _decoratee = decoratee;
            _cache = new Dictionary<Tuple<double, double>, double>();
            foreach (var point in net.GetPoints())
            {
                _cache[Tuple.Create(point.X, point.T)] = net.Get(point.I, point.J);
            }
        }

        public double Get(double x, double t)
        {
            var key = Tuple.Create(x, t);
            if (!_cache.ContainsKey(key))
            {
                var result = _decoratee.Get(x, t);
                _cache[key] = result;
            }

            return _cache[key];
        }
    }

}