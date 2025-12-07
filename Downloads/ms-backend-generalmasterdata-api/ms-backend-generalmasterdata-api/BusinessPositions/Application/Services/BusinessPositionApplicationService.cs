using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Services
{
    public class BusinessPositionApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterBusinessPositionValidator _registerBusinessPositionValidator;
        private readonly EditBusinessPositionValidator _editBusinessPositionValidator;
        private readonly BusinessPositionRepository _businessPositionRepository;
       
        private readonly RegisterListBusinessPositionValidator _registerListBusinessPositionValidator;
        

        public BusinessPositionApplicationService(
       AnaPreventionContext context,
       RegisterBusinessPositionValidator registerBusinessPositionValidator,
       EditBusinessPositionValidator editBusinessPositionValidator,
       BusinessPositionRepository businessPositionRepository,
       
       RegisterListBusinessPositionValidator registerListBusinessPositionValidator)
        {
            _context = context;
            _registerBusinessPositionValidator = registerBusinessPositionValidator;
            _editBusinessPositionValidator = editBusinessPositionValidator;
            _businessPositionRepository = businessPositionRepository;
            
            _registerListBusinessPositionValidator = registerListBusinessPositionValidator;
            
        }

        public Result<RegisterListBusinessPositionResponse, Notification> RegisterListBusinessPosition(RegisterListBusinessPositionRequest request, Guid userId)
        {

            List<string> ListDescription = new();
            request.ListDescription = request.ListDescription.Distinct().ToList();
            foreach (string Description in request.ListDescription)
            {
                Notification notification = _registerListBusinessPositionValidator.Validate(request);

                if (notification.HasErrors())
                    return notification;


                string description = Description.Trim();
                Guid businessAreaId = request.BusinessAreaId;


                BusinessPosition businessPosition = new(description, businessAreaId, Guid.NewGuid());

                _businessPositionRepository.Save(businessPosition);
                ListDescription.Add(businessPosition.Description);
            }
            _context.SaveChanges(userId);
            var response = new RegisterListBusinessPositionResponse
            {
                ListDescription = ListDescription,
                BusinessAreaId = request.BusinessAreaId,
            };

            return response;
        }
        public Result<RegisterBusinessPositionResponse, Notification> RegisterBusinessPosition(RegisterBusinessPositionRequest request, Guid userId)
        {
            Notification notification = _registerBusinessPositionValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            Guid businessAreaId = request.BusinessAreaId;


            BusinessPosition businessPosition = new(description, businessAreaId, Guid.NewGuid());

            _businessPositionRepository.Save(businessPosition);

            _context.SaveChanges(userId);

            var response = new RegisterBusinessPositionResponse
            {
                Id = businessPosition.Id,
                Description = businessPosition.Description,
                BusinessAreaId = businessPosition.BusinessAreaId,
                Status = businessPosition.Status,
            };

            return response;
        }

        public EditBusinessPositionResponse EditBusinessPosition(EditBusinessPositionRequest request, BusinessPosition businessPosition, Guid userId)
        {
            businessPosition.Description = request.Description.Trim();


            _context.SaveChanges(userId);

            var response = new EditBusinessPositionResponse
            {
                Id = businessPosition.Id,
                Description = businessPosition.Description,
                BusinessAreaId = businessPosition.BusinessAreaId,
                Status = businessPosition.Status
            };

            return response;
        }

        public EditBusinessPositionResponse ActiveBusinessPosition(BusinessPosition businessPosition, Guid userId)
        {
            businessPosition.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessPositionResponse
            {
                Id = businessPosition.Id,
                Description = businessPosition.Description,
                BusinessAreaId = businessPosition.BusinessAreaId,
                Status = businessPosition.Status
            };

            return response;
        }
        public Notification ValidateEditBusinessPositionRequest(EditBusinessPositionRequest request)
        {
            return _editBusinessPositionValidator.Validate(request);
        }

        public EditBusinessPositionResponse RemoveBusinessPosition(BusinessPosition businessPosition, Guid userId)
        {
            businessPosition.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessPositionResponse
            {
                Id = businessPosition.Id,
                Description = businessPosition.Description,
                BusinessAreaId = businessPosition.BusinessAreaId,
                Status = businessPosition.Status
            };

            return response;
        }

        public BusinessPosition? GetById(Guid id)
        {
            return _businessPositionRepository.GetById(id);
        }
      
        public List<BusinessPosition> GetListAll(Guid businessAreaId)
        {
            return _businessPositionRepository.GetListAll(businessAreaId);
        }

        public Tuple<IEnumerable<BusinessPosition>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessAreaId, bool status, string descriptionSearch = "")
        {
            return _businessPositionRepository.GetList(pageNumber, pageSize, businessAreaId, status, descriptionSearch);
        }

       

    }
}
