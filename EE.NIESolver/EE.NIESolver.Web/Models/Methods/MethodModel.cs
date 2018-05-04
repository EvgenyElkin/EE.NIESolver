using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models.Methods
{
    public class MethodModel : IModel<MethodEntity>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MethodParameterModel[] Parameteres { get; set; }
        public IEnumerable<ConstantItem> MethodParameterTypes { get; set; }

        public MethodModel()
        {
            Parameteres = new MethodParameterModel[0];
        }

        public void SetModel(MethodEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Parameteres = entity.Parameteres.Select(e =>
            {
                var m = new MethodParameterModel();
                m.SetModel(e);
                return m;
            }).ToArray();
        }

        public void InitializeEdit()
        {
            MethodParameterTypes = ConstantConext.Current.GetValues<MethodParameterTypes>();
        }

        public void ApplyModel(MethodEntity entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            if (entity.Parameteres == null)
            {
                entity.Parameteres = new List<MethodParameterEntity>();
            }
            entity.Parameteres.ApplyCollection(Parameteres.ToArray());
        }
    }

    public static class CollectionApplyManager
    {
        public static void ApplyCollection<TEntity>(this ICollection<TEntity> entityCollection, IModel<TEntity>[] modelCollection)
            where TEntity : class, IDomainEntity, new()
        {
            //Удаление старых
            var actualEntities = modelCollection.Where(x => x.Id.HasValue).Select(x => x.Id.Value).ToHashSet();
            var deletedEntities = entityCollection.Where(x => !actualEntities.Contains(x.Id)).ToArray();
            foreach (var deletedEntity in deletedEntities)
            {
                entityCollection.Remove(deletedEntity);
            }
            
            //Изменение существующих и добавление новых
            foreach (var model in modelCollection)
            {
                var entity = entityCollection.FirstOrDefault(x => x.Id == model.Id);
                if (entity == null)
                {
                    entity = new TEntity();
                    entityCollection.Add(entity);
                }
                model.ApplyModel(entity);
            }
        }
    }

    public class MethodParameterModel : IModel<MethodParameterEntity>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }

        public void SetModel(MethodParameterEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Code = entity.Code;
            TypeId = entity.ParameterTypeId;
        }

        public void ApplyModel(MethodParameterEntity entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.Code = Code;
            entity.ParameterTypeId = TypeId;
        }
    }
}
