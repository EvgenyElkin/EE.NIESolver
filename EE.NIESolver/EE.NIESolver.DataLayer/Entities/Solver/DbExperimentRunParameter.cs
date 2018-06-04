using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("DbExperimentRunParameter", Schema = "solver")]
    public class DbExperimentRunParameter : IDomainEntity
    {
        [Key]
        public int Id { get; set; }

        public int ExperimentReslutId { get; set; }
        [ForeignKey(nameof(ExperimentReslutId))]
        public virtual DbExperimentResult ExperimentResult { get; set; }

        public string Code { get; set; }
        public string Value { get; set; }
    }
}