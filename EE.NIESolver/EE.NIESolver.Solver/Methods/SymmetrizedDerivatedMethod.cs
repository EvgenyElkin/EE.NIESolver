﻿using System;
using EE.NIESolver.MathNet;
using EE.NIESolver.MathNet.Interfaces;
using EE.NIESolver.Solver.Delegates;

namespace EE.NIESolver.Solver.Methods
{
    public class SymmetrizedDerivatedMethod : I2Method
    {
        private readonly OneSpaceVariableRightFunction _func;
        private readonly IInterpolationService _interpolationService;
        private int _level;

        public SymmetrizedDerivatedMethod(OneSpaceVariableRightFunction func, IInterpolationService interpolationService)
        {
            _func = func;
            _interpolationService = interpolationService;
        }

        public double Calculate(I2Pointer p)
        {
            var net = p.Net;
            return (_func(p.X - net.H / 2, p.T - net.D / 2, Interpolate(p)) * 2 * net.H * net.D
                    + (net.H - net.D) * (p.GetDown() - p.GetLeft()))
                   / (net.H + net.D) + p.GetValue(-1, -1);
        }

        public Func<double, double, double> Interpolate(I2Pointer p)
        {
            var net = p.Net;
            return (x, t) =>
            {
                var n = (int) Math.Ceiling(x / net.H);
                var m = (int) Math.Ceiling(t / net.D);
                if (m > _level)
                {
                    _level = m;
                }

                if (t < 0 || m < _level)
                {
                    return (_interpolationService.InterpolateVertical(net, n, t)
                            + _interpolationService.InterpolateVertical(net, n - 1, t)) / 2;
                }

                var coef = ((m - 1) * net.D - (t - 1));
                var left = _interpolationService.InterpolateVertical(net, n - 1, t - net.D);
                var right = _interpolationService.InterpolateVertical(net, n, t - net.D);
                var top = _interpolationService.InterpolateHorizontal(net, x, m - 1);
                var down = (left + right) / 2;
                return down + (top - down) / coef * net.D;
            };
        }
    }
}