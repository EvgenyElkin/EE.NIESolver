using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    internal class MathNet2Builder : IMathNet2BuidlderEmpty,
        IMathNet2BuilderWithArea,
        IMathNet2BuilderWithInitialConditions,
        IMathNet2BuilderWithLeftBorder,
        IMathNet2BuilderWithRightBorder,
        IMathNet2BuilderComplete,
        IMathNet2BuilderWithHistory,
        IMathNet2BuilderWithHistoryComplete
    {
        private double _maxX;
        private double _maxT;
        private double _historySize;

        private Func<double, double> _initialConditions;
        private Func<double, double> _leftBorder;
        private Func<double, double> _rightBorder;
        private Func<double, double, double> _history;

        private readonly List<Tuple<double, double>> _params;

        public MathNet2Builder()
        {
            _params = new List<Tuple<double, double>>();
        }

        public IMathNet2BuilderWithArea SetArea(double maxX, double maxT)
        {
            _maxX = maxX;
            _maxT = maxT;
            return this;
        }

        public IMathNet2BuilderWithInitialConditions SetInitialConditions(Func<double, double> initialConditions)
        {
            _initialConditions = initialConditions;
            return this;
        }

        public IMathNet2BuilderWithLeftBorder SetLeftBorder(Func<double, double> leftBorder)
        {
            _leftBorder = leftBorder;
            return this;
        }

        public IMathNet2BuilderWithRightBorder SetRightBorder(Func<double, double> rightBorder)
        {
            _rightBorder = rightBorder;
            return this;
        }

        IMathNet2BuilderWithBorders IMathNet2BuilderWithRightBorder.SetLeftBorder(Func<double, double> rightBorder) =>
            SetLeftBorder(rightBorder);

        IMathNet2BuilderWithBorders IMathNet2BuilderWithLeftBorder.SetRightBorder(Func<double, double> leftBorder) =>
            SetRightBorder(leftBorder);

        public IMathNet2BuilderComplete WithParams(double h, double d)
        {
            _params.Add(Tuple.Create(h, d));
            return this;
        }
        
        public IEnumerable<MathNet2> Build()
        {
            foreach (var parameters in _params)
            {
                var net = new MathNet2(_maxX, _maxT, parameters.Item1, parameters.Item2);
                InitMathNet(net);
                yield return net;
            }
        }
        
        public MathNet2 Build(double h, double d)
        {
            return WithParams(h, d)
                .Build()
                .First();
        }
        
        #region Сетка с историей

        IMathNet2BuilderWithHistory IMathNet2BuilderWithBorders.SetHistory(double historySize, Func<double, double, double> history)
        {
            _historySize = historySize;
            _history = history;
            return this;
        }

        IMathNet2BuilderWithHistoryComplete IMathNet2BuilderWithHistory.WithParams(double h, double d)
        {
            WithParams(h, d);
            return this;
        }

        IMathNet2BuilderWithHistoryComplete IMathNet2BuilderWithHistoryComplete.WithParams(double h, double d)
        {
            WithParams(h, d);
            return this;
        }

        IEnumerable<MathNet2WithHistory> IMathNet2BuilderWithHistoryComplete.Build()
        {
            foreach (var parameters in _params)
            {
                var net = new MathNet2WithHistory(_maxX, _maxT, _historySize, parameters.Item1, parameters.Item2);
                InitMathNet(net);
                for (var j = -1; j >= -net.HistorySize ; j--)
                for (var i = 0; i <= net.Width; i++)
                {
                    net.Set(i, j, _history(i * net.H, j * net.D));
                }
                yield return net;
            }
        }

        MathNet2WithHistory IMathNet2BuilderWithHistory.Build(double h, double d)
        {
            return ((IMathNet2BuilderWithHistoryComplete) this)
                .WithParams(h,d)
                .Build()
                .First();
        }

        #endregion
        
        private void InitMathNet(MathNet2 net)
        {
            for (var i = 0; i <= net.Width; i++)
            {
                var initialValue = _initialConditions(i * net.H);
                net.Set(i, 0, initialValue);
            }

            for (var j = 0; j <= net.Height; j++)
            {
                if (_leftBorder != null)
                {
                    var leftBorderValue = _leftBorder(j * net.D);
                    net.Set(0, j, leftBorderValue);
                }
                if (_rightBorder != null)
                {
                    var rightBorderValue = _rightBorder(j * net.D);
                    net.Set(net.Width, j, rightBorderValue);
                }
            }
        }
    }
}