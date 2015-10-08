using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class LogMensalidadesRepository : RepositoryBase<LogMensalidades, BarraFisikContext>, ILogMensalidadesRepository
    {
         
    }
}