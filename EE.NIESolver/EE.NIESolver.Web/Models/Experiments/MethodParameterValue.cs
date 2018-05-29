using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models.Experiments
{
    public class MethodParameterValue : IModel<DbMethodParameterValue>
    {
        public int? Id { get; set; }
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public string Value { get; set; }

        public void SetModel(DbMethodParameterValue entity)
        {
            Id = entity.Id;
            ParameterId = entity.ParameterId;
            ParameterName = entity.Parameter.Name;
            ParameterDescription = entity.Parameter.Description;
            Value = entity.Value;
        }

        public void ApplyModel(DbMethodParameterValue entity)
        {
            entity.ParameterId = ParameterId;
            entity.Value = Value;
        }
    }
}