using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Validators
{
    public class RegisterBusinessCampaignValidator : Validator
    {
        private readonly BusinessCampaignRepository _businessCampaignRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterBusinessCampaignValidator(
            BusinessCampaignRepository businessCampaignRepository,
            BusinessRepository businessRepositor)
        {
            _businessCampaignRepository = businessCampaignRepository;
            _businessRepository = businessRepositor;
        }

        public Notification Validate(RegisterBusinessCampaignRequest request)
        {
            Notification notification = new();


            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            if (notification.HasErrors())
            {
                return notification;
            }

            BusinessCampaign? businessCampaign = _businessCampaignRepository.GetbyDescription(request.Description);
            if (businessCampaign != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessCampaignStatic.BusinessIdMsgErrorNotFound);

            return notification;
        }
    }
}
