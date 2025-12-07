using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Application.Validators
{
    public class EditTaxValidator :Validator
    {
        private readonly TaxRepository _taxRepository;

        public EditTaxValidator(TaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public Notification Validate(EditTaxRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _taxRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _taxRepository.CodeTakenForEdit(request.Id, request.Code);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}
