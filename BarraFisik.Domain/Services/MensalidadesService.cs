using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.Validation.Mensalidades;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Services
{
    public class MensalidadesService : ServiceBase<Mensalidades>, IMensalidadesService
    {
        private readonly IMensalidadesRepository _mensalidadesRepository;
        private readonly IMensalidadesRepositoryReadOnly _mensalidadesRepositoryReadOnly;

        public MensalidadesService(IMensalidadesRepository mensalidadesRepository, IMensalidadesRepositoryReadOnly mensalidadesRepositoryReadOnly) : base(mensalidadesRepository)
        {
            _mensalidadesRepository = mensalidadesRepository;
            _mensalidadesRepositoryReadOnly = mensalidadesRepositoryReadOnly;
        }

        public ValidationResult AdicionarMensalidade(Mensalidades mensalidade)
        {
            var resultado = new ValidationResult();

            resultado = VerificaMensalidadeUnica(mensalidade);
            if (!resultado.IsValid)
            {
                resultado.AdicionarErro(mensalidade.ResultadoValidacao);
                return resultado;
            }

            base.Add(mensalidade);
            return resultado;
        }

        public IEnumerable<Mensalidades> GetMensalidadesCliente(Guid id)
        {
            return _mensalidadesRepositoryReadOnly.GetMensalidadesCliente(id);
        }

        public ValidationResult VerificaMensalidadeUnica(Mensalidades mensalidade)
        {
            var fiscal = new MensalidadeUnica(_mensalidadesRepositoryReadOnly, _mensalidadesRepository);

            var result = fiscal.Validar(mensalidade);

            return result;
        }
    }
}