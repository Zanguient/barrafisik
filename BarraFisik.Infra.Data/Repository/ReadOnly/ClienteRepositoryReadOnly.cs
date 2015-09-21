using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class ClienteRepositoryReadOnly : RepositoryBaseReadOnly, IClienteRepositoryReadOnly
    {
        public IEnumerable<Cliente> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"Select * from cliente c where c.IsAtivo = 1";

                cn.Open();
                var clientes = cn.Query<Cliente>(query);
                cn.Close();

                return clientes;
            }
        }

        public ClienteHorario GetByClienteId(Guid id)
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"  Select distinct * from cliente c 
                                left join horario h
                                on c.ClienteId = h.ClienteId
                                where c.ClienteId = '"+ id+"'";

                cn.Open();
                var clienteHorario = cn.Query<ClienteHorario>(query).FirstOrDefault();
                cn.Close();

                return clienteHorario;
            }
        }

        public void UpdateClientesPendentes(int mes, int ano)
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"  update c set c.Situacao = 'Pendente'
                                from Cliente c
                                where not exists 
                                        (
                                            select * from Mensalidades m 
                                                where m.MesReferencia >= "+mes+ "and "+ 
                                                "m.AnoReferencia = "+ano+" and "+  
                                                "c.ClienteId = m.ClienteId" +
                                        ") and c.IsAtivo = 1 and c.Situacao != 'Pendente'";

                cn.Open();
                var clienteHorario = cn.Execute(query);
                cn.Close();
                
            }
        }

        public IEnumerable<Cliente> GetClientesSituacao(string situacao)
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"Select * from cliente c where c.IsAtivo = 1 and c.Situacao = '"+situacao+"'";

                cn.Open();
                var clientes = cn.Query<Cliente>(query);
                cn.Close();

                return clientes;
            }
        }
    }
}