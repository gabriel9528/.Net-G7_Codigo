using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories;
using System.Net.Mail;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Validators
{
    public class EditSubsidiaryValidator : Validator
    {
        private readonly SubsidiaryRepository _subsidiaryRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly SubsidiaryTypeRepository _subsidiaryTypeRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly DistrictRepository _districtRepository;


        public EditSubsidiaryValidator(SubsidiaryRepository subsidiaryRepository,
            ServiceTypeRepository serviceTypeRepository,
            SubsidiaryTypeRepository subsidiaryTypeRepository,
            DoctorRepository doctorRepository,
            DistrictRepository districtRepository)
        {
            _subsidiaryRepository = subsidiaryRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _subsidiaryTypeRepository = subsidiaryTypeRepository;
            _doctorRepository = doctorRepository;
            _districtRepository = districtRepository;

        }

        public Notification Validate(EditSubsidiaryRequest request, Guid companyId)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            if (request.SubsidiaryTypeId == Guid.Empty)
                notification.AddError(SubsidiaryStatic.SubsidiaryTypeIdMsgErrorRequiered);

            if (request.ListServiceTypeId == null || request.ListServiceTypeId.Count <= 0)
                notification.AddError(SubsidiaryStatic.ServicesTypeIdMsgErrorRequiered);

            if (request.DoctorId == Guid.Empty)
                notification.AddError(SubsidiaryStatic.DoctorIdMsgErrorRequiered);


            if (request.Capacity <= 0)
                notification.AddError(SubsidiaryStatic.CapacityMsgErrorRequiered);

            if (companyId == Guid.Empty)
                notification.AddError(SubsidiaryStatic.CompanyIdMsgErrorRequiered);

            string districtId = string.IsNullOrWhiteSpace(request.DistrictId) ? "" : request.DistrictId.Trim();

            if (string.IsNullOrWhiteSpace(districtId))
                notification.AddError(SubsidiaryStatic.DistrictIdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            string address = string.IsNullOrWhiteSpace(request.Address) ? "" : request.Address.Trim();

            if (string.IsNullOrWhiteSpace(address))
                notification.AddError(SubsidiaryStatic.AddressMsgErrorRequiered);

            if (address.Length > SubsidiaryStatic.AddressMaxLength)
                notification.AddError(String.Format(SubsidiaryStatic.AddressMsgErrorMaxLength, SubsidiaryStatic.AddressMaxLength.ToString()));


            if (notification.HasErrors())
                return notification;

            if (request.OfficeHours == null)
            {
                notification.AddError(SubsidiaryStatic.OfficeHourMsgErrorRequiered);
                return notification;
            }

            foreach (RegisterOfficeHourRequest OfficeHourRequest in request.OfficeHours)
            {
                if (OfficeHourRequest.HourStart < TimeOnly.MinValue.Hour || OfficeHourRequest.HourStart > TimeOnly.MaxValue.Hour)
                {
                    notification.AddError(SubsidiaryStatic.HourMsgErrorFormat);
                    return notification;
                }
                if (OfficeHourRequest.HourFinish < TimeOnly.MinValue.Hour || OfficeHourRequest.HourFinish > TimeOnly.MaxValue.Hour)
                {
                    notification.AddError(SubsidiaryStatic.HourMsgErrorFormat);
                    return notification;
                }
                if (OfficeHourRequest.MinuteStart < TimeOnly.MinValue.Minute || OfficeHourRequest.MinuteStart > TimeOnly.MaxValue.Minute)
                {
                    notification.AddError(SubsidiaryStatic.HourMsgErrorFormat);
                    return notification;
                }
                if (OfficeHourRequest.MinuteFinish < TimeOnly.MinValue.Minute || OfficeHourRequest.MinuteFinish > TimeOnly.MaxValue.Minute)
                {
                    notification.AddError(SubsidiaryStatic.HourMsgErrorFormat);
                    return notification;
                }

                if (OfficeHourRequest.FinishDay < DayOfWeek.Sunday || OfficeHourRequest.FinishDay > DayOfWeek.Saturday)
                {
                    notification.AddError(SubsidiaryStatic.DaysMsgErrorFormat);
                    return notification;
                }

                if (OfficeHourRequest.StartDay < DayOfWeek.Sunday || OfficeHourRequest.StartDay > DayOfWeek.Saturday)
                {
                    notification.AddError(SubsidiaryStatic.DaysMsgErrorFormat);
                    return notification;
                }
            }

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _subsidiaryRepository.DescriptionTakenForEdit(request.Id, request.Description, companyId);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _subsidiaryRepository.CodeTakenForEdit(request.Id, request.Code, companyId);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            if (request.ListServiceTypeId != null)
            {
                foreach (Guid ServiceTypeId in request.ListServiceTypeId)
                {
                    if (_serviceTypeRepository.GetById(ServiceTypeId) == null)
                    {
                        notification.AddError(SubsidiaryStatic.ServiceTypeMsgErrorNotFound);
                        return notification;
                    }
                }
            }

            if (_subsidiaryTypeRepository.GetById(request.SubsidiaryTypeId) == null)
            {
                notification.AddError(SubsidiaryStatic.SubsidiaryTypeMsgErrorNotFound);
                return notification;
            }

            if (_doctorRepository.GetById(request.DoctorId) == null)
            {
                notification.AddError(SubsidiaryStatic.DoctorMsgErrorNotFound);
                return notification;
            }

            if (_districtRepository.GetDtoById(request.DistrictId) == null)
            {
                notification.AddError(SubsidiaryStatic.DistrictMsgErrorNotFound);
                return notification;
            }

            return notification;
        }

        public Notification ValidateEmail(string input, string inputType)
        {

            Notification notification = new();

            if (!string.IsNullOrEmpty(input))
            {
                var emails = input.Split(',')
                        .Select(email => email.Trim())
                        .ToList();

                foreach (var email in emails)
                {
                    // Validar si el componente es una dirección de correo electrónico válida
                    if (!IsValidEmail(email))
                    {
                        notification.AddError(string.Format(SubsidiaryStatic.EmailFormatMsgError, inputType));
                    }
                }
            }

            return notification;
        }

    }
}
