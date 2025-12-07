using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Services
{
    public class CreditTimeApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterCreditTimeValidator _registerCreditTimeValidator;
        private readonly EditCreditTimeValidator _editCreditTimeValidator;
        private readonly CreditTimeRepository _creditTimeRepository;

        public CreditTimeApplicationService(
       AnaPreventionContext context,
       RegisterCreditTimeValidator registerCreditTimeValidator,
       EditCreditTimeValidator editCreditTimeValidator,
       CreditTimeRepository creditTimeRepository)
        {
            _context = context;
            _registerCreditTimeValidator = registerCreditTimeValidator;
            _editCreditTimeValidator = editCreditTimeValidator;
            _creditTimeRepository = creditTimeRepository;
        }

        public Result<RegisterCreditTimeResponse, Notification> RegisterCreditTime(RegisterCreditTimeRequest request,Guid userId)
        {
            Notification notification = _registerCreditTimeValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = request.Code.Trim();
            int numberDay = request.NumberDay;


            CreditTime creditTime = new(description, code, numberDay,Guid.NewGuid());

            _creditTimeRepository.Save(creditTime);

            _context.SaveChanges(userId);

            var response = new RegisterCreditTimeResponse
            {
                Id = creditTime.Id,
                Description = creditTime.Description,
                Code = creditTime.Code,
                NumberDay = creditTime.NumberDay,
                Status = creditTime.Status,
            };

            return response;
        }

        public EditCreditTimeResponse EditCreditTime(EditCreditTimeRequest request, CreditTime creditTime,Guid userId)
        {
            creditTime.Description = request.Description.Trim();
            creditTime.Code = request.Code.Trim();
            creditTime.NumberDay = request.NumberDay;


            _context.SaveChanges(userId);

            var response = new EditCreditTimeResponse
            {
                Id = creditTime.Id,
                Description = creditTime.Description,
                Code = creditTime.Code,
                NumberDay = creditTime.NumberDay,
                Status = creditTime.Status
            };

            return response;
        }

        public EditCreditTimeResponse ActiveCreditTime(CreditTime creditTime,Guid userId)
        {
            creditTime.Status = true;

            _context.SaveChanges(userId);

            var response = new EditCreditTimeResponse
            {
                Id = creditTime.Id,
                Description = creditTime.Description,
                Code = creditTime.Code,
                NumberDay = creditTime.NumberDay,
                Status = creditTime.Status
            };

            return response;
        }
        public Notification ValidateEditCreditTimeRequest(EditCreditTimeRequest request)
        {
            return _editCreditTimeValidator.Validate(request);
        }

        public EditCreditTimeResponse RemoveCreditTime(CreditTime creditTime,Guid userId)
        {
            creditTime.Status = false;
            _context.SaveChanges(userId);

            var response = new EditCreditTimeResponse
            {
                Id = creditTime.Id,
                Description = creditTime.Description,
                Code = creditTime.Code,
                NumberDay = creditTime.NumberDay,
                Status = creditTime.Status
            };

            return response;
        }

        public CreditTime? GetById(Guid id)
        {
            return _creditTimeRepository.GetById(id);
        }

        public CreditTime? GetDtoById(Guid id)
        {
            return _creditTimeRepository.GetDtoById(id);
        }

        public List<CreditTime> GetListAll()
        {
            return _creditTimeRepository.GetListAll();
        }
       
        public Tuple<IEnumerable<CreditTime>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _creditTimeRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
