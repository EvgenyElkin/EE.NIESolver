using System;
using EE.NIESolver.Solver.Delegates;

namespace EE.NIESolver.Web.Extractions
{
    public interface IFunctionExtractor
    {
        Func<double, double> ExtractR1Function(string stringValue);
        Func<double, double, double> ExtractR2Function(string stringValue);
        R2DelayedFunction ExtractDelayedR2Function(string stringValue);
    }
}