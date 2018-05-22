using System;
using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.MathNet;
using EE.NIESolver.Web.Extractions;

namespace EE.NIESolver.Web.Factories
{
    public class NetFactory
    {
        private readonly IMathNetFactory _factory;
        private readonly IFunctionExtractor _extractor;

        public NetFactory(IMathNetFactory factory, IFunctionExtractor extractor)
        {
            _factory = factory;
            _extractor = extractor;
        }

        public MathNet2 Create(MethodTypes methodType, IEnumerable<DbMethodParameterValue> parameters)
        {
            var cache = parameters.ToDictionary(x => x.Parameter.Code);
            switch (methodType)
            {
                case MethodTypes.R2SymmetrizedDerivativesMethod:
                    return _factory
                        .CreateMathNet2()
                        .SetArea(double.Parse(cache["X"].Value), int.Parse(cache["T"].Value))
                        .SetInitialConditions(_extractor.ExtractR1Function(cache["INITIAL"].Value))
                        .SetLeftBorder(_extractor.ExtractR1Function(cache["LEFT"].Value))
                        .SetHistory(double.Parse(cache["TAY"].Value), _extractor.ExtractR2Function(cache["HISTORY"].Value))
                        .Build(double.Parse(cache["h"].Value), double.Parse(cache["d"].Value));
                default:
                    throw new NotSupportedException($"{methodType} not supported");
            }
        }
    }
}