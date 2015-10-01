using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class ValoresRepositoryReadOnly : RepositoryBaseReadOnly, IValoresRepositoryReadOnly
    {
        public Valores GetValorCliente(int qtdDias, int horario)
        {
            using (var cn = Connection)
            {
                var query = @"Select * from valores v where v.QtdDias = " + qtdDias + "and " +
                            horario + "between v.HorarioInicio and v.HorarioFim";

                cn.Open();
                var valorCliente = cn.Query<Valores>(query).FirstOrDefault();
                cn.Close();

                return valorCliente;
            }
        }
    }
}