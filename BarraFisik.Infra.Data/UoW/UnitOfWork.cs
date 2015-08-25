using System;
using BarraFisik.Infra.Data.Context;
using BarraFisik.Infra.Data.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace BarraFisik.Infra.Data.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext: IDbContext, new()
    {
        private readonly ContextManager<TContext> _contextManager =
            ServiceLocator.Current.GetInstance<IContextManager<TContext>>() as ContextManager<TContext>;

        private readonly IDbContext _dbContext;
        private bool _diposed;

        public UnitOfWork()
        {
            _dbContext = _contextManager.GetContext();
        }

        public void BeginTransaction()
        {
            _diposed = false;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_diposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}