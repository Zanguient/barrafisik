using System;
using System.Data.Entity;
using BarraFisik.Infra.Data.Interfaces;

namespace BarraFisik.Infra.Data.Context
{
    public class BaseContext : DbContext, IDbContext
    {
        public BaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}