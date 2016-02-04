using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IReceitasRepositoryReadOnly
    {
        IEnumerable<Receitas> GetReceitas();
        IEnumerable<Receitas> SearchReceitas(SearchReceita sr);
    }
}