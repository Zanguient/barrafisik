using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IReceitasService : IServiceBase<Receitas>
    {
        IEnumerable<Receitas> GetReceitas();
    }
}