using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Services
{
    public class IdentityDocumentTypeApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterIdentityDocumentTypeValidator _registerIdentityDocumentTypeValidator;
        private readonly EditIdentityDocumentTypeValidator _editIdentityDocumentTypeValidator;
        private readonly IdentityDocumentTypeRepository _identityDocumentTypeRepository;

        public IdentityDocumentTypeApplicationService(
       AnaPreventionContext context,
       RegisterIdentityDocumentTypeValidator registerIdentityDocumentTypeValidator,
       EditIdentityDocumentTypeValidator editIdentityDocumentTypeValidator,
       IdentityDocumentTypeRepository identityDocumentTypeRepository)
        {
            _context = context;
            _registerIdentityDocumentTypeValidator = registerIdentityDocumentTypeValidator;
            _editIdentityDocumentTypeValidator = editIdentityDocumentTypeValidator;
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
        }

        public Result<RegisterIdentityDocumentTypeResponse, Notification> RegisterIdentityDocumentType(RegisterIdentityDocumentTypeRequest request, Guid userId)
        {
            Notification notification = _registerIdentityDocumentTypeValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = request.Code.Trim();
            string abbreviation = request.Abbreviation.Trim();
            int length = request.Length;
            TaxpayerType taxpayerType = request.TaxpayerType;
            IndicatorLength indicatorLength = request.IndicatorLength;
            InputType inputType = request.InputType;
            PersonType personType = request.PersonType;


            IdentityDocumentType identityDocumentType = new(description, code, abbreviation, length, taxpayerType, indicatorLength, inputType, Guid.NewGuid(), personType);

            _identityDocumentTypeRepository.Save(identityDocumentType);

            _context.SaveChanges(userId);

            var response = new RegisterIdentityDocumentTypeResponse
            {
                Id = identityDocumentType.Id,
                Description = identityDocumentType.Description,
                Code = identityDocumentType.Code,
                Abbreviation = identityDocumentType.Abbreviation,
                Length = identityDocumentType.Length,
                TaxpayerType = identityDocumentType.TaxpayerType,
                IndicatorLength = identityDocumentType.IndicatorLength,
                InputType = identityDocumentType.InputType,
                Status = identityDocumentType.Status,
            };

            return response;
        }

        public EditIdentityDocumentTypeResponse EditIdentityDocumentType(EditIdentityDocumentTypeRequest request, IdentityDocumentType identityDocumentType, Guid userId)
        {
            identityDocumentType.Description = request.Description.Trim();
            identityDocumentType.Code = request.Code.Trim();
            identityDocumentType.Abbreviation = request.Abbreviation.Trim();
            identityDocumentType.Length = request.Length;
            identityDocumentType.TaxpayerType = request.TaxpayerType;
            identityDocumentType.InputType = request.InputType;
            identityDocumentType.IndicatorLength = request.IndicatorLength;
            identityDocumentType.Status = request.Status;
            identityDocumentType.PersonType = request.PersonType;

            _context.SaveChanges(userId);

            var response = new EditIdentityDocumentTypeResponse
            {
                Id = identityDocumentType.Id,
                Description = identityDocumentType.Description,
                Code = identityDocumentType.Code,
                Abbreviation = identityDocumentType.Abbreviation,
                Length = identityDocumentType.Length,
                TaxpayerType = identityDocumentType.TaxpayerType,
                IndicatorLength = identityDocumentType.IndicatorLength,
                InputType = identityDocumentType.InputType,
                Status = identityDocumentType.Status
            };

            return response;
        }

        public EditIdentityDocumentTypeResponse ActiveIdentityDocumentType(IdentityDocumentType identityDocumentType, Guid userId)
        {
            identityDocumentType.Status = true;

            _context.SaveChanges(userId);

            var response = new EditIdentityDocumentTypeResponse
            {
                Id = identityDocumentType.Id,
                Description = identityDocumentType.Description,
                Code = identityDocumentType.Code,
                Abbreviation = identityDocumentType.Abbreviation,
                Length = identityDocumentType.Length,
                TaxpayerType = identityDocumentType.TaxpayerType,
                IndicatorLength = identityDocumentType.IndicatorLength,
                InputType = identityDocumentType.InputType,
                Status = identityDocumentType.Status
            };

            return response;
        }
        public Notification ValidateEditIdentityDocumentTypeRequest(EditIdentityDocumentTypeRequest request)
        {
            return _editIdentityDocumentTypeValidator.Validate(request);
        }

        public EditIdentityDocumentTypeResponse RemoveIdentityDocumentType(IdentityDocumentType identityDocumentType, Guid userId)
        {
            identityDocumentType.Status = false;
            _context.SaveChanges(userId);

            var response = new EditIdentityDocumentTypeResponse
            {
                Id = identityDocumentType.Id,
                Description = identityDocumentType.Description,
                Status = identityDocumentType.Status
            };

            return response;
        }

        public IdentityDocumentType? GetById(Guid id)
        {
            return _identityDocumentTypeRepository.GetById(id);
        }

        public IdentityDocumentType? GetDtoById(Guid id)
        {
            return _identityDocumentTypeRepository.GetDtoById(id);
        }

        public Tuple<IEnumerable<IdentityDocumentType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            return _identityDocumentTypeRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch, abbreviationSearch);
        }

        public List<IdentityDocumentType> GetListAll()
        {
            return _identityDocumentTypeRepository.GetListAll();
        }

        public List<IdentityDocumentType> GetListOnlyPersonLegal()
        {
            return _identityDocumentTypeRepository.GetListOnlyPersonLegal();
        }

        public List<IdentityDocumentType> GetListOnlyPersonNatural()
        {
            return _identityDocumentTypeRepository.GetListOnlyPersonNatural();
        }

        public List<IdentityDocumentType> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            return _identityDocumentTypeRepository.GetListFilter(status, descriptionSearch, codeSearch, abbreviationSearch);
        }

    }
}
