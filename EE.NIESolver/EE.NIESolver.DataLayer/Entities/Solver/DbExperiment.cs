using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Interfaces;

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
        public virtual DbMethod Method { get; set; }
        public virtual ICollection<DbMethodParameterValue> Values { get; set; }
        public virtual ICollection<DbExperimentResult> Results { get; set; }
    }
}