using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("MethodParameterValue", Schema = "solver")]
    public class DbMethodParameterValue : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        [ForeignKey(nameof(ExperimentId))]
        public virtual DbExperiment Experiment { get; set; }
        public int ParameterId { get; set; }
        [ForeignKey(nameof(ParameterId))]
        public virtual DbMethodParameter Parameter { get; set; }
        public string Value { get; set; }
    }
}