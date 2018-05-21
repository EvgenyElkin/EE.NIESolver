using System;

namespace EE.NIESolver.Solver.Delegates
{
    public delegate double OneSpaceVariableRightFunction(double x, double t, Func<double, double, double> u);
}