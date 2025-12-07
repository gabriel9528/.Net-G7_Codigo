using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Services
{
    public class FamilyApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterFamilyValidator _registerFamilyValidator;
        private readonly EditFamilyValidator _editFamilyValidator;
        private readonly FamilyRepository _familyRepository;

        public FamilyApplicationService(
       AnaPreventionContext context,
       RegisterFamilyValidator registerFamilyValidator,
       EditFamilyValidator editFamilyValidator,
       FamilyRepository familyRepository)
        {
            _context = context;
            _registerFamilyValidator = registerFamilyValidator;
            _editFamilyValidator = editFamilyValidator;
            _familyRepository = familyRepository;
        }

        public Result<RegisterFamilyResponse, Notification> RegisterFamily(RegisterFamilyRequest request, Guid companyId, Guid userId)
        {
            Notification notification = _registerFamilyValidator.Validate(request, companyId);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(companyId);
            Guid lineId = request.LineId;
            int orderRow = request.OrderRow;

            Family family = new(description, code, companyId, lineId, Guid.NewGuid(), orderRow);

            _familyRepository.Save(family);

            _context.SaveChanges(userId);

            var response = new RegisterFamilyResponse
            {
                Id = family.Id,
                Code = family.Code,
                Description = family.Description,
                Status = family.Status,
                CompanyId = family.CompanyId,
                LineId = family.LineId,
            };

            return response;
        }

        public string GenerateCode(Guid companyId)
        {
            return _familyRepository.GenerateCode(companyId);
        }
        public EditFamilyResponse EditFamily(EditFamilyRequest request, Family family, Guid userId)
        {
            family.Description = request.Description.Trim();
            family.Code = request.Code.Trim();
            family.Status = request.Status;
            family.LineId = request.LineId;


            _context.SaveChanges(userId);

            var response = new EditFamilyResponse
            {
                Id = family.Id,
                Code = family.Code,
                Description = family.Description,
                Status = family.Status,
                CompanyId = family.CompanyId,
                LineId = family.LineId,
            };

            return response;
        }

        public EditFamilyResponse ActiveFamily(Family family, Guid userId)
        {
            family.Status = true;

            _context.SaveChanges(userId);

            var response = new EditFamilyResponse
            {
                Id = family.Id,
                Description = family.Description,
                CompanyId = family.CompanyId,
                Status = family.Status,
                LineId = family.LineId,
            };

            return response;
        }
        public Notification ValidateEditFamilyRequest(EditFamilyRequest request, Guid companyId)
        {
            return _editFamilyValidator.Validate(request, companyId);
        }
        public EditFamilyResponse RemoveFamily(Family family, Guid userId)
        {
            family.Status = false;
            _context.SaveChanges(userId);

            var response = new EditFamilyResponse
            {
                Id = family.Id,
                Description = family.Description,
                Status = family.Status,
                CompanyId = family.CompanyId,
                LineId = family.LineId,
            };

            return response;
        }
        public Family? GetById(Guid id)
        {
            return _familyRepository.GetById(id);
        }

        public FamilyDto? GetDtoById(Guid id, Guid companyId)
        {
            return _familyRepository.GetDtoById(id, companyId);
        }

        public List<FamilyDto> GetListAll()
        {
            return _familyRepository.GetListAll();
        }
        public List<FamilyDto> GetListAllByLineId(Guid lineId)
        {
            return _familyRepository.GetListAllByLineId(lineId);
        }

       
        public Tuple<IEnumerable<FamilyDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _familyRepository.GetList(pageNumber, pageSize, companyId, status, descriptionSearch, codeSearch);
        }
    }
}
