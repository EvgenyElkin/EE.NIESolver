using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models.Methods
{
    public class MethodParameterModel : IModel<DbMethodParameter>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }

        public void SetModel(DbMethodParameter entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Code = entity.Code;
            TypeId = entity.ParameterTypeId;
        }

        public void ApplyModel(DbMethodParameter entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.Code = Code;
            entity.ParameterTypeId = TypeId;
        }
    }
}