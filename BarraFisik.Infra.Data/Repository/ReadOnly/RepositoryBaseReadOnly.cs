using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class RepositoryBaseReadOnly
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["BarraFisikConnectionString"].ConnectionString);
            }
        }
    }
}