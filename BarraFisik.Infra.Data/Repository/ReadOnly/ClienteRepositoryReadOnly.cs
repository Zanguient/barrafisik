using System.Collections.Generic;
using System.Data;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class ClienteRepositoryReadOnly : RepositoryBaseReadOnly, IClienteRepositoryReadOnly
    {
        public IEnumerable<Cliente> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                var query = @"Select * from cliente c ";

                cn.Open();
                var clientes = cn.Query<Cliente>(query);
                cn.Close();

                return clientes;
            }
        }
    }
}