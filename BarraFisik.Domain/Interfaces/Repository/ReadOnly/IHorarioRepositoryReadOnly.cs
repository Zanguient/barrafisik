using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IHorarioRepositoryReadOnly
    {
        TotalHorario ListaHorarios();
    }
}