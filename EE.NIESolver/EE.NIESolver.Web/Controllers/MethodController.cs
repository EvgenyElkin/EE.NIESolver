using System;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Interfaces;
using EE.NIESolver.Web.Models.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EE.NIESolver.Web.Controllers
{
    public abstract class EntityController<TModel,TEntity> : Controller
        where TModel : class, IModel<TEntity>, new()
        where TEntity : class, IDomainEntity
    {
        protected readonly IDataRepository Repository;

        protected EntityController(IDataRepository repository)
        {
            Repository = repository;
        }

        protected TModel GetModel(int? id)
        {
            var model = new TModel();
            if (id.HasValue)
            {
                var entity = Repository.Get<TEntity>(id.Value);
                if (entity == null)
                {
                    throw new ArgumentException();
                }
                model.SetModel(entity);
            }
            return model;
        }

        protected JsonResult JsonResult(bool result, object item)
        {
            return Json(new
            {
                IsSuccess = result,
                Item = item
            });
        }
    }

    public class MethodController : EntityController<MethodModel, MethodEntity>
    {
        public MethodController(IDataRepository repository) : base(repository)
        {
        }

        public JsonResult GetAll()
        {
            var spaceVariableTypeId = MethodParameterTypes.SpaceVariable.GetId();
            var items = Repository.Select<MethodEntity>()
                .Select(x => new MethodRegistryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    SpaceSize = x.Parameteres.Count(p => p.ParameterTypeId == spaceVariableTypeId)
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
            MethodEntity entity;
            if (model.Id.HasValue)
            {
                entity = Repository.Get<MethodEntity>(model.Id.Value);
            }
            else
            {
                entity = new MethodEntity();
                Repository.Add(entity);
            }
            model.ApplyModel(entity);
            Repository.Apply();
            return JsonResult(true, model);
        }
    }
}
