using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace EE.NIESolver.MathNet
{
    public interface IMathNet2Builder
    { }

    public interface IMathNet2BuidlderEmpty : IMathNet2Builder
    {
        IMathNet2BuilderWithArea SetArea(double maxX, double maxY);
    }

    public interface IMathNet2BuilderWithArea
    {
        IMathNet2BuilderWithInitialConditions SetInitialConditions(Func<double, double> initialConditions);
    }

    public interface IMathNet2BuilderWithInitialConditions
    {
        IMathNet2BuilderWithLeftBorder SetLeftBorder(Func<double, double> leftBorder);
        IMathNet2BuilderWithRightBorder SetRightBorder(Func<double, double> rightBorder);
    }

    public interface IMathNet2BuilderWithLeftBorder : IMathNet2BuilderWithBorders
    {
        IMathNet2BuilderWithBorders SetRightBorder(Func<double, double> leftBorder);
    }

    public interface IMathNet2BuilderWithRightBorder : IMathNet2BuilderWithBorders
    {
        IMathNet2BuilderWithBorders SetLeftBorder(Func<double, double> rightBorder);
    }

    public interface IMathNet2BuilderWithBorders
    {
        MathNet2 Build(double h, double d);
        IMathNet2BuilderComplete WithParams(double h, double d);
    }

    public interface IMathNet2BuilderComplete
    {
        IMathNet2BuilderComplete WithParams(double h, double d);
        IEnumerable<MathNet2> Build();
    }
}