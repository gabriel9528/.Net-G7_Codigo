using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Services
{
    public class BusinessCostCenterApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterBusinessCostCenterValidator _registerBusinessCostCenterValidator;
        private readonly EditBusinessCostCenterValidator _editBusinessCostCenterValidator;
        private readonly BusinessCostCenterRepository _businessCostCenterRepository;
        
        private readonly RegisterListBusinessCostCenterValidator _registerListBusinessCostCenterValidator;
        

        public BusinessCostCenterApplicationService(
       AnaPreventionContext context,
       RegisterBusinessCostCenterValidator registerBusinessCostCenterValidator,
       EditBusinessCostCenterValidator editBusinessCostCenterValidator,
       BusinessCostCenterRepository businessCostCenterRepository,
       
       RegisterListBusinessCostCenterValidator registerListBusinessCostCenterValidator)
        {
            _context = context;
            _registerBusinessCostCenterValidator = registerBusinessCostCenterValidator;
            _editBusinessCostCenterValidator = editBusinessCostCenterValidator;
            _businessCostCenterRepository = businessCostCenterRepository;
            
            _registerListBusinessCostCenterValidator = registerListBusinessCostCenterValidator;
            
        }
        public Result<RegisterListBusinessCostCenterResponse, Notification> RegisterListBusinessCostCenter(RegisterListBusinessCostCenterRequest request, Guid userId)
        {

            List<string> ListDescription = new();
            request.ListDescription = request.ListDescription.Distinct().ToList();
            foreach (string Description in request.ListDescription)
            {

                Notification notification = _registerListBusinessCostCenterValidator.Validate(request);

                if (notification.HasErrors())
                    return notification;


                string description = Description.Trim();
                Guid businessId = request.BusinessId;


                BusinessCostCenter businessCostCenter = new(description, businessId, Guid.NewGuid());

                _businessCostCenterRepository.Save(businessCostCenter);
                ListDescription.Add(businessCostCenter.Description);
            }
            _context.SaveChanges(userId);
            var response = new RegisterListBusinessCostCenterResponse
            {
                ListDescription = ListDescription,
                BusinessId = request.BusinessId,
            };

            return response;
        }
        public Result<RegisterBusinessCostCenterResponse, Notification> RegisterBusinessCostCenter(RegisterBusinessCostCenterRequest request, Guid userId)
        {
            Notification notification = _registerBusinessCostCenterValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            Guid businessId = request.BusinessId;


            BusinessCostCenter businessCostCenter = new(description, businessId, Guid.NewGuid());

            _businessCostCenterRepository.Save(businessCostCenter);

            _context.SaveChanges(userId);

            var response = new RegisterBusinessCostCenterResponse
            {
                Id = businessCostCenter.Id,
                Description = businessCostCenter.Description,
                BusinessId = businessCostCenter.BusinessId,
                Status = businessCostCenter.Status,
            };

            return response;
        }

        public EditBusinessCostCenterResponse EditBusinessCostCenter(EditBusinessCostCenterRequest request, BusinessCostCenter businessCostCenter, Guid userId)
        {
            businessCostCenter.Description = request.Description.Trim();


            _context.SaveChanges(userId);

            var response = new EditBusinessCostCenterResponse
            {
                Id = businessCostCenter.Id,
                Description = businessCostCenter.Description,
                BusinessId = businessCostCenter.BusinessId,
                Status = businessCostCenter.Status
            };

            return response;
        }

        public EditBusinessCostCenterResponse ActiveBusinessCostCenter(BusinessCostCenter businessCostCenter, Guid userId)
        {
            businessCostCenter.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessCostCenterResponse
            {
                Id = businessCostCenter.Id,
                Description = businessCostCenter.Description,
                BusinessId = businessCostCenter.BusinessId,
                Status = businessCostCenter.Status
            };

            return response;
        }
        public Notification ValidateEditBusinessCostCenterRequest(EditBusinessCostCenterRequest request)
        {
            return _editBusinessCostCenterValidator.Validate(request);
        }

        public EditBusinessCostCenterResponse RemoveBusinessCostCenter(BusinessCostCenter businessCostCenter, Guid userId)
        {
            businessCostCenter.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessCostCenterResponse
            {
                Id = businessCostCenter.Id,
                Description = businessCostCenter.Description,
                BusinessId = businessCostCenter.BusinessId,
                Status = businessCostCenter.Status
            };

            return response;
        }

        public BusinessCostCenter? GetById(Guid id)
        {
            return _businessCostCenterRepository.GetById(id);
        }

        public BusinessCostCenter? GetDtoById(Guid id)
        {
            return _businessCostCenterRepository.GetDtoById(id);
        }

        public List<BusinessCostCenter> GetListAll(Guid businessId)
        {
            return _businessCostCenterRepository.GetListAll(businessId);
        }
        
        public Tuple<IEnumerable<BusinessCostCenter>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessId, bool status, string descriptionSearch = "")
        {
            return _businessCostCenterRepository.GetList(pageNumber, pageSize, businessId, status, descriptionSearch);
        }

       
    }
}
