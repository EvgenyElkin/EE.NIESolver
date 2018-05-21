using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Experiments;
using Microsoft.AspNetCore.Mvc;

namespace EE.NIESolver.Web.Controllers
{
    public class ExperimentController : EntityController<ExperimentModel, DbExperiment>
    {
        public ExperimentController(IDataRepository repository) : base(repository)
        {
        }

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
                return JsonResult(false, null);
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
    }
}