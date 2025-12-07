using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Services
{
    public class BusinessAreaApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterBusinessAreaValidator _registerBusinessAreaValidator;
        private readonly EditBusinessAreaValidator _editBusinessAreaValidator;
        private readonly BusinessAreaRepository _businessAreaRepository;
       
        private readonly RegisterListBusinessAreaValidator _registerListBusinessAreaValidator;
        
        public BusinessAreaApplicationService(
       AnaPreventionContext context,
       RegisterBusinessAreaValidator registerBusinessAreaValidator,
       EditBusinessAreaValidator editBusinessAreaValidator,
       BusinessAreaRepository businessAreaRepository,
       
       RegisterListBusinessAreaValidator registerListBusinessAreaValidator)
        {
            _context = context;
            _registerBusinessAreaValidator = registerBusinessAreaValidator;
            _editBusinessAreaValidator = editBusinessAreaValidator;
            _businessAreaRepository = businessAreaRepository;
            
            _registerListBusinessAreaValidator = registerListBusinessAreaValidator;
           
        }

        public Result<RegisterListBusinessAreaResponse, Notification> RegisterListBusinessArea(RegisterListBusinessAreaRequest request, Guid userId)
        {

            List<string> ListDescription = new();
            request.ListDescription = request.ListDescription.Distinct().ToList();
            foreach (string Description in request.ListDescription)
            {
                Notification notification = _registerListBusinessAreaValidator.Validate(request);

                if (notification.HasErrors())
                    return notification;


                string description = Description.Trim();
                Guid businessId = request.BusinessId;


                BusinessArea businessArea = new(description, businessId, Guid.NewGuid());

                _businessAreaRepository.Save(businessArea);
                ListDescription.Add(businessArea.Description);
            }
            _context.SaveChanges(userId);
            var response = new RegisterListBusinessAreaResponse
            {
                ListDescription = ListDescription,
                BusinessId = request.BusinessId,
            };

            return response;
        }
        public Result<RegisterBusinessAreaResponse, Notification> RegisterBusinessArea(RegisterBusinessAreaRequest request, Guid userId)
        {
            Notification notification = _registerBusinessAreaValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            Guid businessId = request.BusinessId;


            BusinessArea businessArea = new(description, businessId, Guid.NewGuid());

            _businessAreaRepository.Save(businessArea);

            _context.SaveChanges(userId);

            var response = new RegisterBusinessAreaResponse
            {
                Id = businessArea.Id,
                Description = businessArea.Description,
                BusinessId = businessArea.BusinessId,
                Status = businessArea.Status,
            };

            return response;
        }


        public EditBusinessAreaResponse EditBusinessArea(EditBusinessAreaRequest request, BusinessArea businessArea, Guid userId)
        {
            businessArea.Description = request.Description.Trim();


            _context.SaveChanges(userId);

            var response = new EditBusinessAreaResponse
            {
                Id = businessArea.Id,
                Description = businessArea.Description,
                BusinessId = businessArea.BusinessId,
                Status = businessArea.Status
            };

            return response;
        }

        public EditBusinessAreaResponse ActiveBusinessArea(BusinessArea businessArea, Guid userId)
        {
            businessArea.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessAreaResponse
            {
                Id = businessArea.Id,
                Description = businessArea.Description,
                BusinessId = businessArea.BusinessId,
                Status = businessArea.Status
            };

            return response;
        }
        public Notification ValidateEditBusinessAreaRequest(EditBusinessAreaRequest request)
        {
            return _editBusinessAreaValidator.Validate(request);
        }

        public EditBusinessAreaResponse RemoveBusinessArea(BusinessArea businessArea, Guid userId)
        {
            businessArea.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessAreaResponse
            {
                Id = businessArea.Id,
                Description = businessArea.Description,
                BusinessId = businessArea.BusinessId,
                Status = businessArea.Status
            };

            return response;
        }

        public BusinessArea? GetById(Guid id)
        {
            return _businessAreaRepository.GetById(id);
        }

      
        public List<BusinessArea> GetListAll(Guid businessId)
        {
            return _businessAreaRepository.GetListAll(businessId);
        }

        public Tuple<IEnumerable<BusinessArea>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessId, bool status, string? descriptionSearch = "")
        {
            return _businessAreaRepository.GetList(pageNumber, pageSize, businessId, status, descriptionSearch);
        }

      

    }
}
