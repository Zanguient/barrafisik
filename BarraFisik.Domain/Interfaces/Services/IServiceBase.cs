using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class 
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Dispose();
    }
}