using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Common;

namespace EE.NIESolver.DataLayer.Entities.Account
{
    [Table("Role", Schema = "account")]
    public class RoleEntity
    {
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        [Required]
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public ConstantEntity Role { get; set; }
    }
}