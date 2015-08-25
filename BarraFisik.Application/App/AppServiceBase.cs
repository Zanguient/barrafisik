using BarraFisik.Application.Interfaces;
using BarraFisik.Application.Validation;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace BarraFisik.Application.App
{
    public class AppServiceBase<TContext> : IAppServiceBase<TContext> where TContext : IDbContext, new()
    {
        private IUnitOfWork<TContext> _uow;

        public void BeginTransaction()
        {
            _uow = ServiceLocator.Current.GetInstance<IUnitOfWork<TContext>>();
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.SaveChanges();
        }

        protected ValidationAppResult DomainToApplicationResult(ValidationResult result)
        {
            var validationAppResult = new ValidationAppResult();

            foreach (var validationError in result.Erros)
            {
                validationAppResult.Erros.Add(new ValidationAppError(validationError.Message));
            }
            validationAppResult.IsValid = result.IsValid;

            return validationAppResult;
        }
    }
}