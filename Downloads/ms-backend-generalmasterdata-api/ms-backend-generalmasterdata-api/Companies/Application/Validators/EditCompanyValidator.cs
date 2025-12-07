using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Companies.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Validators
{
    public class EditCompanyValidator
    {
        private readonly CompanyRepository _companyRepository;
       

        public EditCompanyValidator(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
           
        }

        public Notification ValidateRemove(Company company)
        {
            Notification notification = new();
            return notification;
        }
        public Notification Validate(EditCompanyRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError("Id es obligatorio");

            string description = string.IsNullOrWhiteSpace(request.Description) ? "" : request.Description.Trim();

            if (string.IsNullOrWhiteSpace(description))
                notification.AddError("descripcion es obligatoria");

            bool descriptionTakenForEdit = _companyRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError("Ya existe descripcion");

            return notification;
        }
    }
}
