using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Common
{
    [Table("Constant", Schema = "commmon")]
    public class ConstantEntity : IDomainEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
