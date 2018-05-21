using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models
{
    public static class CollectionApplyManager
    {
        public static void ApplyCollection<TDb>(this ICollection<TDb> entityCollection, IModel<TDb>[] modelCollection)
            where TDb : class, IDomainEntity, new()
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
                    entity = new TDb();
                    entityCollection.Add(entity);
                }
                model.ApplyModel(entity);
            }
        }
    }
}