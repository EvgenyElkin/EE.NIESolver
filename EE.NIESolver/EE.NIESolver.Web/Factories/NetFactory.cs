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

        public MathNet2 Create(MethodTypes methodType, IEnumerable<DbMethodParameterValue> parameters, IEnumerable<DbExperimentRunParameter> runParameters)
        {
            var cache = parameters.ToDictionary(x => x.Parameter.Code);
            var runCache = runParameters.ToDictionary(x => x.Code, x => int.Parse(x.Value));
            switch (methodType)
            {
                case MethodTypes.R1MethodWithHistory:
                    var x = double.Parse(cache["x"].Value);
                    var t = double.Parse(cache["t"].Value);
                    return _factory
                        .CreateMathNet2()
                        .SetArea(x, t)
                        .SetInitialConditions(_extractor.ExtractR1Function(cache["initial"].Value))
                        .SetLeftBorder(_extractor.ExtractR1Function(cache["border"].Value))
                        .SetHistory(double.Parse(cache["tay"].Value), _extractor.ExtractR2Function(cache["u"].Value))
                        .Build(x / runCache["n"], t / runCache["m"]);
                default:
                    throw new NotSupportedException($"{methodType} not supported");
            }
        }
    }
}