using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Account
{
    [Table("User", Schema = "account")]
    public class UserEntity : IDomainEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PassHash { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; }
    }
}
