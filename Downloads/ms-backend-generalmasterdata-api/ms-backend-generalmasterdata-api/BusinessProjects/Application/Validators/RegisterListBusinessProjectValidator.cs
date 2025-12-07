using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Validators
{
    public class RegisterListBusinessProjectValidator : Validator
    {
        private readonly BusinessProjectRepository _businessProjectRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterListBusinessProjectValidator(
            BusinessProjectRepository businessProjectRepository,
            BusinessRepository businessRepositor)
        {
            _businessProjectRepository = businessProjectRepository;
            _businessRepository = businessRepositor;
        }
        public Notification Validate(RegisterListBusinessProjectRequest request)
        {
            Notification notification = new();


            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessProjectStatic.BusinessIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;
            foreach (string Description in request.ListDescription)
            {

                ValidatorString(notification, Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

                if (notification.HasErrors())
                    return notification;

                //BusinessProject? businessProject = _businessProjectRepository.GetbyDescription(Description);
                //if (businessProject != null)
                //    notification.AddError(String.Format(BusinessProjectStatic.ListDescriptionMsgErrorDuplicate, Description));
            }
            return notification;
        }
    }
}
