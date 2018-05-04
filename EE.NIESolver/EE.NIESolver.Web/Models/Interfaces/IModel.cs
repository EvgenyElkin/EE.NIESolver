using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.Web.Models.Interfaces
{
    public interface IModel<in TEntity> where TEntity : IDomainEntity
    {
        int? Id { get; set; }
        void SetModel(TEntity entity);
        void ApplyModel(TEntity entity);
    }
}
