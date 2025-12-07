using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Application.Validators
{
    public class RegisterTaxValidator: Validator
    {
        private readonly TaxRepository _taxRepository;

        public RegisterTaxValidator(TaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public Notification Validate(RegisterTaxRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }


            Tax? tax = _taxRepository.GetbyDescription(request.Description);
            if (tax != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            tax = _taxRepository.GetbyCode(request.Code);
            if (tax != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}
