using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Services
{
    public class SubsidiaryTypeApplicationService(
   AnaPreventionContext context,
   RegisterSubsidiaryTypeValidator registerSubsidiaryTypeValidator,
   EditSubsidiaryTypeValidator editSubsidiaryTypeValidator,
   SubsidiaryTypeRepository subsidiaryTypeRepository)
    {


        private readonly AnaPreventionContext _context = context;
        private readonly RegisterSubsidiaryTypeValidator _registerSubsidiaryTypeValidator = registerSubsidiaryTypeValidator;
        private readonly EditSubsidiaryTypeValidator _editSubsidiaryTypeValidator = editSubsidiaryTypeValidator;
        private readonly SubsidiaryTypeRepository _subsidiaryTypeRepository = subsidiaryTypeRepository;

        public Result<RegisterSubsidiaryTypeResponse, Notification> RegisterSubsidiaryType(RegisterSubsidiaryTypeRequest request,Guid userId)
        {
            Notification notification = _registerSubsidiaryTypeValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();

            SubsidiaryType subsidiaryType = new(description,code,Guid.NewGuid());

            _subsidiaryTypeRepository.Save(subsidiaryType);

            _context.SaveChanges(userId);

            var response = new RegisterSubsidiaryTypeResponse
            {
                Id = subsidiaryType.Id,
                Code = subsidiaryType.Code,
                Description = subsidiaryType.Description,
                Status = subsidiaryType.Status
            };

            return response;
        }
        public string GenerateCode()
        {
            return _subsidiaryTypeRepository.GenerateCode();
        }
        public EditSubsidiaryTypeResponse EditSubsidiaryType(EditSubsidiaryTypeRequest request, SubsidiaryType subsidiaryType,Guid userId)
        {
            subsidiaryType.Description = request.Description.Trim();
            subsidiaryType.Code = request.Code.Trim();
            subsidiaryType.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditSubsidiaryTypeResponse
            {
                Id = subsidiaryType.Id,
                Code=subsidiaryType.Code,
                Description = subsidiaryType.Description,
                Status = subsidiaryType.Status,
            };

            return response;
        }

        public EditSubsidiaryTypeResponse ActiveSubsidiaryType(SubsidiaryType subsidiaryType,Guid userId)
        {
            subsidiaryType.Status = true;

            _context.SaveChanges(userId);

            var response = new EditSubsidiaryTypeResponse
            {
                Id = subsidiaryType.Id,
                Description = subsidiaryType.Description,
                Status = subsidiaryType.Status
            };

            return response;
        }
        public Notification ValidateEditSubsidiaryTypeRequest(EditSubsidiaryTypeRequest request)
        {
            return _editSubsidiaryTypeValidator.Validate(request);
        }
        public EditSubsidiaryTypeResponse RemoveSubsidiaryType(SubsidiaryType subsidiaryType,Guid userId)
        {
            subsidiaryType.Status = false;
            _context.SaveChanges(userId);

            var response = new EditSubsidiaryTypeResponse
            {
                Id = subsidiaryType.Id,
                Description = subsidiaryType.Description,
                Status = subsidiaryType.Status,
            };

            return response;
        }
        public SubsidiaryType? GetById(Guid id)
        {
            return _subsidiaryTypeRepository.GetById(id);
        }

        public SubsidiaryType? GetDtoById(Guid id,Guid companyId)
        {
            return _subsidiaryTypeRepository.GetDtoById(id);
        }
        public List<SubsidiaryType> GetListAll(Guid companyId)
        {
            return _subsidiaryTypeRepository.GetListAll();
        }
        public Tuple<IEnumerable<SubsidiaryType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch="")
        {
            return _subsidiaryTypeRepository.GetList(pageNumber, pageSize,  status , descriptionSearch, codeSearch);
        }
    }
}
