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
        public IEnumerable<ClienteHorario> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"Select * from cliente c 
                                inner join Horario h on c.ClienteId = h.ClienteId 
                                left join Valores v on c.ValoresId = v.ValoresId
                            where c.IsAtivo = 1";

                cn.Open();
                var clientes = cn.Query<ClienteHorario>(query);
                cn.Close();

                return clientes;
            }
        }

        public IEnumerable<ClienteHorario> GetClientesAll()
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"Select * from cliente c left join Horario h on c.ClienteId = h.ClienteId";

                cn.Open();
                var clientes = cn.Query<ClienteHorario>(query);
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

        public TotalInscritos GetTotalInscritos(int ano)
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"  Select distinct "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 1 and YEAR(c.DtInscricao) =  "+ano+")  as Janeiro,    "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 2 and YEAR(c.DtInscricao) =  "+ano+")  as Fevereiro,  "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 3 and YEAR(c.DtInscricao) =  "+ano+")  as Marco,      "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 4 and YEAR(c.DtInscricao) =  "+ano+")  as Abril,      "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 5 and YEAR(c.DtInscricao) =  "+ano+")  as Maio,       "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 6 and YEAR(c.DtInscricao) =  "+ano+")  as Junho,      "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 7 and YEAR(c.DtInscricao) =  "+ano+")  as Julho,      "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 8 and YEAR(c.DtInscricao) =  "+ano+")  as Agosto,     "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 9 and YEAR(c.DtInscricao) =  "+ano+")  as Setembro,   "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 10 and YEAR(c.DtInscricao) = "+ano+")  as Outubro,    "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 11 and YEAR(c.DtInscricao) = "+ano+")  as Novembro,   "+
                                    "(select count(*) from Cliente c where Month(c.DtInscricao) = 12 and YEAR(c.DtInscricao) = "+ano+")  as Dezembro,   "+
                                    "(select MAX(YEAR(c.DtInscricao)) from Cliente c )                                                   as UltimoAno,  "+
                                    "(select MIN(YEAR(c.DtInscricao)) from Cliente c )                                                   as PrimeiroAno "+                                
                                " from Cliente c";
                cn.Open();
                var inscritos = cn.Query<TotalInscritos>(query).FirstOrDefault();
                cn.Close();

                return inscritos;
            }
        }

        public ClienteHorario GetClientePerfil(Guid id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var lookup = new Dictionary<Guid, ClienteHorario>();
                    cn.Query<ClienteHorario, Mensalidades, ClienteHorario>(@"
                                        SELECT *
                                        FROM Cliente c
                                        LEFT JOIN Horario h on c.ClienteId = h.ClienteId
                                        FULL JOIN Mensalidades m ON c.ClienteId = m.ClienteId
                                        where c.ClienteId = '" + id+"'"
                                        , (c, m) => {
                        ClienteHorario clienteH;
                        if (!lookup.TryGetValue(c.ClienteId, out clienteH))
                        {
                            lookup.Add(c.ClienteId, clienteH = c);
                        }
                        //if (shop.Mensalidades == null)
                        //    shop.Mensalidades = new List<Mensalidades>();
                        clienteH.Mensalidades.Add(m);
                        
                        return clienteH;
                    }, splitOn: "ClienteId, MensalidadesId");

                var resultList = lookup.Values;

                cn.Close();
                return resultList.FirstOrDefault();
            }
        }
    }
}