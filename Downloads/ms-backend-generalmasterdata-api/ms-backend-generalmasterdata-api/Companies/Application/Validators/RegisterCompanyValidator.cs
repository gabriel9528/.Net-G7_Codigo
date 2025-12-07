using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Companies.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Validators
{
    public class RegisterCompanyValidator
    {
        private readonly CompanyRepository _companyRepository;

        public RegisterCompanyValidator(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Notification Validate(RegisterCompanyRequest request)
        {
            Notification notification = new();

            string description = string.IsNullOrWhiteSpace(request.Description) ? "" : request.Description.Trim();
            if (string.IsNullOrWhiteSpace(description))
                notification.AddError("descripcion es obligatoria");

            if (notification.HasErrors())
            {
                return notification;
            }


            Company? company = _companyRepository.GetbyDescription(request.Description);
            if (company != null)
                notification.AddError("descripcion ya existe");

            return notification;
        }
    }
}
