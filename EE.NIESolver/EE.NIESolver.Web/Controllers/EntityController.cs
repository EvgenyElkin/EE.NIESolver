using System;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Controllers
{
    public abstract class EntityController<TModel, TDb> : ControllerBase
        where TModel : class, IModel<TDb>, new()
        where TDb : class, IDomainEntity
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
                var entity = Repository.Get<TDb>(id.Value);
                if (entity == null)
                {
                    throw new ArgumentException();
                }
                model.SetModel(entity);
            }
            return model;
        }

        
    }
}