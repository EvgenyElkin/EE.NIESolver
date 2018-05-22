using System;
using org.mariuszgromada.math.mxparser;

namespace EE.NIESolver.Web.Extractions.FunctionExtensions
{
    public class R2Function : FunctionExtension
    {
        private readonly Func<double, double, double> _func;
        private readonly double[] _args;
        private readonly string[] _argNames = {"x", "t"};

        public R2Function(Func<double, double, double> func)
        {
            _func = func;
            _args = new double[2];
        }

        public int getParametersNumber()
        {
            return 2;
        }

        public void setParameterValue(int parameterIndex, double parameterValue)
        {
            _args[parameterIndex] = parameterValue;
        }

        public string getParameterName(int parameterIndex)
        {
            return _argNames[parameterIndex];
        }

        public double calculate()
        {
            return _func(_args[0], _args[1]);
        }

        public FunctionExtension clone()
        {
            var clone = new R2Function(_func);
            clone.setParameterValue(0, _args[0]);
            clone.setParameterValue(1, _args[1]);
            return clone;
        }
    }
}