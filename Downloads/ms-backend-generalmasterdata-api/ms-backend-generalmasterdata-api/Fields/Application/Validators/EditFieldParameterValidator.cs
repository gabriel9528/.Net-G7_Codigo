using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Validators
{
    public class EditFieldParameterValidator : Validator
    {

        public EditFieldParameterValidator()
        {
        }

        public Notification Validate(EditFieldParameterRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.DefaultValue, FieldStatic.DefaultValueMaxLength, FieldStatic.DefaultValueMsgErrorMaxLength);
            ValidatorString(notification, request.Uom, FieldStatic.UomMaxLength, FieldStatic.UomMsgErrorMaxLength);
            ValidatorString(notification, request.Legend, FieldStatic.LegendMaxLength, FieldStatic.LegendMsgErrorMaxLength);

            return notification;
        }
    }
}
