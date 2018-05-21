using System.Collections.Generic;
using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Solver;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using EE.NIESolver.Web.Models.Interfaces;

namespace EE.NIESolver.Web.Models.Methods
{
    public class MethodModel : IModel<DbMethod>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MethodParameterModel[] Parameteres { get; set; }
        public int MethodTypeId { get; set; }
        public IEnumerable<ConstantItem> MethodParameterTypes { get; set; }
        public IEnumerable<ConstantItem> MethodTypes { get; set; }

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
            MethodTypes = ConstantConext.Current.GetValues<MethodTypes>();
        }

        public void ApplyModel(DbMethod entity)
        {
            entity.Name = Name;
            entity.MethodTypeId = MethodTypeId;
            entity.Description = Description;
            if (entity.Parameteres == null)
            {
                entity.Parameteres = new List<DbMethodParameter>();
            }
            entity.Parameteres.ApplyCollection(Parameteres.ToArray());
        }
    }
}
