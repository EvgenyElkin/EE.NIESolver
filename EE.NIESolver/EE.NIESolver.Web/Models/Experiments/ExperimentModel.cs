using System;
using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
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
        public ExperimentResultModel[] Results { get; set; }

        public IEnumerable<ConstantItem> Methods;
        public IEnumerable<ConstantItem> Runners;
        public ExperimentRunModel RunParameters;

        public void InitializeEdit(IDataRepository repository)
        {
            Methods = repository.Query<DbMethod>()
                .Select(x => new ConstantItem
                {
                    Id = x.Id,
                    Value = x.Name,
                    Description = x.Description
                });
        }

        public void InitializeDisplay(IDataRepository repository)
        {
            Runners = repository.Query<DbRunnerType>()
                .Select(x => new ConstantItem
                {
                    Value = x.Name,
                    Id = x.Id
                });

            var runVariableTypeId = MethodParameterTypes.RunVariable.GetId();
            var parameters = repository.Query<DbMethodParameter>()
                .Where(x => x.MethodId == MethodId && x.ParameterTypeId == runVariableTypeId)
                .ToArray();
            RunParameters = new ExperimentRunModel
            {
                Parameters = parameters.ToDictionary(x => x.Code, x => string.Empty)
            };

            Results = repository.Query<DbExperimentResult>()
                .Where(x => x.ExperimentId == Id)
                .Select(x => new ExperimentResultModel
                {
                    Id = x.Id,
                    Status = x.Status.Description,
                    Date = x.Date,
                    RunnerName = x.RunnerType.Name,
                    Parameters = x.Parameters.ToDictionary(p => p.Code, p => p.Value)
                })
                .ToArray();
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

    public class ExperimentResultModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string RunnerName { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public DateTime Date { get; set; }
    }
}
