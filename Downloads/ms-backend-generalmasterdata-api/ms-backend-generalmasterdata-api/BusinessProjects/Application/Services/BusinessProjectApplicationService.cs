using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Services
{
    public class BusinessProjectApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterBusinessProjectValidator _registerBusinessProjectValidator;
        private readonly EditBusinessProjectValidator _editBusinessProjectValidator;
        private readonly BusinessProjectRepository _businessProjectRepository;
        
        private readonly RegisterListBusinessProjectValidator _registerListBusinessProjectValidator;
      

        public BusinessProjectApplicationService(
       AnaPreventionContext context,
       RegisterBusinessProjectValidator registerBusinessProjectValidator,
       EditBusinessProjectValidator editBusinessProjectValidator,
       BusinessProjectRepository businessProjectRepository,
       
       RegisterListBusinessProjectValidator registerListBusinessProjectValidator)
        {
            _context = context;
            _registerBusinessProjectValidator = registerBusinessProjectValidator;
            _editBusinessProjectValidator = editBusinessProjectValidator;
            _businessProjectRepository = businessProjectRepository;
            
            _registerListBusinessProjectValidator = registerListBusinessProjectValidator;
            
        }

        public Result<RegisterListBusinessProjectResponse, Notification> RegisterListBusinessProject(RegisterListBusinessProjectRequest request, Guid userId)
        {

            List<string> ListDescription = new();
            request.ListDescription = request.ListDescription.Distinct().ToList();
            foreach (string Description in request.ListDescription)
            {
                Notification notification = _registerListBusinessProjectValidator.Validate(request);

                if (notification.HasErrors())
                    return notification;


                string description = Description.Trim();
                Guid businessId = request.BusinessId;


                BusinessProject businessProject = new(description, businessId);

                _businessProjectRepository.Save(businessProject);
                ListDescription.Add(businessProject.Description);
            }
            _context.SaveChanges(userId);
            var response = new RegisterListBusinessProjectResponse
            {
                ListDescription = ListDescription,
                BusinessId = request.BusinessId,
            };

            return response;
        }
        public Result<RegisterBusinessProjectResponse, Notification> RegisterBusinessProject(RegisterBusinessProjectRequest request, Guid userId)
        {
            Notification notification = _registerBusinessProjectValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            Guid businessId = request.BusinessId;


            BusinessProject businessProject = new(description, businessId);

            _businessProjectRepository.Save(businessProject);

            _context.SaveChanges(userId);

            var response = new RegisterBusinessProjectResponse
            {
                Id = businessProject.Id,
                Description = businessProject.Description,
                BusinessId = businessProject.BusinessId,
                Status = businessProject.Status,
            };

            return response;
        }

        public EditBusinessProjectResponse EditBusinessProject(EditBusinessProjectRequest request, BusinessProject businessProject, Guid userId)
        {
            businessProject.Description = request.Description.Trim();


            _context.SaveChanges(userId);

            var response = new EditBusinessProjectResponse
            {
                Id = businessProject.Id,
                Description = businessProject.Description,
                BusinessId = businessProject.BusinessId,
                Status = businessProject.Status
            };

            return response;
        }

        public EditBusinessProjectResponse ActiveBusinessProject(BusinessProject businessProject, Guid userId)
        {
            businessProject.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessProjectResponse
            {
                Id = businessProject.Id,
                Description = businessProject.Description,
                BusinessId = businessProject.BusinessId,
                Status = businessProject.Status
            };

            return response;
        }
        public Notification ValidateEditBusinessProjectRequest(EditBusinessProjectRequest request)
        {
            return _editBusinessProjectValidator.Validate(request);
        }

        public EditBusinessProjectResponse RemoveBusinessProject(BusinessProject businessProject, Guid userId)
        {
            businessProject.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessProjectResponse
            {
                Id = businessProject.Id,
                Description = businessProject.Description,
                BusinessId = businessProject.BusinessId,
                Status = businessProject.Status
            };

            return response;
        }

        public BusinessProject? GetById(Guid id)
        {
            return _businessProjectRepository.GetById(id);
        }

        public BusinessProject? GetDtoById(Guid id)
        {
            return _businessProjectRepository.GetDtoById(id);
        }
        
        public List<BusinessProject> GetListAll(Guid businessId)
        {
            return _businessProjectRepository.GetListAll(businessId);
        }

        public Tuple<IEnumerable<BusinessProject>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessId, bool status, string descriptionSearch = "")
        {
            return _businessProjectRepository.GetList(pageNumber, pageSize, businessId, status, descriptionSearch);
        }

      

    }
}
