using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Services
{
    public class MedicalFormatApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterMedicalFormatValidator _registerMedicalFormatValidator;
        private readonly EditMedicalFormatValidator _editMedicalFormatValidator;
        private readonly MedicalFormatRepository _medicalFormatRepository;

        public MedicalFormatApplicationService(
       AnaPreventionContext context,
       RegisterMedicalFormatValidator registerMedicalFormatValidator,
       EditMedicalFormatValidator editMedicalFormatValidator,
       MedicalFormatRepository medicalFormatRepository)
        {
            _context = context;
            _registerMedicalFormatValidator = registerMedicalFormatValidator;
            _editMedicalFormatValidator = editMedicalFormatValidator;
            _medicalFormatRepository = medicalFormatRepository;
        }

        public Result<RegisterMedicalFormatResponse, Notification> RegisterMedicalFormat(RegisterMedicalFormatRequest request, Guid userId)
        {
            Notification notification = _registerMedicalFormatValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();
            MedicalFormatType medicalFormatType = request.MedicalFormatType;


            MedicalFormat medicalFormat = new(description, code, medicalFormatType, Guid.NewGuid());

            _medicalFormatRepository.Save(medicalFormat);

            _context.SaveChanges(userId);

            var response = new RegisterMedicalFormatResponse
            {
                Id = medicalFormat.Id,
                Description = medicalFormat.Description,
                Code = medicalFormat.Code,
                Status = medicalFormat.Status,
                MedicalFormatType = medicalFormat.MedicalFormatType,
            };

            return response;
        }
        public string GenerateCode()
        {
            return _medicalFormatRepository.GenerateCode();
        }
        public EditMedicalFormatResponse EditMedicalFormat(EditMedicalFormatRequest request, MedicalFormat medicalFormat, Guid userId)
        {
            medicalFormat.Description = request.Description.Trim();
            medicalFormat.Code = request.Code.Trim();
            medicalFormat.MedicalFormatType = request.MedicalFormatType;


            _context.SaveChanges(userId);

            var response = new EditMedicalFormatResponse
            {
                Id = medicalFormat.Id,
                Description = medicalFormat.Description,
                Code = medicalFormat.Code,
                Status = medicalFormat.Status,
                MedicalFormatType = medicalFormat.MedicalFormatType,
            };

            return response;
        }

        public EditMedicalFormatResponse ActiveMedicalFormat(MedicalFormat medicalFormat, Guid userId)
        {
            medicalFormat.Status = true;

            _context.SaveChanges(userId);

            var response = new EditMedicalFormatResponse
            {
                Id = medicalFormat.Id,
                Description = medicalFormat.Description,
                Code = medicalFormat.Code,
                Status = medicalFormat.Status,
                MedicalFormatType = medicalFormat.MedicalFormatType,
            };

            return response;
        }
        public Notification ValidateEditMedicalFormatRequest(EditMedicalFormatRequest request)
        {
            return _editMedicalFormatValidator.Validate(request);
        }

        public EditMedicalFormatResponse RemoveMedicalFormat(MedicalFormat medicalFormat, Guid userId)
        {
            medicalFormat.Status = false;
            _context.SaveChanges(userId);

            var response = new EditMedicalFormatResponse
            {
                Id = medicalFormat.Id,
                Description = medicalFormat.Description,
                Code = medicalFormat.Code,
                Status = medicalFormat.Status,
                MedicalFormatType = medicalFormat.MedicalFormatType,
            };

            return response;
        }

        public MedicalFormat? GetById(Guid id)
        {
            return _medicalFormatRepository.GetById(id);
        }

        public List<MedicalFormat> GetListAll()
        {
            return _medicalFormatRepository.GetListAll();
        }
        public Tuple<IEnumerable<MedicalFormat>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _medicalFormatRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
