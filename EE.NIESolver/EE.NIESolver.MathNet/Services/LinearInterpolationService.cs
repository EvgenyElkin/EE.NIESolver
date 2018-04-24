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

        double Integral(double x, double from, double to);
    }

    public class SymmetrizedDerivativesNetHistory : INetHistory
    {
        private readonly IInterpolationService _interpolationService;
        private readonly MathNet2 _net;
        private int _level;

        public SymmetrizedDerivativesNetHistory(MathNet2 net, IInterpolationService interpolation)
        {
            _interpolationService = interpolation;
            _net = net;
        }

        public double Get(double x, double t)
        {
            var n = (int) Math.Ceiling(x / _net.H);
            var m = (int) Math.Ceiling(t / _net.D);
            if (m > _level)
            {
                _level = m;
            }

            if (t < 0 || m < _level)
            {
                return (_interpolationService.InterpolateVertical(_net, n, t)
                        + _interpolationService.InterpolateVertical(_net, n - 1, t)) / 2;
            }

            var coef = ((m - 1) * _net.D - (t - 1));
            var left = _interpolationService.InterpolateVertical(_net, n - 1, t - _net.D);
            var right = _interpolationService.InterpolateVertical(_net, n, t - _net.D);
            var top = _interpolationService.InterpolateHorizontal(_net, x, m - 1);
            var down = (left + right) / 2;
            return down + (top - down)/ coef * _net.D;
        }

        public double Integral(double x, double from, double to)
        {
            var result = 0d;
            var n = (int)Math.Ceiling(x / _net.H);
            var m = (int)Math.Floor(from / _net.D);
            while (m * _net.D < to)
            {
                result += ((_net.Get(n, m) + _net.Get(n, m + 1)) / 2) * _net.D;
                m++;
            }

            return result;
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

        public double Integral(double x, double from, double to)
        {
            return _decoratee.Integral(x, from, to);
        }
    }

}