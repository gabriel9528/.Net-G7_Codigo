using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Validators
{
    public class RegisterFieldParameterValidator : Validator
    {
        private readonly FieldRepository _fieldRepository;
        private readonly GenderRepository _genderRepository;
        private readonly SubsidiaryRepository _subsidiaryRepository;
        private readonly MedicalFormRepository _medicalFormRepository;

        public RegisterFieldParameterValidator(FieldRepository fieldRepository, GenderRepository genderRepository, SubsidiaryRepository subsidiaryRepository, MedicalFormRepository medicalFormRepository)
        {
            _fieldRepository = fieldRepository;
            _genderRepository = genderRepository;
            _subsidiaryRepository = subsidiaryRepository;
            _medicalFormRepository = medicalFormRepository;
        }

        public Notification Validate(RegisterFieldParameterRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.DefaultValue, FieldStatic.DefaultValueMaxLength, FieldStatic.DefaultValueMsgErrorMaxLength);
            ValidatorString(notification, request.Uom, FieldStatic.UomMaxLength, FieldStatic.UomMsgErrorMaxLength);
            ValidatorString(notification, request.Legend, FieldStatic.LegendMaxLength, FieldStatic.LegendMsgErrorMaxLength);

            Field? field = _fieldRepository.GetById(request.FieldId);

            if (field == null)
            {
                notification.AddError(FieldStatic.FieldMsgNotFound);
                return notification;
            }

            if (request.Range != null)
                //ValidateRanges(notification, request.Range, field.FieldType, _subsidiaryRepository, _genderRepository);

            if (notification.HasErrors())
                return notification;

            return notification;
        }

    }
}
