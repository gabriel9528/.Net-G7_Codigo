using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Services
{
    public class MedicalFormApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterMedicalFormValidator _registerMedicalFormValidator;
        private readonly EditMedicalFormValidator _editMedicalFormValidator;
        private readonly MedicalFormRepository _medicalFormRepository;

        public MedicalFormApplicationService(
       AnaPreventionContext context,
       RegisterMedicalFormValidator registerMedicalFormValidator,
       EditMedicalFormValidator editMedicalFormValidator,
       MedicalFormRepository medicalFormRepository)
        {
            _context = context;
            _registerMedicalFormValidator = registerMedicalFormValidator;
            _editMedicalFormValidator = editMedicalFormValidator;
            _medicalFormRepository = medicalFormRepository;
        }

        public Result<RegisterMedicalFormResponse, Notification> RegisterMedicalForm(RegisterMedicalFormRequest request, Guid userId)
        {
            Notification notification = _registerMedicalFormValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            Guid serviceTypeId = request.ServiceTypeId;
            Guid medicalAreaId = request.MedicalAreaId;
            string code = request.Code.Replace(" ", String.Empty); ;
            MedicalFormsType medicalFormsType = request.MedicalFormsType;


            MedicalForm medicalForm = new(description, serviceTypeId, medicalAreaId, medicalFormsType, code, Guid.NewGuid());

            _medicalFormRepository.Save(medicalForm);

            _context.SaveChanges(userId);

            var response = new RegisterMedicalFormResponse
            {
                Id = medicalForm.Id,
                Code = medicalForm.Code,
                Description = medicalForm.Description,
                Status = medicalForm.Status,
                ServiceTypeId = medicalForm.ServiceTypeId,
                MedicalAreaId = medicalForm.MedicalAreaId,
                MedicalFormsType = medicalForm.MedicalFormsType,
            };

            return response;
        }

        public EditMedicalFormResponse EditMedicalForm(EditMedicalFormRequest request, MedicalForm medicalForm, Guid userId)
        {
            medicalForm.Description = request.Description.Trim();
            medicalForm.Status = request.Status;
            medicalForm.ServiceTypeId = request.ServiceTypeId;
            medicalForm.MedicalAreaId = request.MedicalAreaId;
            medicalForm.MedicalFormsType = request.MedicalFormsType;
            medicalForm.Code = request.Code.Replace(" ", String.Empty);


            _context.SaveChanges(userId);

            var response = new EditMedicalFormResponse
            {
                Id = medicalForm.Id,
                Code = medicalForm.Code,
                Description = medicalForm.Description,
                Status = medicalForm.Status,
                ServiceTypeId = medicalForm.ServiceTypeId,
                MedicalAreaId = medicalForm.MedicalAreaId,
                MedicalFormsType = medicalForm.MedicalFormsType,
            };

            return response;
        }

        public EditMedicalFormResponse ActiveMedicalForm(MedicalForm medicalForm, Guid userId)
        {
            medicalForm.Status = true;

            _context.SaveChanges(userId);

            var response = new EditMedicalFormResponse
            {
                Id = medicalForm.Id,
                Code = medicalForm.Code,
                Description = medicalForm.Description,
                Status = medicalForm.Status,
                ServiceTypeId = medicalForm.ServiceTypeId,
                MedicalAreaId = medicalForm.MedicalAreaId,
                MedicalFormsType = medicalForm.MedicalFormsType,
            };

            return response;
        }
        public Notification ValidateEditMedicalFormRequest(EditMedicalFormRequest request)
        {
            return _editMedicalFormValidator.Validate(request);
        }

        public EditMedicalFormResponse RemoveMedicalForm(MedicalForm medicalForm, Guid userId)
        {
            medicalForm.Status = false;
            _context.SaveChanges(userId);

            var response = new EditMedicalFormResponse
            {
                Id = medicalForm.Id,
                Code = medicalForm.Code,
                Description = medicalForm.Description,
                Status = medicalForm.Status,
                ServiceTypeId = medicalForm.ServiceTypeId,
                MedicalAreaId = medicalForm.MedicalAreaId,
                MedicalFormsType = medicalForm.MedicalFormsType,
            };

            return response;
        }

        public MedicalForm? GetById(Guid id)
        {
            return _medicalFormRepository.GetById(id);
        }

        public MedicalFormDto? GetDtoById(Guid id)
        {
            return _medicalFormRepository.GetDtoById(id);
        }

        public List<MedicalForm> GetListAll()
        {
            return _medicalFormRepository.GetListAll();
        }

        public Tuple<IEnumerable<MedicalFormDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string serviceTypeSearch = "", string medicalAreaSearch = "")
        {
            return _medicalFormRepository.GetList(pageNumber, pageSize, status, descriptionSearch, serviceTypeSearch, medicalAreaSearch);
        }
    }
}
