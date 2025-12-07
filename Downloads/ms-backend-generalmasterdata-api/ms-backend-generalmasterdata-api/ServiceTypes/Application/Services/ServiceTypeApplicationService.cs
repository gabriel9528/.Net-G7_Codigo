using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Services
{
    public class ServiceTypeApplicationService
    {

        private readonly AnaPreventionContext _context;
        private readonly RegisterServiceTypeValidator _registerServiceTypeValidator;
        private readonly EditServiceTypeValidator _editServiceTypeValidator;
        private readonly ServiceTypeRepository _serviceTypeRepository;

        public ServiceTypeApplicationService(
       AnaPreventionContext context,
       RegisterServiceTypeValidator registerServiceTypeValidator,
       EditServiceTypeValidator editServiceTypeValidator,
       ServiceTypeRepository serviceTypeRepository)
        {
            _context = context;
            _registerServiceTypeValidator = registerServiceTypeValidator;
            _editServiceTypeValidator = editServiceTypeValidator;
            _serviceTypeRepository = serviceTypeRepository;
        }

        public Result<RegisterServiceTypeResponse, Notification> RegisterServiceType(RegisterServiceTypeRequest request, Guid companyId, Guid userId)
        {
            Notification notification = _registerServiceTypeValidator.Validate(request, companyId);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            ServiceTypeEnum code = request.Code;

            ServiceType serviceType = new(description, code, companyId, Guid.NewGuid());

            _serviceTypeRepository.Save(serviceType);

            _context.SaveChanges(userId);

            var response = new RegisterServiceTypeResponse
            {
                Id = serviceType.Id,
                Code = serviceType.Code,
                Description = serviceType.Description,
                Status = serviceType.Status,
                CompanyId = serviceType.CompanyId,
            };

            return response;
        }
        public EditServiceTypeResponse EditServiceType(EditServiceTypeRequest request, ServiceType serviceType, Guid userId)
        {
            serviceType.Description = request.Description.Trim();
            serviceType.Code = request.Code;
            serviceType.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditServiceTypeResponse
            {
                Id = serviceType.Id,
                Code = serviceType.Code,
                Description = serviceType.Description,
                Status = serviceType.Status,
                CompanyId = serviceType.CompanyId,
            };

            return response;
        }

        public EditServiceTypeResponse ActiveServiceType(ServiceType serviceType, Guid userId)
        {
            serviceType.Status = true;

            _context.SaveChanges(userId);

            var response = new EditServiceTypeResponse
            {
                Id = serviceType.Id,
                Description = serviceType.Description,
                CompanyId = serviceType.CompanyId,
                Status = serviceType.Status
            };

            return response;
        }
        public Notification ValidateEditServiceTypeRequest(EditServiceTypeRequest request, Guid companyId)
        {
            return _editServiceTypeValidator.Validate(request, companyId);
        }
        public EditServiceTypeResponse RemoveServiceType(ServiceType serviceType, Guid userId)
        {
            serviceType.Status = false;
            _context.SaveChanges(userId);

            var response = new EditServiceTypeResponse
            {
                Id = serviceType.Id,
                Description = serviceType.Description,
                Status = serviceType.Status,
                CompanyId = serviceType.CompanyId,
            };

            return response;
        }
        public ServiceType? GetById(Guid id)
        {
            return _serviceTypeRepository.GetById(id);
        }

        public ServiceType? GetDtoById(Guid id, Guid companyId)
        {
            return _serviceTypeRepository.GetByIdAndCompanyId(id, companyId);
        }
        public async Task<List<ServiceType>> GetListAll(Guid companyId)
        {
            return await _serviceTypeRepository.GetListAll(companyId);
        }
        public Tuple<IEnumerable<ServiceType>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _serviceTypeRepository.GetList(pageNumber, pageSize, companyId, status, descriptionSearch, codeSearch);
        }
    }
}
