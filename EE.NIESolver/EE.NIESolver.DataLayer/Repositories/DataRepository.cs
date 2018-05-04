﻿using System;
using System.Linq;
using System.Linq.Expressions;
using EE.NIESolver.DataLayer.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EE.NIESolver.DataLayer.Repositories
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludeCollectionAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public IncludeCollectionAttribute(string propertyName = null)
        {
            
        }
    }

    public class DataRepository : IDataRepository
    {
        private readonly SolverContext _context;

        public DataRepository(SolverContext context)
        {
            _context = context;
        }
        
        public IQueryable<TEntity> Select<TEntity>() where TEntity : class, IEntity
        {
            IQueryable<TEntity> result =  _context.Set<TEntity>();
            var properties = typeof(TEntity).GetProperties()
                .Where(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(IncludeCollectionAttribute)));
            foreach (var property in properties)
            {
                var attribute = property
                    .GetCustomAttributes(typeof(IncludeCollectionAttribute), false)
                    .OfType<IncludeCollectionAttribute>()
                    .First();
                var propertyName = !string.IsNullOrEmpty(attribute.PropertyName)
                    ? attribute.PropertyName
                    : property.Name;
                result = result.Include(propertyName);
            }
            return result;
        }
        

        public IQueryable<TEntity> Select<TEntity>(Expression<Func<TEntity, bool>> selector)
            where TEntity : class, IEntity
        {
            return Select<TEntity>().Where(selector);
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