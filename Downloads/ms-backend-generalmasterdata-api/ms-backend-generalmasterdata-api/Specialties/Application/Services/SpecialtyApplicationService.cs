using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;
using AnaPrevention.GeneralMasterData.Api.Specialties.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Specialties.Application.Services
{
    public class SpecialtyApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterSpecialtyValidator _registerSpecialtieValidator;
        private readonly EditSpecialtyValidator _editSpecialtieValidator;
        private readonly SpecialtyRepository _specialtieRepository;

        public SpecialtyApplicationService(
       AnaPreventionContext context,
       RegisterSpecialtyValidator registerSpecialtieValidator,
       EditSpecialtyValidator editSpecialtieValidator,
       SpecialtyRepository specialtieRepository)
        {
            _context = context;
            _registerSpecialtieValidator = registerSpecialtieValidator;
            _editSpecialtieValidator = editSpecialtieValidator;
            _specialtieRepository = specialtieRepository;
        }

        public Result<RegisterSpecialtyResponse, Notification> RegisterSpecialty(RegisterSpecialtyRequest request,Guid userId)
        {
            Notification notification = _registerSpecialtieValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();

            Specialty specialty = new(description, code,Guid.NewGuid());

            _specialtieRepository.Save(specialty);

            _context.SaveChanges(userId);

            var response = new RegisterSpecialtyResponse
            {
                Id = specialty.Id,
                Description = specialty.Description,
                Code = specialty.Code,
                Status = specialty.Status,
            };

            return response;
        }
        public string GenerateCode()
        {
            return _specialtieRepository.GenerateCode();
        }
        public EditSpecialtyResponse EditSpecialty(EditSpecialtyRequest request, Specialty specialtie,Guid userId)
        {
            specialtie.Description = request.Description.Trim();
            specialtie.Code = request.Code.Trim();
            specialtie.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditSpecialtyResponse
            {
                Id = specialtie.Id,
                Description = specialtie.Description,
                Code = specialtie.Code,
                Status = specialtie.Status
            };

            return response;
        }

        public EditSpecialtyResponse ActiveSpecialty(Specialty specialtie,Guid userId)
        {
            specialtie.Status = true;

            _context.SaveChanges(userId);

            var response = new EditSpecialtyResponse
            {
                Id = specialtie.Id,
                Description = specialtie.Description,
                Code = specialtie.Code,
                Status = specialtie.Status
            };

            return response;
        }
        public Notification ValidateEditSpecialtieRequest(EditSpecialtyRequest request)
        {
            return _editSpecialtieValidator.Validate(request);
        }

        public EditSpecialtyResponse RemoveSpecialty(Specialty specialtie,Guid userId)
        {
            specialtie.Status = false;
            _context.SaveChanges(userId);

            var response = new EditSpecialtyResponse
            {
                Id = specialtie.Id,
                Description = specialtie.Description,
                Code = specialtie.Code,
                Status = specialtie.Status
            };

            return response;
        }

        public Specialty? GetById(Guid id)
        {
            return _specialtieRepository.GetById(id);
        }

        public Specialty? GetDtoById(Guid id)
        {
            return _specialtieRepository.GetDtoById(id);
        }

        public List<Specialty> GetListAll()
        {
            return _specialtieRepository.GetListAll();
        }

        public Tuple<IEnumerable<Specialty>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _specialtieRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
