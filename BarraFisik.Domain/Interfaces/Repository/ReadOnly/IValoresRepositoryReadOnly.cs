using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IValoresRepositoryReadOnly
    {
        Valores GetValorCliente(int qtdDias, int horario);
    }
}