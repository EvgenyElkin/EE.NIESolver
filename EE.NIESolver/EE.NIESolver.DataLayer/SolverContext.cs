﻿using EE.NIESolver.DataLayer.Entities.Account;
using EE.NIESolver.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EE.NIESolver.DataLayer
{
    public class SolverContext : DbContext
    {
        public SolverContext(DbContextOptions<SolverContext> options) : base(options)
        { }

        #region Common - инфраструктурные объекты

        public DbSet<ConstantEntity> Constants { get; set; }

        #endregion

        #region Account - объекты пользователей

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>()
                .HasKey(x => new {x.UserId, x.RoleId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
