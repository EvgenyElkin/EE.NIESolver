using System;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Account;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.QueryHelpers;
using EE.NIESolver.DataLayer.Repositories;
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
            var items = Repository.Query<DbExperiment>()
                .Select(x => new ExperimentRegistryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
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
            var runParameterTypeId = MethodParameterTypes.RunVariable.GetId();
            var parameters = Repository.Query<DbMethodParameter>()
                .Where(x => x.MethodId == methodId)
                .Where(x => x.ParameterTypeId != runParameterTypeId)
                .Select(x => new ConstantItem
                {
                    Id = x.Id,
                    Value = x.Name,
                    Description = x.Description
                });

            return JsonResult(true, parameters);
        }

        #endregion

        #region Простмотр эксперимента

        [HttpGet]
        public JsonResult Display(int id)
        {
            var entity = Repository.Query<DbExperiment>()
                .WithAllProperties()
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new ArgumentException();
            }

            var model = new ExperimentModel();
            model.SetModel(entity);
            model.InitializeDisplay(Repository);

            return JsonResult(true, model);
        }

        #endregion

        #region Удаление эксперимента

        [HttpDelete]
        public JsonResult Delete(int id)
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

        [HttpPost]
        public JsonResult Run([FromBody]ExperimentRunsModel model)
        {
            var entity = Repository.Get<DbExperiment>(model.ExperimentId);
            if (entity == null)
            {
                return JsonResult(false);
            }

            foreach (var runs in model.Runs)
            foreach (var modelRunnerId in model.RunnerIds)
            {
                var result = new DbExperimentResult
                {
                    ExperimentId = model.ExperimentId,
                    Date = DateTime.Now,
                    RunnerTypeId = modelRunnerId,
                    StatusId = ExperimentStatusTypes.Wait.GetId(),
                    Parameters = runs.Parameters
                        .Select(x => new DbExperimentRunParameter
                        {
                            Code = x.Key,
                            Value = x.Value
                        }).ToList()
                };
                entity.Results.Add(result);
            }

            //var values = Repository.Query<DbMethodParameterValue>(x => x.ExperimentId == model.ExperimentId);
            //var net = _netFactory.Create(entity.Method.MethodTypeId.GetEnum<MethodTypes>(), values);
            //var method = _methodFactory.CreateMethod2(entity.Method.MethodTypeId.GetEnum<MethodTypes>(), values);
            //var runner = new ClassicRunner(method);
            //runner.Run(net);
            //
            //double ExpectedFunc(double x, double t) => t * Math.Sin(Math.PI * x);
            //
            //var result = double.MinValue;
            //for (var j = 0; j <= net.Height; j++)
            //for (var i = 0; i <= net.Width; i++)
            //{
            //    var expected = ExpectedFunc(i * net.H, j * net.D);
            //    var error = Math.Abs(expected - net.Get(i, j));
            //    if (error > result)
            //    {
            //        result = error;
            //    }
            //}
            
            Repository.Apply();

            return JsonResult(true);
        }

        #endregion
    }
}