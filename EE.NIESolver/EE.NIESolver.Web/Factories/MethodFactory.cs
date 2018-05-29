using System;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.MathNet.Interfaces;
using EE.NIESolver.Solver.Methods;
using EE.NIESolver.Web.Extractions;
using EE.NIESolver.Web.Methods;

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

        public I2Method CreateMethod2(DbExperiment experiment)
        {
            var methodType = experiment.Method.MethodTypeId.GetEnum<MethodTypes>();
            var experimentParameters = experiment.Values.ToDictionary(x => x.Parameter.Code, x => x.Value);
            switch (methodType)
            {
                case MethodTypes.R1MethodWithHistory:
                    return new Universal2Method(experiment.Method.MethodExpression, experimentParameters["f"], _interpolationService);
                default:
                    throw new NotSupportedException($"{methodType} not supported");
            }
        }
    }
}
