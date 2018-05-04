using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("MathodParameter", Schema = "solver")]
    public class MethodParameterEntity : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        
        public int MethodId { get; set; }
        [ForeignKey(nameof(MethodId))]
        public virtual MethodEntity Method { get; set; }

        public int ParameterTypeId { get; set; }
        [ForeignKey(nameof(ParameterTypeId))]
        public virtual ConstantEntity ParameterType { get; set; }
    }
}