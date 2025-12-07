using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Validators
{
    public class RegisterBusinessContactValidator
    {
        private readonly BusinessContactRepository _businessContactRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterBusinessContactValidator(
            BusinessContactRepository businessContactRepository,
            BusinessRepository businessRepositor)
        {
            _businessContactRepository = businessContactRepository;
            _businessRepository = businessRepositor;
        }

        public Notification Validate(RegisterBusinessContactRequest request)
        {
            Notification notification = new();


            string firstName = string.IsNullOrWhiteSpace(request.FirstName) ? "" : request.FirstName.Trim();

            if (string.IsNullOrWhiteSpace(firstName))
                notification.AddError(BusinessContactStatic.FirstNameMsgErrorRequiered);

            if (firstName.Length > BusinessContactStatic.FirstNameMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.FirstNameMsgErrorMaxLength, BusinessContactStatic.FirstNameMaxLength.ToString()));


            if (notification.HasErrors())
            {
                return notification;
            }



            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessContactStatic.BusinessIdMsgErrorNotFound);

            return notification;
        }
    }
}
