using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;
using BarraFisik.Infra.Data.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace BarraFisik.Infra.Data.Repository
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> 
        where TEntity : class
        where TContext : IDbContext, new()
    {
        private readonly ContextManager<TContext> _contextManager = ServiceLocator.Current.GetInstance<IContextManager<TContext>>() as ContextManager<TContext>;
        protected IDbSet<TEntity> DbSet;
        protected readonly IDbContext Context;

        public RepositoryBase()
        {
            Context = _contextManager.GetContext();
            DbSet = Context.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            DbSet.AddOrUpdate(obj);
        }

        public virtual TEntity GetById(Guid id)
        {
            var entry = DbSet.Find(id);
            if (entry != null)
                Context.Entry(entry).State = EntityState.Detached;
            return entry;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public void Update(TEntity obj)
        {
            var entry = Context.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
            //Context.SaveChanges();
        }

        public virtual void Remove(TEntity obj)
        {
            var entry = Context.Entry(obj);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(obj);
            DbSet.Remove(obj);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<TEntity> GetAll(int skip, int take)
        {
            return DbSet.ToList();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}