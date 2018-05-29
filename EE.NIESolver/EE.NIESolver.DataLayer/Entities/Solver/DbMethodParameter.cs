﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.NIESolver.DataLayer.Attrubutes;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Entities.Interfaces;

namespace EE.NIESolver.DataLayer.Entities.Solver
{
    [Table("MethodParameter", Schema = "solver")]
    public class DbMethodParameter : IDomainEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }

        public int MethodId { get; set; }
        [ForeignKey(nameof(MethodId))]
        public virtual DbMethod Method { get; set; }

        public int ParameterTypeId { get; set; }
        [ForeignKey(nameof(ParameterTypeId))]
        public virtual DbConstant ParameterType { get; set; }

        [IncludeProperty]
        public virtual ICollection<DbMethodParameterValue> Values { get; set; }
    }
}