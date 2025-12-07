using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Services
{
    public class BusinessCampaignApplicationService(
   AnaPreventionContext context,
   RegisterBusinessCampaignValidator registerBusinessCampaignValidator,
   EditBusinessCampaignValidator editBusinessCampaignValidator,
   BusinessCampaignRepository businessCampaignRepository)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly RegisterBusinessCampaignValidator _registerBusinessCampaignValidator = registerBusinessCampaignValidator;
        private readonly EditBusinessCampaignValidator _editBusinessCampaignValidator = editBusinessCampaignValidator;
        private readonly BusinessCampaignRepository _businessCampaignRepository = businessCampaignRepository;

        public Result<RegisterBusinessCampaignResponse, Notification> RegisterBusinessCampaign(RegisterBusinessCampaignRequest request, Guid userId)
        {
            Notification notification = _registerBusinessCampaignValidator.Validate(request);
            if (notification.HasErrors())
                return notification;

            Result<Date, Notification> resultDateStart = Date.Create(request.DateStart, BusinessCampaignStatic.DateStart);
            Result<Date, Notification> resultDateFinish = Date.Create(request.DateFinish, BusinessCampaignStatic.DateFinish);

            if (resultDateStart.IsFailure)
                return resultDateStart.Error;

            if (resultDateFinish.IsFailure)
                return resultDateFinish.Error;


            string description = request.Description.Trim();
            Guid businessId = request.BusinessId;
            Date dateStart = resultDateStart.Value;
            Date dateFinish = resultDateFinish.Value;


            BusinessCampaign businessCampaign = new(description, businessId, dateStart, dateFinish, Guid.NewGuid());

            _businessCampaignRepository.Save(businessCampaign);

            _context.SaveChanges(userId);

            var response = new RegisterBusinessCampaignResponse
            {
                Id = businessCampaign.Id,
                Description = businessCampaign.Description,
                BusinessId = businessCampaign.BusinessId,
                Status = businessCampaign.Status,
                DateStart = resultDateStart.Value.StringValue,
                DateFinish = resultDateFinish.Value.StringValue,
            };

            return response;
        }

        public EditBusinessCampaignResponse EditBusinessCampaign(EditBusinessCampaignRequest request, BusinessCampaign businessCampaign, Guid userId)
        {
            businessCampaign.Description = request.Description.Trim();

            businessCampaign.DateStart = Date.Create(request.DateStart).Value;
            businessCampaign.DateFinish = Date.Create(request.DateFinish).Value;

            _context.SaveChanges(userId);

            var response = new EditBusinessCampaignResponse
            {
                Id = businessCampaign.Id,
                Description = businessCampaign.Description,
                BusinessId = businessCampaign.BusinessId,
                DateStart = businessCampaign.DateStart.StringValue,
                DateFinish = businessCampaign.DateFinish.StringValue,
                Status = businessCampaign.Status
            };

            return response;
        }

        public EditBusinessCampaignResponse ActiveBusinessCampaign(BusinessCampaign businessCampaign, Guid userId)
        {
            businessCampaign.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessCampaignResponse
            {
                Id = businessCampaign.Id,
                Description = businessCampaign.Description,
                BusinessId = businessCampaign.BusinessId,
                DateStart = businessCampaign.DateStart.StringValue,
                DateFinish = businessCampaign.DateFinish.StringValue,
                Status = businessCampaign.Status
            };

            return response;
        }
        public Notification ValidateEditBusinessCampaignRequest(EditBusinessCampaignRequest request)
        {
            return _editBusinessCampaignValidator.Validate(request);
        }

        public EditBusinessCampaignResponse RemoveBusinessCampaign(BusinessCampaign businessCampaign, Guid userId)
        {
            businessCampaign.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessCampaignResponse
            {
                Id = businessCampaign.Id,
                Description = businessCampaign.Description,
                BusinessId = businessCampaign.BusinessId,
                DateStart = businessCampaign.DateStart.StringValue,
                DateFinish = businessCampaign.DateFinish.StringValue,
                Status = businessCampaign.Status
            };

            return response;
        }

        public BusinessCampaign? GetById(Guid id)
        {
            return _businessCampaignRepository.GetById(id);
        }

        public BusinessCampaignDto? GetDtoById(Guid id)
        {
            return _businessCampaignRepository.GetDtoById(id);
        }
       
        public List<BusinessCampaignDto> GetListAll(Guid businessId)
        {
            return _businessCampaignRepository.GetListAll(businessId);
        }

        public Tuple<IEnumerable<BusinessCampaignDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessId, bool status, string descriptionSearch = "")
        {
            return _businessCampaignRepository.GetList(pageNumber, pageSize, businessId, status, descriptionSearch);
        }

     

    }
}
