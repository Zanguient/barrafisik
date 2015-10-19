using System.Linq;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class HorarioRepositoryReadOnly : RepositoryBaseReadOnly, IHorarioRepositoryReadOnly
    {
        public TotalHorario ListaHorarios()
        {
            using (var cn = Connection)
            {
                var query = @"select distinct

                            (select count(*) from Horario where Segunda = 1 and HSegunda = 06) as Segunda06,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 07) as Segunda07,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 08) as Segunda08,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 09) as Segunda09,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 10) as Segunda10,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 14) as Segunda14,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 15) as Segunda15,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 16) as Segunda16,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 17) as Segunda17,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 18) as Segunda18,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 19) as Segunda19,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 20) as Segunda20,
                            (select count(*) from Horario where Segunda = 1 and HSegunda = 21) as Segunda21,

                            (select count(*) from Horario where Terca = 1 and HTerca = 06) as Terca06,
                            (select count(*) from Horario where Terca = 1 and HTerca = 07) as Terca07,
                            (select count(*) from Horario where Terca = 1 and HTerca = 08) as Terca08,
                            (select count(*) from Horario where Terca = 1 and HTerca = 09) as Terca09,
                            (select count(*) from Horario where Terca = 1 and HTerca = 10) as Terca10,
                            (select count(*) from Horario where Terca = 1 and HTerca = 14) as Terca14,
                            (select count(*) from Horario where Terca = 1 and HTerca = 15) as Terca15,
                            (select count(*) from Horario where Terca = 1 and HTerca = 16) as Terca16,
                            (select count(*) from Horario where Terca = 1 and HTerca = 17) as Terca17,
                            (select count(*) from Horario where Terca = 1 and HTerca = 18) as Terca18,
                            (select count(*) from Horario where Terca = 1 and HTerca = 19) as Terca19,
                            (select count(*) from Horario where Terca = 1 and HTerca = 20) as Terca20,
                            (select count(*) from Horario where Terca = 1 and HTerca = 21) as Terca21,

                            (select count(*) from Horario where Quarta = 1 and HQuarta = 06) as Quarta06,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 07) as Quarta07,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 08) as Quarta08,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 09) as Quarta09,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 10) as Quarta10,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 14) as Quarta14,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 15) as Quarta15,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 16) as Quarta16,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 17) as Quarta17,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 18) as Quarta18,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 19) as Quarta19,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 20) as Quarta20,
                            (select count(*) from Horario where Quarta = 1 and HQuarta = 21) as Quarta21,

                            (select count(*) from Horario where Quinta = 1 and HQuinta = 06) as Quinta06,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 07) as Quinta07,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 08) as Quinta08,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 09) as Quinta09,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 10) as Quinta10,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 14) as Quinta14,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 15) as Quinta15,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 16) as Quinta16,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 17) as Quinta17,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 18) as Quinta18,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 19) as Quinta19,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 20) as Quinta20,
                            (select count(*) from Horario where Quinta = 1 and HQuinta = 21) as Quinta21,

                            (select count(*) from Horario where Sexta = 1 and HSexta = 06) as Sexta06,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 07) as Sexta07,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 08) as Sexta08,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 09) as Sexta09,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 10) as Sexta10,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 14) as Sexta14,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 15) as Sexta15,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 16) as Sexta16,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 17) as Sexta17,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 18) as Sexta18,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 19) as Sexta19,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 20) as Sexta20,
                            (select count(*) from Horario where Sexta = 1 and HSexta = 21) as Sexta21

                            from Horario";
                cn.Open();
                var horarios = cn.Query<TotalHorario>(query).First();
                cn.Close();

                return horarios;
            }
        }
    }
}