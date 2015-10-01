using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IValoresService : IServiceBase<Valores>
    {
        Valores GetValorCliente(int qtdDias, int horario);
    }
}