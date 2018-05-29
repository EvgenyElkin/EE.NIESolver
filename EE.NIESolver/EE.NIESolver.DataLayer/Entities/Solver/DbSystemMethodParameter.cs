using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("SystemMethodParameter", Schema = "solver")]
    public class DbSystemMethodParameter : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ParameterTypeId { get; set; }
        [ForeignKey(nameof(ParameterTypeId))]
        public virtual DbConstant ParameterType { get; set; }
        public int MethodTypeId { get; set; }
        [ForeignKey(nameof(MethodTypeId))]
        public virtual DbConstant MethodType { get; set; }
    }
}