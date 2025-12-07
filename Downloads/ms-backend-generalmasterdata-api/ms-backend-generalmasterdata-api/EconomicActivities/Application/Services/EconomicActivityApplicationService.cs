using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Services
{
    public class EconomicActivityApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterEconomicActivityValidator _registerEconomicActivityValidator;
        private readonly EditEconomicActivityValidator _editEconomicActivityValidator;
        private readonly EconomicActivityRepository _economicActivityRepository;

        public EconomicActivityApplicationService(
       AnaPreventionContext context,
       RegisterEconomicActivityValidator registerEconomicActivityValidator,
       EditEconomicActivityValidator editEconomicActivityValidator,
       EconomicActivityRepository economicActivityRepository)
        {
            _context = context;
            _registerEconomicActivityValidator = registerEconomicActivityValidator;
            _editEconomicActivityValidator = editEconomicActivityValidator;
            _economicActivityRepository = economicActivityRepository;
        }

        public Result<RegisterEconomicActivityResponse, Notification> RegisterEconomicActivity(RegisterEconomicActivityRequest request,Guid userId)
        {
            Notification notification = _registerEconomicActivityValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();


            EconomicActivity economicActivity = new(description, code, Guid.NewGuid());

            _economicActivityRepository.Save(economicActivity);

            _context.SaveChanges(userId);

            var response = new RegisterEconomicActivityResponse
            {
                Id = economicActivity.Id,
                Description = economicActivity.Description,
                Code = economicActivity.Code,
                Status = economicActivity.Status,
            };

            return response;
        }

        public EditEconomicActivityResponse EditEconomicActivity(EditEconomicActivityRequest request, EconomicActivity economicActivity,Guid userId)
        {
            economicActivity.Description = request.Description.Trim();
            economicActivity.Code = request.Code.Trim();
            economicActivity.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditEconomicActivityResponse
            {
                Id = economicActivity.Id,
                Description = economicActivity.Description,
                Code = economicActivity.Code,
                Status = economicActivity.Status
            };

            return response;
        }

        public EditEconomicActivityResponse ActiveEconomicActivity(EconomicActivity economicActivity,Guid userId)
        {
            economicActivity.Status = true;

            _context.SaveChanges(userId);

            var response = new EditEconomicActivityResponse
            {
                Id = economicActivity.Id,
                Description = economicActivity.Description,
                Code = economicActivity.Code,
                Status = economicActivity.Status
            };

            return response;
        }
        public Notification ValidateEditEconomicActivityRequest(EditEconomicActivityRequest request)
        {
            return _editEconomicActivityValidator.Validate(request);
        }

        public string GenerateCode()
        {
            return _economicActivityRepository.GenerateCode();
        }

        public EditEconomicActivityResponse RemoveEconomicActivity(EconomicActivity economicActivity,Guid userId)
        {
            economicActivity.Status = false;
            _context.SaveChanges(userId);

            var response = new EditEconomicActivityResponse
            {
                Id = economicActivity.Id,
                Description = economicActivity.Description,
                Code = economicActivity.Code,
                Status = economicActivity.Status
            };

            return response;
        }

        public EconomicActivity? GetById(Guid id)
        {
            return _economicActivityRepository.GetById(id);
        }

        public List<EconomicActivity> GetListAll()
        {
            return _economicActivityRepository.GetListAll();
        }

       
        public Tuple<IEnumerable<EconomicActivity>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _economicActivityRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
