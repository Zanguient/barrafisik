using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Domain.Validation.Mensalidades;

namespace BarraFisik.Domain.Services
{
    public class ReceitasService : ServiceBase<Receitas>, IReceitasService
    {
        private readonly IReceitasRepository _receitasRepository;
        private readonly IReceitasRepositoryReadOnly _receitasRepositoryReadOnly;

        public ReceitasService(IReceitasRepository receitasRepository, IReceitasRepositoryReadOnly receitasRepositoryReadOnly) : base(receitasRepository)
        {
            _receitasRepository = receitasRepository;
            _receitasRepositoryReadOnly = receitasRepositoryReadOnly;
        }

        public ValidationResult AddMensalidade(Receitas receita)
        {
            var resultado = new ValidationResult();

            resultado = VerificaMensalidadeUnica(receita);
            if (!resultado.IsValid)
            {
                resultado.AdicionarErro(receita.ResultadoValidacao);
                return resultado;
            }

            base.Add(receita);
            return resultado;
        }

        public IEnumerable<Receitas> GetAvaliacaoCliente(Guid? idCliente)
        {
            return _receitasRepositoryReadOnly.GetAvaliacaoCliente(idCliente);
        }

        public IEnumerable<Receitas> GetMensalidadesCliente(Guid? idCliente)
        {
            return _receitasRepositoryReadOnly.GetMensalidadesCliente(idCliente);
        }

        public IEnumerable<Receitas> GetReceitas()
        {
            return _receitasRepositoryReadOnly.GetReceitas();
        }

        public IEnumerable<Receitas> SearchReceitas(SearchReceita sr)
        {
            return _receitasRepositoryReadOnly.SearchReceitas(sr);
        }

        public ValidationResult VerificaMensalidadeUnica(Receitas mensalidade)
        {
            var fiscal = new MensalidadeUnica(_receitasRepositoryReadOnly, _receitasRepository);

            var result = fiscal.Validar(mensalidade);

            return result;
        }
    }
}