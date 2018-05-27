using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Attrubutes;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Repositories;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("Experiment", Schema = "solver")]
    public class DbExperiment : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public int? MethodId { get; set; }
        [ForeignKey(nameof(MethodId))]
        [IncludeProperty]
        public virtual DbMethod Method { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [IncludeProperty]
        public virtual ICollection<DbMethodParameterValue> Values { get; set; }
        [IncludeProperty]
        public virtual ICollection<DbExperimentResult> Results { get; set; }
    }
}