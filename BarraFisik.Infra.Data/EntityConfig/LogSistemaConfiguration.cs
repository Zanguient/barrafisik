﻿using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class LogSistemaConfiguration : EntityTypeConfiguration<LogSistema>
    {
        public LogSistemaConfiguration()
        {
            ToTable("LogSistema");

            HasKey(l => l.LogSistemaId);

            Property(l => l.Data).IsRequired();
            Property(l => l.UserId).IsRequired();
            Property(l => l.UsuarioNome).IsRequired();
            Property(l => l.Acao).IsRequired();
            Property(l => l.Tabela).IsRequired();
            Property(l => l.RegistroId).IsRequired();
            Property(l => l.Descricao).IsOptional().HasMaxLength(250);
        }
    }
}