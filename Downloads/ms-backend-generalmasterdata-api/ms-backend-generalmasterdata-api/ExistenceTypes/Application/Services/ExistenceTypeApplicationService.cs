using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Services
{
    public class ExistenceTypeApplicationService
    {

        private readonly AnaPreventionContext _context;
        private readonly RegisterExistenceTypeValidator _registerExistenceTypeValidator;
        private readonly EditExistenceTypeValidator _editExistenceTypeValidator;
        private readonly ExistenceTypeRepository _existenceTypeRepository;

        public ExistenceTypeApplicationService(
       AnaPreventionContext context,
       RegisterExistenceTypeValidator registerExistenceTypeValidator,
       EditExistenceTypeValidator editExistenceTypeValidator,
       ExistenceTypeRepository existenceTypeRepository)
        {
            _context = context;
            _registerExistenceTypeValidator = registerExistenceTypeValidator;
            _editExistenceTypeValidator = editExistenceTypeValidator;
            _existenceTypeRepository = existenceTypeRepository;
        }

        public Result<RegisterExistenceTypeResponse, Notification> RegisterExistenceType(RegisterExistenceTypeRequest request, Guid userId)
        {
            Notification notification = _registerExistenceTypeValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();


            ExistenceType existenceType = new(description, code, Guid.NewGuid());

            _existenceTypeRepository.Save(existenceType);

            _context.SaveChanges(userId);

            var response = new RegisterExistenceTypeResponse
            {
                Id = existenceType.Id,
                Description = existenceType.Description,
                Code = existenceType.Code,
                Status = existenceType.Status,
            };

            return response;
        }

        public string GenerateCode()
        {
            return _existenceTypeRepository.GenerateCode();
        }

        public EditExistenceTypeResponse EditExistenceType(EditExistenceTypeRequest request, ExistenceType existenceType, Guid userId)
        {
            existenceType.Description = request.Description.Trim();
            existenceType.Code = request.Code.Trim();
            existenceType.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditExistenceTypeResponse
            {
                Id = existenceType.Id,
                Description = existenceType.Description,
                Code = existenceType.Code,
                Status = existenceType.Status
            };

            return response;
        }

        public EditExistenceTypeResponse ActiveExistenceType(ExistenceType existenceType, Guid userId)
        {
            existenceType.Status = true;

            _context.SaveChanges(userId);

            var response = new EditExistenceTypeResponse
            {
                Id = existenceType.Id,
                Description = existenceType.Description,
                Code = existenceType.Code,
                Status = existenceType.Status
            };

            return response;
        }
        public Notification ValidateEditExistenceTypeRequest(EditExistenceTypeRequest request)
        {
            return _editExistenceTypeValidator.Validate(request);
        }

        public EditExistenceTypeResponse RemoveExistenceType(ExistenceType existenceType, Guid userId)
        {
            existenceType.Status = false;
            _context.SaveChanges(userId);

            var response = new EditExistenceTypeResponse
            {
                Id = existenceType.Id,
                Description = existenceType.Description,
                Code = existenceType.Code,
                Status = existenceType.Status
            };

            return response;
        }

        public ExistenceType? GetById(Guid id)
        {
            return _existenceTypeRepository.GetById(id);
        }

        public ExistenceType? GetDtoById(Guid id)
        {
            return _existenceTypeRepository.GetDtoById(id);
        }

        public List<ExistenceType> GetListAll()
        {
            return _existenceTypeRepository.GetListAll();
        }
       
        public Tuple<IEnumerable<ExistenceType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _existenceTypeRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
