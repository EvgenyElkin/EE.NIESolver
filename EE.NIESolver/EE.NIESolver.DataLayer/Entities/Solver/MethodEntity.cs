using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Repositories;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("Method", Schema = "solver")]
    public class MethodEntity : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [IncludeCollection]
        public virtual ICollection<MethodParameterEntity> Parameteres { get; set; }
    }
}
