using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IReceitasAvaliacaoFisicaAppService : IDisposable
    {
        void Add(ReceitasAvaliacaoFisicaViewModel receitasAvaliacaoFisicaViewModel);
        ReceitasAvaliacaoFisicaViewModel GetById(Guid id);
        IEnumerable<ReceitasAvaliacaoFisicaViewModel> GetAll();
        IEnumerable<ReceitasAvaliacaoFisicaViewModel> GetByCliente(Guid id);
        void Update(ReceitasAvaliacaoFisicaViewModel receitasAvaliacaoFisicaViewModel);
        void Remove(Guid id);

    }
}