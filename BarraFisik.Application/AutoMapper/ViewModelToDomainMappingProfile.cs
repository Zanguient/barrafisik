using AutoMapper;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName {
            get { return "ViewModelToDomainMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ClienteHorarioViewModel, Cliente>();
            Mapper.CreateMap<ClienteHorarioViewModel, Horario>();

            Mapper.CreateMap<ClienteViewModel, Cliente>();
            Mapper.CreateMap<HorarioViewModel, Horario>();
            Mapper.CreateMap<TotalHorarioViewModel, TotalHorario>();
            Mapper.CreateMap<ClienteHorarioViewModel, ClienteHorario>();
            Mapper.CreateMap<FilaEsperaViewModel, FilaEspera>();
            Mapper.CreateMap<MensalidadesViewModel, Mensalidades>();
            Mapper.CreateMap<TotalInscritosViewModel, TotalInscritos>();
            Mapper.CreateMap<ValoresViewModel, Valores>();
            Mapper.CreateMap<ReceitasAvaliacaoFisicaViewModel, ReceitasAvaliacaoFisica>();
            Mapper.CreateMap<CategoriaFinanceiraViewModel, CategoriaFinanceira>();
            Mapper.CreateMap<ReceitasViewModel, Receitas>();
            Mapper.CreateMap<DespesasViewModel, Despesas>();
            Mapper.CreateMap<RelatorioFinanceiroViewModel, RelatorioFinanceiro>();
            Mapper.CreateMap<RelatorioFinanceiroSearchViewModel, RelatorioFinanceiroSearch>();
            Mapper.CreateMap<TipoPagamentoViewModel, TipoPagamento>();
            Mapper.CreateMap<ArmazemViewModel, Armazem>();
            Mapper.CreateMap<FuncionariosViewModel, Funcionarios>();
            Mapper.CreateMap<FornecedoresViewModel, Fornecedores>();
        }
    }
}