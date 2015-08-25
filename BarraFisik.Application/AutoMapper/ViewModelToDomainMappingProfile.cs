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
            Mapper.CreateMap<ClienteViewModel, Cliente>();
        }
    }
}