using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Factories;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models.Methods
{
    public class MethodModel : IModel<DbMethod>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MethodExpression { get; set; }
        public MethodParameterModel[] Parameteres { get; set; }
        public int MethodTypeId { get; set; }
        public IEnumerable<ConstantItem> MethodParameterTypes { get; set; }
        public IEnumerable<ConstantItem> MethodTypes { get; set; }
        public Dictionary<int, DbMethodParameter[]> SystemParameters { get; set; }

        public MethodModel()
        {
            Parameteres = new MethodParameterModel[0];
        }

        public void SetModel(DbMethod entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            MethodTypeId = entity.MethodTypeId;
            MethodExpression = entity.MethodExpression;
            Parameteres = entity.Parameteres.Select(e =>
            {
                var m = new MethodParameterModel();
                m.SetModel(e);
                return m;
            }).ToArray();
        }

        public void InitializeEdit(IDataRepository repository)
        {
            MethodParameterTypes = ConstantConext.Current.GetValues<MethodParameterTypes>();
            MethodTypes = ConstantConext.Current.GetValues<MethodTypes>();
            SystemParameters = new Dictionary<int, DbMethodParameter[]>();
            var parameters = repository.Query<DbSystemMethodParameter>().ToArray();
            foreach (var methodType in MethodTypes)
            {

                SystemParameters[methodType.Id] = parameters
                    .Where(x => x.MethodTypeId == methodType.Id)
                    .Select(x => new DbMethodParameter
                    {
                        Code = x.Code,
                        Name = x.Name,
                        Description = x.Description,
                        IsSystem = true,
                        ParameterTypeId = x.ParameterTypeId
                    })
                    .ToArray();
            }
        }

        public void ApplyModel(DbMethod entity)
        {
            entity.Name = Name;
            entity.MethodTypeId = MethodTypeId;
            entity.Description = Description;
            entity.MethodExpression = MethodExpression;
            if (entity.Parameteres == null)
            {
                entity.Parameteres = new List<DbMethodParameter>();
            }
            entity.Parameteres.ApplyCollection(Parameteres.ToArray());
        }
    }
}
