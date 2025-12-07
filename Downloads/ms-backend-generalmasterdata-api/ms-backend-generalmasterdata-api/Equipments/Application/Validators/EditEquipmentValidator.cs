using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Equipments.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Validators
{
    public class EditEquipmentValidator : Validator
    {
        private readonly EquipmentRepository _equipmentRepository;

        private readonly PersonRepository _personRepository;
        private readonly MedicalAreaRepository _medicalAreaRepository;
        private readonly SubsidiaryRepository _subsidiary;
        public EditEquipmentValidator(EquipmentRepository equipmentRepository, PersonRepository personRepository, MedicalAreaRepository medicalAreaRepository, SubsidiaryRepository subsidiary)
        {
            _equipmentRepository = equipmentRepository;
            _personRepository = personRepository;
            _medicalAreaRepository = medicalAreaRepository;
            _subsidiary = subsidiary;
        }

        public Notification Validate(EditEquipmentRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            ValidatorString(notification, request.Supplier, EquipmentStatic.SupplierMaxLength, EquipmentStatic.SupplierMsgErrorMaxLength, EquipmentStatic.SupplierMsgErrorRequiered, true);
            ValidatorString(notification, request.Brand, EquipmentStatic.BrandMaxLength, EquipmentStatic.BrandMsgErrorMaxLength, EquipmentStatic.BrandMsgErrorRequiered, true);
            ValidatorString(notification, request.Model, EquipmentStatic.ModelMaxLength, EquipmentStatic.ModelMsgErrorMaxLength, EquipmentStatic.ModelMsgErrorRequiered, true);
            ValidatorString(notification, request.SerialNumber, EquipmentStatic.SerialNumberMaxLength, EquipmentStatic.SerialNumberMsgErrorMaxLength, EquipmentStatic.SerialNumberMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _equipmentRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);


            if (request.PersonDeviceManagerId == Guid.Empty)
            {
                notification.AddError(EquipmentStatic.PersonDeviceManagerIdMsgErrorRequiered);
                return notification;
            }

            var person = _personRepository.GetById(request.PersonDeviceManagerId);
            if (person == null)
                notification.AddError(EquipmentStatic.PersonDeviceManagerIdMsgErrorNotFound);

            if (request.MedicalAreaId == Guid.Empty)
            {
                notification.AddError(MedicalAreaStatic.MedicalAreaIdMsgErrorRequiered);
                return notification;
            }

            var medicalArea = _medicalAreaRepository.GetById(request.MedicalAreaId);
            if (medicalArea == null)
                notification.AddError(MedicalAreaStatic.MedicalAreaIdMsgErrorNotFound);

            if (request.SubsidiaryId == Guid.Empty)
            {
                notification.AddError(SubsidiaryStatic.SubsidiaryMsgErrorRequiered);
                return notification;
            }

            var subsidiary = _subsidiary.GetById(request.SubsidiaryId);
            if (subsidiary == null)
                notification.AddError(SubsidiaryStatic.SubsidiaryMsgErrorNotFound);

            return notification;
        }
    }
}
