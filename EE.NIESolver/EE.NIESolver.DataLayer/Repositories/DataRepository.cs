using System;
using System.Linq;
using System.Linq.Expressions;
using EE.NIESolver.DataLayer.Attrubutes;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EE.NIESolver.DataLayer.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly SolverContext _context;

        public DataRepository(SolverContext context)
        {
            _context = context;
        }
        
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        {
            IQueryable<TEntity> result =  _context.Set<TEntity>();
            var properties = typeof(TEntity).GetProperties()
                .Where(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(IncludePropertyAttribute)));
            foreach (var property in properties)
            {
                var attribute = property
                    .GetCustomAttributes(typeof(IncludePropertyAttribute), false)
                    .OfType<IncludePropertyAttribute>()
                    .First();
                var propertyName = !string.IsNullOrEmpty(attribute.PropertyName)
                    ? attribute.PropertyName
                    : property.Name;
                result = result.Include(propertyName);
            }
            return result;
        }
        

        public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> selector)
            where TEntity : class, IEntity
        {
            return Query<TEntity>().Where(selector);
        }

        public TEntity Get<TEntity>(int id) where TEntity : class, IDomainEntity
        {
            return Query<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> selector) where TEntity : class, IDomainEntity
        {
            return Query<TEntity>().FirstOrDefault(selector);
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