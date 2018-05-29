using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Attrubutes;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Entities.Common;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("Method", Schema = "solver")]
    public class DbMethod : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MethodExpression { get; set; }
        public int MethodTypeId { get; set; }
        [ForeignKey(nameof(MethodTypeId))]
        public virtual DbConstant MethodType { get; set; }

        [IncludeProperty]
        public virtual ICollection<DbMethodParameter> Parameteres { get; set; }

        [IncludeProperty]
        public virtual ICollection<DbExperiment> Experiments { get; set; }
    }
}
