using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Methods;
using Microsoft.AspNetCore.Mvc;

namespace EE.NIESolver.Web.Controllers
{
    public class MethodController : EntityController<MethodModel, DbMethod>
    {
        public MethodController(IDataRepository repository) : base(repository)
        {
        }

        public JsonResult GetAll()
        {
            var items = Repository.Select<DbMethod>()
                .Select(x => new MethodRegistryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MethodType = x.MethodTypeId.GetDescription<MethodTypes>()
                });
            return JsonResult(true, items);
        }

        [HttpGet]
        public JsonResult Display(int methodId)
        {
            var model = GetModel(methodId);
            return JsonResult(true, model);
        }

        [HttpGet]
        public JsonResult Edit(int? methodId)
        {
            var model = GetModel(methodId);
            model.InitializeEdit();
            return JsonResult(true, model);
        }

        [HttpPost]
        public JsonResult Edit([FromBody]MethodModel model)
        {
            if (!ModelState.IsValid)
            {
                return JsonResult(false, new { });
            }
            DbMethod entity;
            if (model.Id.HasValue)
            {
                entity = Repository.Get<DbMethod>(model.Id.Value);
            }
            else
            {
                entity = new DbMethod();
                Repository.Add(entity);
            }
            model.ApplyModel(entity);
            Repository.Apply();
            return JsonResult(true, model);
        }
    }
}
