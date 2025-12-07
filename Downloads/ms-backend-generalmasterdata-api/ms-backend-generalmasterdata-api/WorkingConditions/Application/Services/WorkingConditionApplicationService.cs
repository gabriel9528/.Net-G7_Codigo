using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Services
{
    public class WorkingConditionApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterWorkingConditionValidator _registerWorkingConditionValidator;
        private readonly EditWorkingConditionValidator _editWorkingConditionValidator;
        private readonly WorkingConditionRepository _workingConditionRepository;

        public WorkingConditionApplicationService(
       AnaPreventionContext context,
       RegisterWorkingConditionValidator registerWorkingConditionValidator,
       EditWorkingConditionValidator editWorkingConditionValidator,
       WorkingConditionRepository workingConditionRepository)
        {
            _context = context;
            _registerWorkingConditionValidator = registerWorkingConditionValidator;
            _editWorkingConditionValidator = editWorkingConditionValidator;
            _workingConditionRepository = workingConditionRepository;



        }

        public Result<RegisterWorkingConditionResponse, Notification> RegisterWorkingCondition(RegisterWorkingConditionRequest request, Guid userId)
        {
            Notification notification = _registerWorkingConditionValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            int code = request.Code;


            WorkingCondition workingCondition = new(description, code, Guid.NewGuid());

            _workingConditionRepository.Save(workingCondition);

            _context.SaveChanges(userId);

            var response = new RegisterWorkingConditionResponse
            {
                Id = workingCondition.Id,
                Description = workingCondition.Description,
                Code = workingCondition.Code,
                Status = workingCondition.Status,
            };

            return response;
        }

        public EditWorkingConditionResponse EditWorkingCondition(EditWorkingConditionRequest request, WorkingCondition workingCondition, Guid userId)
        {
            workingCondition.Description = request.Description.Trim();
            workingCondition.Code = request.Code;
            workingCondition.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditWorkingConditionResponse
            {
                Id = workingCondition.Id,
                Description = workingCondition.Description,
                Code = workingCondition.Code,
                Status = workingCondition.Status
            };

            return response;
        }

        public EditWorkingConditionResponse ActiveWorkingCondition(WorkingCondition workingCondition, Guid userId)
        {
            workingCondition.Status = true;

            _context.SaveChanges(userId);

            var response = new EditWorkingConditionResponse
            {
                Id = workingCondition.Id,
                Description = workingCondition.Description,
                Code = workingCondition.Code,
                Status = workingCondition.Status
            };

            return response;
        }
        public Notification ValidateEditWorkingConditionRequest(EditWorkingConditionRequest request)
        {
            return _editWorkingConditionValidator.Validate(request);
        }

        public EditWorkingConditionResponse RemoveWorkingCondition(WorkingCondition workingCondition, Guid userId)
        {
            workingCondition.Status = false;
            _context.SaveChanges(userId);

            var response = new EditWorkingConditionResponse
            {
                Id = workingCondition.Id,
                Description = workingCondition.Description,
                Code = workingCondition.Code,
                Status = workingCondition.Status
            };

            return response;
        }

        public WorkingCondition? GetById(Guid id)
        {
            return _workingConditionRepository.GetById(id);
        }

        public List<WorkingCondition> GetListAll()
        {
            return _workingConditionRepository.GetListAll();
        }

        
        public Tuple<IEnumerable<WorkingCondition>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _workingConditionRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
