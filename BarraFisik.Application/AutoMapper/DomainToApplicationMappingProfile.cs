using AutoMapper;
using BarraFisik.Application.Validation;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Application.AutoMapper
{
    public class DomainToApplicationMappingProfile : Profile
    {
        public override string ProfileName {
            get { return "DomainToApplicationMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ValidationError, ValidationAppError>();
            Mapper.CreateMap<ValidationResult, ValidationAppResult>();
        }
    }
}