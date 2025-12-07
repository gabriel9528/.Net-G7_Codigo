using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Services
{
    public class MedicalAreaApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterMedicalAreaValidator _registerMedicalAreaValidator;
        private readonly EditMedicalAreaValidator _editMedicalAreaValidator;
        private readonly MedicalAreaRepository _medicalAreaRepository;

        public MedicalAreaApplicationService(
       AnaPreventionContext context,
       RegisterMedicalAreaValidator registerMedicalAreaValidator,
       EditMedicalAreaValidator editMedicalAreaValidator,
       MedicalAreaRepository medicalAreaRepository)
        {
            _context = context;
            _registerMedicalAreaValidator = registerMedicalAreaValidator;
            _editMedicalAreaValidator = editMedicalAreaValidator;
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Result<RegisterMedicalAreaResponse, Notification> RegisterMedicalArea(RegisterMedicalAreaRequest request, Guid userId)
        {
            Notification notification = _registerMedicalAreaValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();
            int orderRowTourSheet = request.OrderRowTourSheet;

            MedicalArea medicalArea = new(description, code, Guid.NewGuid(), orderRowTourSheet);

            _medicalAreaRepository.Save(medicalArea);

            _context.SaveChanges(userId);

            var response = new RegisterMedicalAreaResponse
            {
                Id = medicalArea.Id,
                Description = medicalArea.Description,
                Code = medicalArea.Code,
                Status = medicalArea.Status,
            };

            return response;
        }

        public string GenerateCode()
        {
            return _medicalAreaRepository.GenerateCode();
        }

        public EditMedicalAreaResponse EditMedicalArea(EditMedicalAreaRequest request, MedicalArea medicalArea, Guid userId)
        {
            medicalArea.Description = request.Description.Trim();
            medicalArea.Code = request.Code.Trim();
            medicalArea.Status = request.Status;

            _context.SaveChanges(userId);

            var response = new EditMedicalAreaResponse
            {
                Id = medicalArea.Id,
                Description = medicalArea.Description,
                Code = medicalArea.Code,
                Status = medicalArea.Status
            };

            return response;
        }

        public EditMedicalAreaResponse ActiveMedicalArea(MedicalArea medicalArea, Guid userId)
        {
            medicalArea.Status = true;

            _context.SaveChanges(userId);

            var response = new EditMedicalAreaResponse
            {
                Id = medicalArea.Id,
                Description = medicalArea.Description,
                Code = medicalArea.Code,
                Status = medicalArea.Status
            };

            return response;
        }
        public Notification ValidateEditMedicalAreaRequest(EditMedicalAreaRequest request)
        {
            return _editMedicalAreaValidator.Validate(request);
        }

        public EditMedicalAreaResponse RemoveMedicalArea(MedicalArea medicalArea, Guid userId)
        {
            medicalArea.Status = false;
            _context.SaveChanges(userId);

            var response = new EditMedicalAreaResponse
            {
                Id = medicalArea.Id,
                Description = medicalArea.Description,
                Code = medicalArea.Code,
                Status = medicalArea.Status
            };

            return response;
        }

        public MedicalArea? GetById(Guid id)
        {
            return _medicalAreaRepository.GetById(id);
        }

        public MedicalArea? GetDtoById(Guid id)
        {
            return _medicalAreaRepository.GetDtoById(id);
        }
        public List<MedicalArea> GetListAll()
        {
            return _medicalAreaRepository.GetListAll();
        }
        public Tuple<IEnumerable<MedicalArea>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _medicalAreaRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
