using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Validators
{
    public class EditBusinessCampaignValidator : Validator
    {
        private readonly BusinessCampaignRepository _businessCampaignRepository;

        public EditBusinessCampaignValidator(BusinessCampaignRepository businessCampaignRepository)
        {
            _businessCampaignRepository = businessCampaignRepository;
        }

        public Notification Validate(EditBusinessCampaignRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _businessCampaignRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);


            return notification;
        }
    }
}
