using System;
using EE.NIESolver.Solver.Delegates;
using EE.NIESolver.Web.Extractions.FunctionExtensions;
using EE.NIESolver.Web.Factories;
using org.mariuszgromada.math.mxparser;

namespace EE.NIESolver.Web.Extractions
{
    public class FunctionExtractor : IFunctionExtractor
    {
        public Func<double, double> ExtractR1Function(string stringValue)
        {
            var function = new Function(stringValue);
            if (!function.checkSyntax() || function.getArgumentsNumber() != 1)
            {
                //TODO #18 Добавить исключения
                throw new NotSupportedException();
            }
            return x =>
            {
                function.setArgumentValue(0, x);
                return function.calculate();
            };
        }

        public Func<double, double, double> ExtractR2Function(string stringValue)
        {
            var function = new Function(stringValue);
            if (!function.checkSyntax() || function.getArgumentsNumber() != 2)
            {
                //TODO #18 Добавить исключения
                throw new NotSupportedException();
            }
            return (x,t) =>
            {
                function.setArgumentValue(0, x);
                function.setArgumentValue(1, t);
                return function.calculate();
            };
        }

        public R2DelayedFunction ExtractDelayedR2Function(string stringValue)
        {
            var function = new Function(stringValue);
            if (!function.checkSyntax() || function.getArgumentsNumber() != 2)
            {
                //TODO #18 Добавить исключения
                throw new NotSupportedException();
            }
            return (x, t, u) =>
            {
                var uFunction = new Function("u", new R2Function(u));
                function.setArgumentValue(0, x);
                function.setArgumentValue(1, t);
                if (function.getFunction("u") == null)
                {
                    function.addDefinitions(uFunction);
                }
                return function.calculate();
            };
        }
    }
}