using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Services
{
    public class CommercialDocumentTypeApplicationService
    {

        private readonly AnaPreventionContext _context;
        private readonly RegisterCommercialDocumentTypeValidator _registerCommercialDocumentTypeValidator;
        private readonly EditCommercialDocumentTypeValidator _editCommercialDocumentTypeValidator;
        private readonly CommercialDocumentTypeRepository _commercialDocumentTypeRepository;


        public CommercialDocumentTypeApplicationService(
       AnaPreventionContext context,
       RegisterCommercialDocumentTypeValidator registerCommercialDocumentTypeValidator,
       EditCommercialDocumentTypeValidator editCommercialDocumentTypeValidator,
       CommercialDocumentTypeRepository commercialDocumentTypeRepository)
        {
            _context = context;
            _registerCommercialDocumentTypeValidator = registerCommercialDocumentTypeValidator;
            _editCommercialDocumentTypeValidator = editCommercialDocumentTypeValidator;
            _commercialDocumentTypeRepository = commercialDocumentTypeRepository;
        }

        public Result<RegisterCommercialDocumentTypeResponse, Notification> RegisterCommercialDocumentType(RegisterCommercialDocumentTypeRequest request,Guid userId)
        {
            Notification notification = _registerCommercialDocumentTypeValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = request.Code.Trim();
            string abbreviation = request.Abbreviation.Trim();
            bool purchaseDocument = request.PurchaseDocument;
            bool salesDocument = request.SalesDocument;
            bool getSetDocument = request.GetSetDocument;


            CommercialDocumentType commercialDocumentType = new(description, code, abbreviation, purchaseDocument, salesDocument, getSetDocument,Guid.NewGuid());

            _commercialDocumentTypeRepository.Save(commercialDocumentType);

            _context.SaveChanges(userId);

            var response = new RegisterCommercialDocumentTypeResponse
            {
                Id = commercialDocumentType.Id,
                Description = commercialDocumentType.Description,
                Code = commercialDocumentType.Code,
                Abbreviation = commercialDocumentType.Abbreviation,
                PurchaseDocument = commercialDocumentType.PurchaseDocument,
                SalesDocument = commercialDocumentType.SalesDocument,
                GetSetDocument = commercialDocumentType.GetSetDocument,
                Status = commercialDocumentType.Status,
            };

            return response;
        }
        public EditCommercialDocumentTypeResponse EditCommercialDocumentType(EditCommercialDocumentTypeRequest request, CommercialDocumentType commercialDocumentType, Guid userId)
        {
            commercialDocumentType.Description = request.Description.Trim();
            commercialDocumentType.Code = request.Code.Trim();
            commercialDocumentType.Abbreviation = request.Abbreviation.Trim();
            commercialDocumentType.PurchaseDocument = request.PurchaseDocument;
            commercialDocumentType.SalesDocument = request.SalesDocument;
            commercialDocumentType.GetSetDocument = request.GetSetDocument;
            commercialDocumentType.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditCommercialDocumentTypeResponse
            {
                Id = commercialDocumentType.Id,
                Description = commercialDocumentType.Description,
                Code = commercialDocumentType.Code,
                Abbreviation = commercialDocumentType.Abbreviation,
                PurchaseDocument = commercialDocumentType.PurchaseDocument,
                SalesDocument = commercialDocumentType.SalesDocument,
                GetSetDocument = commercialDocumentType.GetSetDocument,
                Status = commercialDocumentType.Status
            };

            return response;
        }

        public EditCommercialDocumentTypeResponse ActiveCommercialDocumentType(CommercialDocumentType commercialDocumentType, Guid userId)
        {
            commercialDocumentType.Status = true;

            _context.SaveChanges(userId);

            var response = new EditCommercialDocumentTypeResponse
            {
                Id = commercialDocumentType.Id,
                Description = commercialDocumentType.Description,
                Abbreviation = commercialDocumentType.Abbreviation,
                Status = commercialDocumentType.Status
            };

            return response;
        }

        public Notification ValidateEditCommercialDocumentTypeRequest(EditCommercialDocumentTypeRequest request)
        {
            return _editCommercialDocumentTypeValidator.Validate(request);
        }

        public EditCommercialDocumentTypeResponse RemoveCommercialDocumentTypel(CommercialDocumentType commercialDocumentType, Guid userId)
        {
            commercialDocumentType.Status = false;
            _context.SaveChanges(userId);

            var response = new EditCommercialDocumentTypeResponse
            {
                Id = commercialDocumentType.Id,
                Description = commercialDocumentType.Description,
                Abbreviation = commercialDocumentType.Abbreviation,
                PurchaseDocument = commercialDocumentType.PurchaseDocument,
                SalesDocument = commercialDocumentType.SalesDocument,
                GetSetDocument = commercialDocumentType.GetSetDocument,
                Status = commercialDocumentType.Status
            };

            return response;
        }
        public CommercialDocumentType? GetById(Guid id)
        {
            return _commercialDocumentTypeRepository.GetById(id);
        }

        public CommercialDocumentType? GetDtoById(Guid id)
        {
            return _commercialDocumentTypeRepository.GetDtoById(id);
        }
        public List<CommercialDocumentType> GetListAll()
        {
            return _commercialDocumentTypeRepository.GetListAll();
        }

        public Tuple<IEnumerable<CommercialDocumentType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            return _commercialDocumentTypeRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch,abbreviationSearch);
        }
    }
}
