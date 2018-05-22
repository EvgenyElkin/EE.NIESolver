using System;
using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.MathNet.Interfaces;
using EE.NIESolver.Solver.Methods;
using EE.NIESolver.Web.Extractions;

namespace EE.NIESolver.Web.Factories
{
    public class MethodFactory
    {
        private readonly IInterpolationService _interpolationService;
        private readonly IFunctionExtractor _extractor;

        public MethodFactory(IInterpolationService interpolationService, IFunctionExtractor extractor)
        {
            _interpolationService = interpolationService;
            _extractor = extractor;
        }

        public I2Method CreateMethod2(MethodTypes methodType, IEnumerable<DbMethodParameterValue> parameters)
        {
            
            switch (methodType)
            {
                case MethodTypes.R2SymmetrizedDerivativesMethod:
                    var stringValue = parameters.First(x => x.Parameter.Code == "F").Value;
                    var func = _extractor.ExtractDelayedR2Function(stringValue);
                    return new SymmetrizedDerivatedMethod(func, _interpolationService);
                default:
                    throw new NotSupportedException($"{methodType} not supported");
            }
        }
    }
}
