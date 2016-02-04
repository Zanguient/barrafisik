using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IReceitasService : IServiceBase<Receitas>
    {
        IEnumerable<Receitas> GetReceitas();
        IEnumerable<Receitas> SearchReceitas(SearchReceita sr);
    }
}