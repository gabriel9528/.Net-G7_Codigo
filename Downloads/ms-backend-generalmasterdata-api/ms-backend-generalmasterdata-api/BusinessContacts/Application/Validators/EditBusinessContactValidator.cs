using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Validators
{
    public class EditBusinessContactValidator
    {
        private readonly BusinessContactRepository _businessContactRepository;

        public EditBusinessContactValidator(BusinessContactRepository businessContactRepository)
        {
            _businessContactRepository = businessContactRepository;
        }

        public Notification Validate(EditBusinessContactRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(BusinessContactStatic.IdMsgErrorRequiered);

            string firstName = string.IsNullOrWhiteSpace(request.FirstName) ? "" : request.FirstName.Trim();

            if (string.IsNullOrWhiteSpace(firstName))
                notification.AddError(BusinessContactStatic.FirstNameMsgErrorRequiered);

            if (firstName.Length > BusinessContactStatic.FirstNameMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.FirstNameMsgErrorMaxLength, BusinessContactStatic.FirstNameMaxLength.ToString()));

            string lastName = string.IsNullOrWhiteSpace(request.LastName) ? "" : request.LastName.Trim();

            if (string.IsNullOrWhiteSpace(lastName))
                notification.AddError(BusinessContactStatic.lastNameMsgErrorRequiered);

            if (lastName.Length > BusinessContactStatic.LastNameMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.LastNameMsgErrorMaxLength, BusinessContactStatic.LastNameMaxLength.ToString()));

            string position = string.IsNullOrWhiteSpace(request.Position) ? "" : request.Position.Trim();

            if (position.Length > BusinessContactStatic.PositionMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.PositionMsgErrorMaxLength, BusinessContactStatic.PositionMaxLength.ToString()));

            string cellPhone = string.IsNullOrWhiteSpace(request.CellPhone) ? "" : request.CellPhone.Trim();

            if (cellPhone.Length > BusinessContactStatic.CellPhoneMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.CellPhoneMsgErrorMaxLength, BusinessContactStatic.CellPhoneMaxLength.ToString()));

            string phone = string.IsNullOrWhiteSpace(request.Phone) ? "" : request.Phone.Trim();

            if (phone.Length > BusinessContactStatic.PositionMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.PhoneMsgErrorMaxLength, BusinessContactStatic.PhoneMaxLength.ToString()));

            string secondPhone = string.IsNullOrWhiteSpace(request.SecondPhone) ? "" : request.SecondPhone.Trim();

            if (secondPhone.Length > BusinessContactStatic.PositionMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.SecondPhoneMsgErrorMaxLength, BusinessContactStatic.SecondPhoneMaxLength.ToString()));

            string secondCellPhone = string.IsNullOrWhiteSpace(request.SecondCellPhone) ? "" : request.SecondCellPhone.Trim();

            if (secondCellPhone.Length > BusinessContactStatic.SecondCellPhoneMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.PositionMsgErrorMaxLength, BusinessContactStatic.SecondCellPhoneMaxLength.ToString()));

            string email = string.IsNullOrWhiteSpace(request.Email) ? "" : request.Email.Trim();

            if (email.Length > BusinessContactStatic.EmailMaxLength)
                notification.AddError(String.Format(BusinessContactStatic.EmailMsgErrorMaxLength, BusinessContactStatic.EmailMaxLength.ToString()));

            if (notification.HasErrors())
            {
                return notification;
            }

            return notification;
        }
    }
}
