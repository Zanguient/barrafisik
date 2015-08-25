using BarraFisik.Infra.Data.Interfaces;

namespace BarraFisik.Application.Interfaces
{
    public interface IAppServiceBase<TContext> where TContext : IDbContext
    {
        void BeginTransaction();
        void Commit();
    }
}