using System;
using System.Linq;
using System.Linq.Expressions;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using EE.NIESolver.DataLayer.Repositories;

namespace EE.NIESolver.DataLayer.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly SolverContext _context;

        public DataRepository(SolverContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Select<TEntity>() where TEntity : class, IEntity
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Select<TEntity>(Expression<Func<TEntity, bool>> selector)
            where TEntity : class, IEntity
        {
            return _context.Set<TEntity>().Where(selector);
        }

        public TEntity Get<TEntity>(int id) where TEntity : class, IDomainEntity
        {
            return Select<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> selector) where TEntity : class, IDomainEntity
        {
            return Select<TEntity>().FirstOrDefault(selector);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IDomainEntity
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Add<TEntity>(params TEntity[] entities) where TEntity : class, IDomainEntity
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(params TEntity[] entities) where TEntity : class, IEntity
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Apply()
        {
            _context.SaveChanges();
        }
    }
}