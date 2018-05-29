using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Attrubutes;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("ExperimentResult", Schema = "solver")]
    public class DbExperimentResult : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        [ForeignKey(nameof(ExperimentId))]
        public virtual DbExperiment Experiment { get; set; }
        public int RunnerTypeId { get; set; }
        [ForeignKey(nameof(RunnerTypeId))]
        public virtual DbRunnerType RunnerType { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Result { get; set; }
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual DbConstant Status { get; set; }

        [IncludeProperty]
        public virtual ICollection<DbExperimentRunParameter> Parameters { get; set; }
    }
}