using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Services
{
    public class EmailTagApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterEmailTagValidator _registerEmailTagValidator;
        private readonly EmailTagRepository _emailTagRepository;

        public EmailTagApplicationService(
       AnaPreventionContext context,
       RegisterEmailTagValidator registerEmailTagValidator,
       EmailTagRepository emailTagRepository)
        {
            _context = context;
            _registerEmailTagValidator = registerEmailTagValidator;
            _emailTagRepository = emailTagRepository;
        }

        public Result<RegisterEmailTagResponse, Notification> RegisterEmailTag(RegisterEmailTagRequest request, Guid userId)
        {

           Notification notification = _registerEmailTagValidator.ValidateRegister(request);

            if (notification.HasErrors())
                return notification;

            string description = request.Description.Trim();
            string tag = request.Tag.Trim();
            EmailTagTemplateType emailTagTemplateType = request.EmailTagTemplateType;


            EmailTag emailTag = new(Guid.NewGuid(),description,tag, emailTagTemplateType);

            _emailTagRepository.Save(emailTag);

            _context.SaveChanges(userId);

            var response = new RegisterEmailTagResponse
            {
                Id = emailTag.Id,
                Status = emailTag.Status,
            };

            return response;
        }

        public Notification ValidateEditEmailTagRequest(EditEmailTagRequest request)
        {
            return _registerEmailTagValidator.ValidateEdit(request);
        }

        public RegisterEmailTagResponse EditEmailTag(EditEmailTagRequest request, EmailTag EmailTag, Guid userId)
        {
            EmailTag.Description = request.Description.Trim();
            EmailTag.Tag = request.Tag.Trim();
            EmailTag.EmailTagTemplateType = request.EmailTagTemplateType;

            _context.SaveChanges(userId);

            var response = new RegisterEmailTagResponse
            {
                Id = EmailTag.Id,
                Status = EmailTag.Status,
            };

            return response;
        }

        public RegisterEmailTagResponse ActiveEmailTag(EmailTag emailTag, Guid userId)
        {
            emailTag.Status = true;

            _context.SaveChanges(userId);

            var response = new RegisterEmailTagResponse
            {
                Id = emailTag.Id,
                Status = emailTag.Status,
            };

            return response;
        }
        public RegisterEmailTagResponse RemoveEmailTag(EmailTag emailTag, Guid userId)
        {
            emailTag.Status = false;
            _context.SaveChanges(userId);

            var response = new RegisterEmailTagResponse
            {
                Id = emailTag.Id,
                Status = emailTag.Status,
            };
            return response;
        }

        public EmailTag? GetById(Guid id)
        {
            return _emailTagRepository.GetById(id);
        }
        public EmailTagDto? GetDtoById(Guid id)
        {
            return _emailTagRepository.GetDtoById(id);
        }

        public List<EmailTagDto>? GetListAll(EmailTagTemplateType emailTagTemplateType)
        {
            return _emailTagRepository.GetListAll(emailTagTemplateType);
        }

        public Tuple<IEnumerable<EmailTagDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string tagSearch = "")
        {
            return _emailTagRepository.GetList(pageNumber, pageSize, status, descriptionSearch, tagSearch);
        }
    }
}
