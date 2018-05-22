using System;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Solver.Runners;
using EE.NIESolver.Web.Factories;
using EE.NIESolver.Web.Models.Experiments;
using Microsoft.AspNetCore.Mvc;

namespace EE.NIESolver.Web.Controllers
{
    public class ExperimentController : EntityController<ExperimentModel, DbExperiment>
    {
        private readonly NetFactory _netFactory;
        private readonly MethodFactory _methodFactory;

        public ExperimentController(IDataRepository repository, NetFactory netFactory, MethodFactory methodFactory) : base(repository)
        {
            _netFactory = netFactory;
            _methodFactory = methodFactory;
        }

        #region Реестр экспериментов

        public JsonResult GetAll()
        {
            var items = Repository.Select<DbExperiment>()
                .Select(x => new ExperimentRegistryModel
                {
                    Id = x.Id,
                    //Name = x.Name,
                    //Description = x.Description,
                    ResultCount = x.Results.Count
                });
            return JsonResult(true, items);
        }

        #endregion

        #region Создание эксперимента

        [HttpGet]
        public JsonResult Create()
        {
            var model = GetModel(null);
            model.InitializeEdit(Repository);
            return JsonResult(true, model);
        }

        [HttpPost]
        public JsonResult Create([FromBody]ExperimentModel model)
        {
            if (!ModelState.IsValid)
            {
                return JsonResult(false);
            }

            var entity = new DbExperiment();
            Repository.Add(entity);
            model.ApplyModel(entity);
            Repository.Apply();
            return JsonResult(true, model);
        }

        [HttpGet]
        public JsonResult GetParameters(int methodId)
        {
            var parameters = Repository.Select<DbMethodParameter>()
                .Where(x => x.MethodId == methodId)
                .Select(x => new ConstantItem
                {
                    Id = x.Id,
                    Value = x.Name,
                    Description = x.Description
                });

            return JsonResult(true, parameters);
        }

        #endregion

        #region Удаление эксперимента

        [HttpDelete]
        public JsonResult DeleteExperiment(int id)
        {
            var entity = Repository.Get<DbExperiment>(id);
            if (entity == null)
            {
                return JsonResult(false);
            }
            Repository.Delete(entity);
            Repository.Apply();
            return JsonResult(true);
        }

        #endregion

        #region Выполнение эксперимента

        [HttpGet]
        public JsonResult RunExperiment(int experimentId)
        {
            var entity = Repository.Get<DbExperiment>(experimentId);
            if (entity == null)
            {
                return JsonResult(false);
            }

            var values = Repository.Select<DbMethodParameterValue>(x => x.ExperimentId == experimentId);
            var net = _netFactory.Create(entity.Method.MethodTypeId.GetEnum<MethodTypes>(), values);
            var method = _methodFactory.CreateMethod2(entity.Method.MethodTypeId.GetEnum<MethodTypes>(), values);
            var runner = new ClassicRunner(method);
            runner.Run(net);

            double ExpectedFunc(double x, double t) => t * Math.Sin(Math.PI * x);

            var result = double.MinValue;
            for (var j = 0; j <= net.Height; j++)
            for (var i = 0; i <= net.Width; i++)
            {
                var expected = ExpectedFunc(i * net.H, j * net.D);
                var error = Math.Abs(expected - net.Get(i, j));
                if (error > result)
                {
                    result = error;
                }
            }
            
            entity.Results.Add(new DbExperimentResult
            {
                Date = DateTime.Now,
                Duration = 1,
                RunnerTypeId = 1,
                Result = result.ToString("f10")
            });
            Repository.Apply();

            return JsonResult(true);
        }

        #endregion


    }
}