using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Validators
{
    public class RegisterBusinessValidator: Validator
    {
        private readonly BusinessRepository _businessRepository;
        private readonly IdentityDocumentTypeRepository _identityDocumentTypeRepository;
        private readonly MedicalFormatRepository _medicalFormatRepository;
        private readonly CreditTimeRepository _creditTimeRepository;
        private readonly DistrictRepository _districtRepository;
        private readonly EconomicActivityRepository _economicActivityRepository;

        public RegisterBusinessValidator(
            BusinessRepository businessRepository,
            IdentityDocumentTypeRepository identityDocumentTypeRepository,
            MedicalFormatRepository medicalFormatRepository,
            CreditTimeRepository creditTimeRepository,
            DistrictRepository districtRepository,
            EconomicActivityRepository economicActivityRepository
            )
        {
            _businessRepository = businessRepository;
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
            _medicalFormatRepository = medicalFormatRepository;
            _creditTimeRepository = creditTimeRepository;
            _districtRepository = districtRepository;
            _economicActivityRepository = economicActivityRepository;
        }

        public Notification Validate(RegisterBusinessRequest request)
        {
            Notification notification = new();

            if (request.IdentityDocumentTypeId == Guid.Empty)
                notification.AddError(BusinessStatic.IdentityDocumentTypeIdMsgErrorRequiered);

            if (request.MedicalFormatId == Guid.Empty)
                notification.AddError(BusinessStatic.MedicalFormatIdMsgErrorRequiered);

            if (request.CreditTimeId == Guid.Empty)
                notification.AddError(BusinessStatic.CreditTimeIdMsgErrorRequiered);

            if (string.IsNullOrWhiteSpace(request.DistrictId))
                notification.AddError(BusinessStatic.DistrictIdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            string trandname = string.IsNullOrWhiteSpace(request.Tradename) ? "" : request.Tradename.Trim();

            if (trandname.Length > BusinessStatic.TradenameMaxLength)
                notification.AddError(String.Format(BusinessStatic.TradenameMsgErrorMaxLength, BusinessStatic.TradenameMaxLength.ToString()));

            string documentNumber = string.IsNullOrWhiteSpace(request.DocumentNumber) ? "" : request.DocumentNumber.Trim();

            if (string.IsNullOrWhiteSpace(documentNumber))
                notification.AddError(BusinessStatic.DocumentNumberMsgErrorRequiered);

            if (documentNumber.Length > BusinessStatic.DocumentNumberMaxLength)
                notification.AddError(String.Format(BusinessStatic.DocumentNumberMsgErrorMaxLength, BusinessStatic.DocumentNumberMaxLength.ToString()));

            string address = string.IsNullOrWhiteSpace(request.Address) ? "" : request.Address.Trim();

            if (address.Length > BusinessStatic.AddressMaxLength)
                notification.AddError(String.Format(BusinessStatic.AddressMsgErrorMaxLength, BusinessStatic.AddressMaxLength.ToString()));

            if (notification.HasErrors())
                return notification;

            Business? business = _businessRepository.GetbyDescription(request.Description);
            if (business != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            business = _businessRepository.GetbyDocumentNumber(request.DocumentNumber, request.IdentityDocumentTypeId);
            if (business != null)
                notification.AddError(BusinessStatic.DocumentNumberMsgErrorDuplicate);

            IdentityDocumentType? identityDocumentType = _identityDocumentTypeRepository.GetById(request.IdentityDocumentTypeId);
            if (identityDocumentType == null)
                notification.AddError(BusinessStatic.IdentityDocumentTypeIdMsgErrorNoFound);

            MedicalFormat? medicalFormat = _medicalFormatRepository.GetById(request.MedicalFormatId);
            if (medicalFormat == null)
                notification.AddError(BusinessStatic.MedicalFormatIdMsgErrorNoFound);

            CreditTime? creditTime = _creditTimeRepository.GetById(request.CreditTimeId);
            if (creditTime == null)
                notification.AddError(BusinessStatic.CreditTimeIdMsgErrorNoFound);

            DistrictDto? district = _districtRepository.GetDtoById(request.DistrictId);
            if (district == null)
                notification.AddError(BusinessStatic.DistrictIdMsgErrorNoFound);

            if (request.ListEconomicActivityId == null || request.ListEconomicActivityId.Count == 0)
                notification.AddError(BusinessStatic.EconomicActivityIdMsgErrorRequiered);
            else
                foreach (var EconomicActivityId in request.ListEconomicActivityId)
                {
                    EconomicActivity? economicActivity = _economicActivityRepository.GetById(EconomicActivityId);
                    if (economicActivity == null)
                        notification.AddError(BusinessStatic.EconomicActivityIdMsgErrorNoFound);
                }

            return notification;
        }
    }
}

//private readonly EconomicActivityRepository _economicActivityRepository;