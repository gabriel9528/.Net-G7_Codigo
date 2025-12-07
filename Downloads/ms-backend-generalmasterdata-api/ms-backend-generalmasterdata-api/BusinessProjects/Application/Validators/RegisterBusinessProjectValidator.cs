using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Validators
{
    public class RegisterBusinessProjectValidator : Validator
    {
        private readonly BusinessProjectRepository _businessProjectRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterBusinessProjectValidator(
            BusinessProjectRepository businessProjectRepository,
            BusinessRepository businessRepositor)
        {
            _businessProjectRepository = businessProjectRepository;
            _businessRepository = businessRepositor;
        }

        public Notification Validate(RegisterBusinessProjectRequest request)
        {
            Notification notification = new();


            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessProjectStatic.BusinessIdMsgErrorNotFound);

            BusinessProject? businessProject = _businessProjectRepository.GetbyDescription(request.Description, request.BusinessId);
            if (businessProject != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);



            return notification;
        }
    }
}
