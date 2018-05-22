using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models.Experiments
{
    public class ExperimentModel : IModel<DbExperiment>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MethodId { get; set; }
        public string MethodName { get; set; }
        public MethodParameterValue[] Values { get; set; }
        public IEnumerable<ConstantItem> Methods;

        public void InitializeEdit(IDataRepository repository)
        {
            Methods = repository.Select<DbMethod>()
                .Select(x => new ConstantItem
                {
                    Id = x.Id,
                    Value = x.Name,
                    Description = x.Description
                });
        }

        public void SetModel(DbExperiment entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            MethodId = entity.MethodId.Value;
            MethodName = entity.Method.Name;
            Values = entity.Values.Select(e =>
            {
                var m = new MethodParameterValue();
                m.SetModel(e);
                return m;
            }).ToArray();

        }

        public void ApplyModel(DbExperiment entity)
        {
            entity.MethodId = MethodId;
            entity.Name = Name;
            entity.Description = Description;
            entity.Values = new List<DbMethodParameterValue>();
            entity.Values.ApplyCollection(Values);
        }
    }

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
