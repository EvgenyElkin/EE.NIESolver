using System;

namespace EE.NIESolver.Solver.Delegates
{
    public delegate double R2DelayedFunction(double x, double t, Func<double, double, double> u);
}