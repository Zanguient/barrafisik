using AutoMapper;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName {
            get { return "DomainToViewModelMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Cliente, ClienteHorarioViewModel>();
            Mapper.CreateMap<Horario, ClienteHorarioViewModel>();

            Mapper.CreateMap<Cliente, ClienteViewModel>();
            Mapper.CreateMap<Horario, HorarioViewModel>();
            Mapper.CreateMap<TotalHorario, TotalHorarioViewModel>();
            Mapper.CreateMap<ClienteHorario, ClienteHorarioViewModel>();
            Mapper.CreateMap<FilaEspera, FilaEsperaViewModel>();
            Mapper.CreateMap<Mensalidades, MensalidadesViewModel>();
            Mapper.CreateMap<TotalInscritos, TotalInscritosViewModel>();
            Mapper.CreateMap<Valores, ValoresViewModel>();
            Mapper.CreateMap<ReceitasAvaliacaoFisica, ReceitasAvaliacaoFisicaViewModel>();
            Mapper.CreateMap<CategoriaFinanceira, CategoriaFinanceiraViewModel>();
            Mapper.CreateMap<SubCategoriaFinanceira, SubCategoriaFinanceiraViewModel>();
            Mapper.CreateMap<Receitas, ReceitasViewModel>();
            Mapper.CreateMap<Despesas, DespesasViewModel>();
            Mapper.CreateMap<RelatorioFinanceiro, RelatorioFinanceiroViewModel>();
            Mapper.CreateMap<RelatorioFinanceiroTotalMeses, RelatorioFinanceiroTotalMesesViewModel>();
            Mapper.CreateMap<TipoPagamento, TipoPagamentoViewModel>();
            Mapper.CreateMap<Armazem, ArmazemViewModel>();
            Mapper.CreateMap<Funcionarios, FuncionariosViewModel>();
            Mapper.CreateMap<Fornecedores, FornecedoresViewModel>();
            Mapper.CreateMap<SearchDespesa, SearchDespesasViewModel>();
            Mapper.CreateMap<SearchReceita, SearchReceitasViewModel>();
            Mapper.CreateMap<Produtos, ProdutosViewModel>();
            Mapper.CreateMap<ProdutosCategoria, ProdutosCategoriaViewModel>();
            Mapper.CreateMap<Estoque, EstoqueViewModel>();
            Mapper.CreateMap<MovimentacaoEstoque, MovimentacaoEstoqueViewModel>();
        }
    }
}