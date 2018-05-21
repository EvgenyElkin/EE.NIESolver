using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Common;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("Runner", Schema = "solver")]
    public class DbRunnerType
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int MethodTypeId { get; set; }
        [ForeignKey(nameof(MethodTypeId))]
        public virtual DbConstant MethodType { get; set; }
    }
}