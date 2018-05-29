using EE.NIESolver.DataLayer.Entities.Account;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Entities.Solver;
using Microsoft.EntityFrameworkCore;

namespace EE.NIESolver.DataLayer
{
    public class SolverContext : DbContext
    {
        public SolverContext(DbContextOptions<SolverContext> options) : base(options)
        { }

        #region Common - инфраструктурные объекты

        public DbSet<DbConstant> Constants { get; set; }

        #endregion

        #region Account - объекты пользователей

        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbRole> Roles { get; set; }

        #endregion

        #region Solver - объекты для вычислений

        private DbSet<DbMethod> Methods { get; set; }
        private DbSet<DbMethodParameter> MethodParameters { get; set; }
        private DbSet<DbExperiment> Experiments { get; set; }
        private DbSet<DbRunnerType> RunnerTypes { get; set; }
        private DbSet<DbSystemMethodParameter> SystemParameters { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbRole>()
                .HasKey(x => new {x.UserId, x.RoleId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
