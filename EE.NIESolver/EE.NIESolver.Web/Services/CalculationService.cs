using System;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Account;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Controllers;
using EE.NIESolver.Web.Factories;
using EE.NIESolver.Web.Models.Experiments;
using EE.NIESolver.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace EE.NIESolver.Web.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IDataRepository _repository;
        private readonly NetFactory _netFactory;
        private readonly MethodFactory _methodFactory;
        private readonly RunnerFactory _runnerFactory;

        public CalculationService(IDataRepository repository, NetFactory netFactory, MethodFactory methodFactory, RunnerFactory runnerFactory)
        {
            _repository = repository;
            _netFactory = netFactory;
            _methodFactory = methodFactory;
            _runnerFactory = runnerFactory;
        }

        public void Calculate(int resultId)
        {
            var result = _repository.Get<DbExperimentResult>(resultId);
            var expreriment = _repository.Get<DbExperiment>(result.ExperimentId);
            var experimentParameters = _repository.Query<DbMethodParameterValue>(x => x.ExperimentId == result.ExperimentId);

            try
            {
                var net = _netFactory.Create(expreriment.Method.MethodTypeId.GetEnum<MethodTypes>(), experimentParameters, result.Parameters);
                var method = _methodFactory.CreateMethod2(expreriment);
                var runner = _runnerFactory.Create2Runner(result.RunnerTypeId, method);

                runner.Run(net);

                //TODO сделать полиморфное решение в зависимости от типа метода
                var resultModel = new Experiment2ResultModel
                {
                    X = net.MaxX,
                    T = net.MaxT,
                    SizeX = net.Width,
                    SizeY = net.Height,
                    Values = net.GetResults()
                };

                result.Result = JsonConvert.SerializeObject(resultModel);
                result.StatusId = ExperimentStatusTypes.Complete.GetId();
            }
            catch (Exception e)
            {
                result.StatusId = ExperimentStatusTypes.Error.GetId();
            }
            
        }
    }
}