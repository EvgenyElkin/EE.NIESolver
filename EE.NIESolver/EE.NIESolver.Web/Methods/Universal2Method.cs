using System;
using EE.NIESolver.MathNet;
using EE.NIESolver.MathNet.Interfaces;
using EE.NIESolver.Solver.Methods;
using EE.NIESolver.Web.Extractions.FunctionExtensions;
using org.mariuszgromada.math.mxparser;

namespace EE.NIESolver.Web.Methods
{
    public class Universal2Method : I2Method
    {
        private readonly Expression _method;
        private readonly Argument _leftFunc;
        private readonly Argument _rightFunc;
        private readonly Argument _topFunc;
        private readonly Argument _downFunc;
        private readonly Argument _x;
        private readonly Argument _t;
        private readonly Argument _d;
        private readonly Argument _h;
        private readonly R2Function _u;
        private readonly R2Function _v;
        private bool inited = false;
        private readonly Interpolator _interpolator;

        public Universal2Method(string methodExpression, string fExpression, IInterpolationService interpolationService)
        {
            _leftFunc = new Argument("left",0);
            _rightFunc = new Argument("right",0);
            _topFunc = new Argument("top",0);
            _downFunc = new Argument("down",0);
            _x = new Argument("x",0);
            _t = new Argument("t",0);
            _d = new Argument("d",0);
            _h = new Argument("h",0);
            _u = new R2Function();
            var u = new Function("u", _u);
            _v = new R2Function();
            var v = new Function("v", _v);
            var f = new Function(fExpression);
            f.addDefinitions(u);
            _interpolator = new Interpolator(interpolationService);
            _method = new Expression(methodExpression, _leftFunc, _rightFunc, _topFunc, _downFunc, _x, _t, _d, _h, f, u, v);
        }

        public double Calculate(I2Pointer p)
        {
            _leftFunc.setArgumentValue(p.GetLeft());
            _rightFunc.setArgumentValue(p.GetRight());
            _topFunc.setArgumentValue(p.GetTop());
            _downFunc.setArgumentValue(p.GetDown());
            _x.setArgumentValue(p.X);
            _t.setArgumentValue(p.T);
            _h.setArgumentValue(p.Net.H);
            _d.setArgumentValue(p.Net.D);
            if (!inited)
            {
                _interpolator.SetPointer(p);
                _u.SetFunction(_interpolator.Interpolate);
                _v.SetFunction((di, dj) => p.GetValue((int)di, (int)dj));
                inited = true;
            }
            return _method.calculate();
        }

        public class Interpolator
        {
            private readonly IInterpolationService _interpolationService;
            private int _level;
            private I2Pointer _p;

            public Interpolator(IInterpolationService interpolationService)
            {
                _interpolationService = interpolationService;
            }

            public void SetPointer(I2Pointer p)
            {
                _p = p;
            }

            public double Interpolate(double x, double t)
            {
                var net = _p.Net;
                var n = (int)Math.Ceiling(x / net.H);
                var m = (int)Math.Ceiling(t / net.D);
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
            }
        }
    }
}
