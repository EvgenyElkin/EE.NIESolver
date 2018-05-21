using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.MathNet.Interfaces;
using EE.NIESolver.Solver.Delegates;
using EE.NIESolver.Solver.Methods;

namespace EE.NIESolver.Web.Factories
{
    public class MethodFactory
    {
        private readonly IInterpolationService _interpolationService;
        private readonly IFunctionExtractor _extractor;

        public I2Method CreateMethod2(MethodTypes methodType, IEnumerable<DbMethodParameterValue> parameters)
        {
            
            switch (methodType)
            {
                case MethodTypes.R2SymmetrizedDerivativesMethod:
                    var stringValue = parameters.First(x => x.Parameter.Code == "RIGHT-FUNC").Value;
                    var func = _extractor.ExtractRightFunction<OneSpaceVariableRightFunction>(stringValue);
                    return new SymmetrizedDerivatedMethod(func, _interpolationService);
                default:
                    throw new NotSupportedException($"{methodType} not supported");
            }
        }
    }

    public interface IFunctionExtractor
    {
        TFunction ExtractRightFunction<TFunction>(string stringValue);
    }
}
