using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Equipments.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Services
{
    public class EquipmentApplicationService(AnaPreventionContext context, RegisterEquipmentValidator registerEquipmentValidator, EditEquipmentValidator editEquipmentValidator, EquipmentRepository equipmentRepository, AttachmentApplicationService attachmentApplicationService, EquipmentCalibrationRepository equipmentCalibrationRepository)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly RegisterEquipmentValidator _registerEquipmentValidator = registerEquipmentValidator;
        private readonly EditEquipmentValidator _editEquipmentValidator = editEquipmentValidator;
        private readonly EquipmentRepository _equipmentRepository = equipmentRepository;
        private readonly AttachmentApplicationService _attachmentApplicationService = attachmentApplicationService;
        private readonly EquipmentCalibrationRepository _equipmentCalibrationRepository = equipmentCalibrationRepository;

        public async Task<Result<RegisterEquipmentResponse, Notification>> RegisterEquipment(RegisterEquipmentRequest request, Guid userId, Guid companyId)
        {
            Notification notification = _registerEquipmentValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            string description = request.Description;
            string brand = request.Brand;
            string model = request.Model;
            string supplier = request.Supplier;
            string serialNumber = request.SerialNumber;


            Guid medicalAreaId = request.MedicalAreaId;
            Guid subsidiaryId = request.SubsidiaryId;
            Guid personDeviceManagerId = request.PersonDeviceManagerId;
            string code = _equipmentRepository.GenerateCode(companyId);
            Guid equipmentId = Guid.NewGuid();

            Equipment equipment = new(description, brand, model, serialNumber, medicalAreaId, subsidiaryId, code, companyId, supplier, personDeviceManagerId, equipmentId);

            _equipmentRepository.Save(equipment);
            if (request.EquipmentCalibrations != null)
            {
                foreach (var calibrations in request.EquipmentCalibrations)
                {
                    var DatecalibrationResult = Date.Create(calibrations.Datecalibration);
                    if (DatecalibrationResult.IsFailure)
                        return DatecalibrationResult.Error;

                    DateTime datecalibration = DatecalibrationResult.Value.DateTimeValue;

                    var NextDatecalibrationResult = Date.Create(calibrations.NextDatecalibration);
                    if (NextDatecalibrationResult.IsFailure)
                        return NextDatecalibrationResult.Error;

                    DateTime nextDatecalibration = NextDatecalibrationResult.Value.DateTimeValue;

                    Guid equipmentCalibrationId = Guid.NewGuid();
                    EquipmentCalibration equipmentCalibration = new(equipmentCalibrationId, equipment.Id, datecalibration, nextDatecalibration);
                    _equipmentCalibrationRepository.Save(equipmentCalibration);
                }
            }

            List<string>? fileUrls = [];
            if (request.Attachments != null)
            {
                foreach (Attachments.Application.Dtos.RegisterAttachmentRequest attachment in request.Attachments)
                {
                    Result<string, Notification> result = _attachmentApplicationService.RegisterAttachment(attachment, equipment.Id, EntityType.EQUIPMENT,userId);

                    if (result.IsFailure)
                        return result.Error;

                    fileUrls.Add(result.Value);
                }
            }

            _context.SaveChanges(userId);

            var response = new RegisterEquipmentResponse
            {
                Id = equipment.Id,
                Description = equipment.Description,
                Code = equipment.Code,
                SubsidiaryId = equipment.SubsidiaryId,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber,
                MedicalAreaId = equipment.MedicalAreaId,
                Supplier = equipment.Supplier,
                PersonDeviceManagerId = equipment.PersonDeviceManagerId,
                Status = equipment.Status,
            };

            return response;
        }

        public async Task<Result<EditEquipmentResponse, Notification>> EditEquipment(EditEquipmentRequest request, Equipment equipment, Guid userId)
        {
            Notification notification = _editEquipmentValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            equipment.Description = request.Description;
            equipment.Brand = request.Brand;
            equipment.Model = request.Model;
            equipment.SerialNumber = request.SerialNumber;
            equipment.MedicalAreaId = request.MedicalAreaId;
            equipment.Supplier = request.Supplier;
            equipment.PersonDeviceManagerId = request.PersonDeviceManagerId;
            equipment.SubsidiaryId = request.SubsidiaryId;

            if (request.EquipmentCalibrations != null)
            {
                foreach (var calibrations in request.EquipmentCalibrations)
                {
                    var DatecalibrationResult = Date.Create(calibrations.Datecalibration);
                    if (DatecalibrationResult.IsFailure)
                        return DatecalibrationResult.Error;

                    DateTime datecalibration = DatecalibrationResult.Value.DateTimeValue;

                    var NextDatecalibrationResult = Date.Create(calibrations.NextDatecalibration);
                    if (NextDatecalibrationResult.IsFailure)
                        return NextDatecalibrationResult.Error;

                    DateTime nextDatecalibration = NextDatecalibrationResult.Value.DateTimeValue;

                    if (calibrations.Id != null)
                    {
                        EquipmentCalibration? equipmentCalibration = _equipmentCalibrationRepository.GetById((Guid)calibrations.Id);
                        if (equipmentCalibration != null)
                        {
                            equipmentCalibration.NextDatecalibration = nextDatecalibration;
                            equipmentCalibration.Datecalibration = datecalibration;
                        }
                    }
                    else
                    {
                        Guid equipmentCalibrationId = Guid.NewGuid();
                        EquipmentCalibration equipmentCalibration = new(equipmentCalibrationId, equipment.Id, datecalibration, nextDatecalibration);
                        _equipmentCalibrationRepository.Save(equipmentCalibration);
                    }

                }
            }

            List<string>? fileUrls = [];
            if (request.Attachments != null)
            {        
                
                foreach (var attachment in request.Attachments)
                {
                    Result<string, Notification> result = _attachmentApplicationService.RegisterAttachment(attachment, equipment.Id, EntityType.EQUIPMENT,userId);

                    if (result.IsFailure)
                        return result.Error;

                    fileUrls.Add(result.Value);
                }
            }

            _context.SaveChanges(userId);

            var response = new EditEquipmentResponse
            {
                Id = equipment.Id,
                Description = equipment.Description,
                Code = equipment.Code,
                SubsidiaryId = equipment.SubsidiaryId,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber,
                MedicalAreaId = equipment.MedicalAreaId,
                Supplier = equipment.Supplier,
                PersonDeviceManagerId = equipment.PersonDeviceManagerId,
                Status = equipment.Status
            };

            return response;
        }

        public List<EquipmentDto> GetListByMedicalAreaTypeAndSubsidiaryId(Guid subsidiaryId, MedicalAreaType medicalAreaType)
        {
            return _equipmentRepository.GetListByMedicalAreaTypeAndSubsidiaryId(subsidiaryId, medicalAreaType);
        }

        public EditEquipmentResponse ActiveEquipment(Equipment equipment, Guid userId)
        {
            equipment.Status = true;

            _context.SaveChanges(userId);

            var response = new EditEquipmentResponse
            {
                Id = equipment.Id,
                Description = equipment.Description,
                Code = equipment.Code,
                SubsidiaryId = equipment.SubsidiaryId,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber,
                MedicalAreaId = equipment.MedicalAreaId,
                Supplier = equipment.Supplier,
                PersonDeviceManagerId = equipment.PersonDeviceManagerId,
                Status = equipment.Status
            };

            return response;
        }
        public EditEquipmentResponse RemoveEquipment(Equipment equipment, Guid userId)
        {
            equipment.Status = false;
            _context.SaveChanges(userId);

            var response = new EditEquipmentResponse
            {
                Id = equipment.Id,
                Description = equipment.Description,
                Code = equipment.Code,
                SubsidiaryId = equipment.SubsidiaryId,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber,
                MedicalAreaId = equipment.MedicalAreaId,
                Supplier = equipment.Supplier,
                PersonDeviceManagerId = equipment.PersonDeviceManagerId,
                Status = equipment.Status
            };

            return response;
        }
        public Equipment? GetById(Guid id)
        {
            return _equipmentRepository.GetById(id);
        }

        public EquipmentDto? GetDtoById(Guid id)
        {
            return _equipmentRepository.GetDtoById(id);
        }

        public List<EquipmentDto> GetListAll(Guid companyId)
        {
            return _equipmentRepository.GetListAll(companyId);
        }

        public Tuple<IEnumerable<EquipmentDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string? descriptionSearch, Guid companyId)
        {
            return _equipmentRepository.GetList(pageNumber, pageSize, status, descriptionSearch, companyId);
        }

        public Tuple<IEnumerable<EquipmentDto>, PaginationMetadata> GetListBySubsidiary(int pageNumber, int pageSize, bool status, Guid companyId, Guid SubsidiaryId)
        {
            return _equipmentRepository.GetListBySubsidiary(pageNumber, pageSize, status, companyId, SubsidiaryId);
        }
    }
}
