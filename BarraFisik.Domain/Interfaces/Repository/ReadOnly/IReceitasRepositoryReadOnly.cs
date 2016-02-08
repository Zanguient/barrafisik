using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;
using System;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IReceitasRepositoryReadOnly
    {
        IEnumerable<Receitas> GetReceitas();
        IEnumerable<Receitas> SearchReceitas(SearchReceita sr);
        bool ExisteMensalidade(Receitas mensalidade);
        IEnumerable<Receitas> GetMensalidadesCliente(Guid? idCliente);
        IEnumerable<Receitas> GetAvaliacaoCliente(Guid? idCliente);
    }
}