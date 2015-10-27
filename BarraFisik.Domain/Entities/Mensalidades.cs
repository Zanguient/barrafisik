﻿using System;
using BarraFisik.Domain.Interfaces.Validation;
using BarraFisik.Domain.Validation.Mensalidades;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Entities
{
    public class Mensalidades
    {
        public Mensalidades()
        {
            MensalidadesId = Guid.NewGuid();
        }

        public Guid MensalidadesId { get; set; }
        public decimal ValorPago { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime DataPagamento { get; set; }
        public virtual Cliente Cliente { get; set; }

        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public string Nome { get; set; }

        public bool isPersonal { get; set; }
        public decimal ValorPersonal { get; set; }

        public ValidationResult ResultadoValidacao { get; private set; }
    }
}