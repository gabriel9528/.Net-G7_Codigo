using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Services
{
    public class SubFamilyApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterSubFamilyValidator _registerSubFamilyValidator;
        private readonly EditSubFamilyValidator _editSubFamilyValidator;
        private readonly SubFamilyRepository _subFamilyRepository;

        public SubFamilyApplicationService(
       AnaPreventionContext context,
       RegisterSubFamilyValidator registerSubFamilyValidator,
       EditSubFamilyValidator editSubFamilyValidator,
       SubFamilyRepository subFamilyRepository)
        {
            _context = context;
            _registerSubFamilyValidator = registerSubFamilyValidator;
            _editSubFamilyValidator = editSubFamilyValidator;
            _subFamilyRepository = subFamilyRepository;
        }

        public Result<RegisterSubFamilyResponse, Notification> RegisterSubFamily(RegisterSubFamilyRequest request, Guid companyId,Guid userId)
        {
            Notification notification = _registerSubFamilyValidator.Validate(request, companyId);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(companyId);
            Guid familyId = request.FamilyId;
            int orderRow = request.OrderRow;

            SubFamily subFamily = new(description, code, companyId, familyId, Guid.NewGuid(), request.SubFamilyType, orderRow);

            _subFamilyRepository.Save(subFamily);

            _context.SaveChanges(userId);

            var response = new RegisterSubFamilyResponse
            {
                Id = subFamily.Id,
                Code = subFamily.Code,
                Description = subFamily.Description,
                Status = subFamily.Status,
                CompanyId = subFamily.CompanyId,
                FamilyId = subFamily.FamilyId,
            };

            return response;
        }

        public string GenerateCode(Guid companyId)
        {
            return _subFamilyRepository.GenerateCode(companyId);
        }
        public EditSubFamilyResponse EditSubFamily(EditSubFamilyRequest request, SubFamily subFamily,Guid userId)
        {
            subFamily.Description = request.Description.Trim();
            subFamily.Code = request.Code.Trim();
            subFamily.Status = request.Status;
            subFamily.FamilyId = request.FamilyId;
            subFamily.SubFamilyType = request.SubFamilyType;

                
            _context.SaveChanges(userId);

            var response = new EditSubFamilyResponse
            {
                Id = subFamily.Id,
                Code = subFamily.Code,
                Description = subFamily.Description,
                Status = subFamily.Status,
                CompanyId = subFamily.CompanyId,
                FamilyId = subFamily.FamilyId,
            };

            return response;
        }

        public EditSubFamilyResponse ActiveSubFamily(SubFamily subFamily,Guid userId)
        {
            subFamily.Status = true;

            _context.SaveChanges(userId);

            var response = new EditSubFamilyResponse
            {
                Id = subFamily.Id,
                Description = subFamily.Description,
                CompanyId = subFamily.CompanyId,
                Status = subFamily.Status,
                FamilyId = subFamily.FamilyId,
            };

            return response;
        }
        public Notification ValidateEditSubFamilyRequest(EditSubFamilyRequest request, Guid companyId)
        {
            return _editSubFamilyValidator.Validate(request, companyId);
        }
        public EditSubFamilyResponse RemoveSubFamily(SubFamily subFamily,Guid userId)
        {
            subFamily.Status = false;
            _context.SaveChanges(userId);

            var response = new EditSubFamilyResponse
            {
                Id = subFamily.Id,
                Description = subFamily.Description,
                Status = subFamily.Status,
                CompanyId = subFamily.CompanyId,
                FamilyId = subFamily.FamilyId,
            };

            return response;
        }
        public SubFamily? GetById(Guid id)
        {
            return _subFamilyRepository.GetById(id);
        }

        public SubFamilyDto? GetDtoById(Guid id, Guid companyId)
        {
            return _subFamilyRepository.GetDtoById(id, companyId);
        }

        public List<SubFamilyDto> GetListAllByFamilyId(Guid familyId)
        {
            return _subFamilyRepository.GetListAllByFamilyId(familyId);
        }
        public List<SubFamilyDto> GetListAll()
        {
            return _subFamilyRepository.GetListAll();
        }
        public Tuple<IEnumerable<SubFamilyDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _subFamilyRepository.GetList(pageNumber, pageSize, companyId, status, descriptionSearch, codeSearch);
        }
    }
}
