using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Services
{
    public class BusinessProfileApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterBusinessProfileValidator _registerBusinessProfileValidator;
        private readonly EditBusinessProfileValidator _editBusinessProfileValidator;
        private readonly BusinessProfileRepository _businessProfileRepository;
        
        private readonly RegisterListBusinessProfileValidator _registerListBusinessProfileValidator;
        

        public BusinessProfileApplicationService(
        AnaPreventionContext context,
       RegisterBusinessProfileValidator registerBusinessProfileValidator,
       EditBusinessProfileValidator editBusinessProfileValidator,
       BusinessProfileRepository businessProfileRepository,
       
       RegisterListBusinessProfileValidator registerListBusinessProfileValidator)
        {
            _context = context;
            _registerBusinessProfileValidator = registerBusinessProfileValidator;
            _editBusinessProfileValidator = editBusinessProfileValidator;
            _businessProfileRepository = businessProfileRepository;
            
            _registerListBusinessProfileValidator = registerListBusinessProfileValidator;
            
        }

        public Result<RegisterListBusinessProfileResponse, Notification> RegisterListBusinessProfile(RegisterListBusinessProfileRequest request, Guid userId)
        {

            List<string> ListDescription = new();
            request.ListDescription = request.ListDescription.Distinct().ToList();
            foreach (string Description in request.ListDescription)
            {

                Notification notification = _registerListBusinessProfileValidator.Validate(request);

                if (notification.HasErrors())
                    return notification;


                string description = Description.Trim();
                Guid businessId = request.BusinessId;


                BusinessProfile businessProfile = new(description, businessId, Guid.NewGuid());

                _businessProfileRepository.Save(businessProfile);
                ListDescription.Add(businessProfile.Description);
            }
            _context.SaveChanges(userId);
            var response = new RegisterListBusinessProfileResponse
            {
                ListDescription = ListDescription,
                BusinessId = request.BusinessId,
            };

            return response;
        }
        public Result<RegisterBusinessProfileResponse, Notification> RegisterBusinessProfile(RegisterBusinessProfileRequest request, Guid userId, bool isCommit = true)
        {
            Notification notification = _registerBusinessProfileValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            Guid businessId = request.BusinessId;


            BusinessProfile businessProfile = new(description, businessId, Guid.NewGuid());

            _businessProfileRepository.Save(businessProfile);

            if (isCommit)
            {
                _context.SaveChanges(userId);
            }


            var response = new RegisterBusinessProfileResponse
            {
                Id = businessProfile.Id,
                Description = businessProfile.Description,
                BusinessId = businessProfile.BusinessId,
                Status = businessProfile.Status,
            };

            return response;
        }

        public EditBusinessProfileResponse EditBusinessProfile(EditBusinessProfileRequest request, BusinessProfile businessProfile, Guid userId)
        {
            businessProfile.Description = request.Description.Trim();


            _context.SaveChanges(userId);

            var response = new EditBusinessProfileResponse
            {
                Id = businessProfile.Id,
                Description = businessProfile.Description,
                BusinessId = businessProfile.BusinessId,
                Status = businessProfile.Status
            };

            return response;
        }

        public EditBusinessProfileResponse ActiveBusinessProfile(BusinessProfile businessProfile, Guid userId)
        {
            businessProfile.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessProfileResponse
            {
                Id = businessProfile.Id,
                Description = businessProfile.Description,
                BusinessId = businessProfile.BusinessId,
                Status = businessProfile.Status
            };

            return response;
        }
        public Notification ValidateEditBusinessProfileRequest(EditBusinessProfileRequest request)
        {
            return _editBusinessProfileValidator.Validate(request);
        }

        public EditBusinessProfileResponse RemoveBusinessProfile(BusinessProfile businessProfile, Guid userId)
        {
            businessProfile.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessProfileResponse
            {
                Id = businessProfile.Id,
                Description = businessProfile.Description,
                BusinessId = businessProfile.BusinessId,
                Status = businessProfile.Status
            };

            return response;
        }

        public BusinessProfile? GetById(Guid id)
        {
            return _businessProfileRepository.GetById(id);
        }
       

        //public List<BusinessProfileProtocolDto> GetValidListAll(Guid businessId, DateTime? date = null, Guid? subsidiaryId = null)
        //{
        //    return _businessProfileRepository.GetValidListAllProtocol(businessId, date, subsidiaryId: subsidiaryId);
        //}
        public List<BusinessProfile> GetListAll(Guid businessId)
        {
            return _businessProfileRepository.GetListAll(businessId);
        }

        public Tuple<IEnumerable<BusinessProfile>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessId, bool status, string descriptionSearch = "")
        {
            return _businessProfileRepository.GetList(pageNumber, pageSize, businessId, status, descriptionSearch);
        }

       
    }
}
