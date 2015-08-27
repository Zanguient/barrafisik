using AutoMapper;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName {
            get { return "DomainToViewModelMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Cliente, ClienteViewModel>();
            Mapper.CreateMap<Horario, HorarioViewModel>();
        }
    }
}