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
        }
    }
}