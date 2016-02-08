using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;
using System;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IReceitasService : IServiceBase<Receitas>
    {
        IEnumerable<Receitas> GetReceitas();
        IEnumerable<Receitas> SearchReceitas(SearchReceita sr);
        ValidationResult AddMensalidade(Receitas receita);
        IEnumerable<Receitas> GetMensalidadesCliente(Guid? idCliente);
        IEnumerable<Receitas> GetAvaliacaoCliente(Guid? idCliente);
    }
}