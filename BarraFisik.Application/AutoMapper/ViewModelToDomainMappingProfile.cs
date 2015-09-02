using AutoMapper;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;

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
        }
    }
}