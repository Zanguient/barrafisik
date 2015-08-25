using System.Web;
using BarraFisik.Infra.Data.Interfaces;

namespace BarraFisik.Infra.Data.Context
{
    public class ContextManager<TContext> : IContextManager<TContext> where TContext : IDbContext, new()
    {
        //OneContextPerRequest
        private const string ContextKey = "ContextManager.Context";

        public IDbContext GetContext()
        {
            if (HttpContext.Current.Items[ContextKey] == null)
                HttpContext.Current.Items[ContextKey] = new TContext();

            return (IDbContext)HttpContext.Current.Items[ContextKey];
        }
    }
}