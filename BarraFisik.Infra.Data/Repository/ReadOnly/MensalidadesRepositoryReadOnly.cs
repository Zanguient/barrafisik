using System;
using System.Collections.Generic;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class MensalidadesRepositoryReadOnly : RepositoryBaseReadOnly, IMensalidadesRepositoryReadOnly
    {
        public IEnumerable<Mensalidades> GetMensalidadesCliente(Guid id)
        {
            using (var cn = Connection)
            {
                var query = @"  Select distinct * from mensalidades m 
                                inner join TipoPagamento tp on m.TipoPagamentoId = tp.TipoPagamentoId
                                where m.ClienteId = '" + id + "'";

                cn.Open();
                var mensalidades = cn.Query<Mensalidades, TipoPagamento, Mensalidades>(
                    query,
                    (m, tp) => {
                        m.TipoPagamento = tp;
                        return m;
                    }, splitOn: "MensalidadesId, TipoPagamentoId");                    
                cn.Close();

                return mensalidades;
            }
        }

        public bool ExisteMensalidade(Mensalidades mensalidade)
        {
            using (var cn = Connection)
            {
                var query = @"  SELECT CASE WHEN EXISTS (
                                SELECT *
                                FROM Mensalidades m 
                                WHERE	m.ClienteId = '"+mensalidade.ClienteId+ "' and "+
			                            "m.AnoReferencia = '"+mensalidade.AnoReferencia+"' and " +
			                            "m.MesReferencia = '"+mensalidade.MesReferencia+"' and " +
                                        "m.MensalidadesId != '" + mensalidade.MensalidadesId + "' "+
                                ") THEN CAST(1 AS INT) ELSE CAST(0 AS INT) END ";

                cn.Open();
                var valido = cn.Query<int>(query).First();
                cn.Close();
                if (valido == 1)
                {
                    return true;
                }
                return false;                
            }
        }      
    }
}