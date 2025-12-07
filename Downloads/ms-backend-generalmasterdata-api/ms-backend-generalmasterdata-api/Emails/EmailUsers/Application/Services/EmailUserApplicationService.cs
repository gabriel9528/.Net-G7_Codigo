using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Services

{
    public class EmailUserApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterEmailUserValidator _registerEmailUserValidator;
        private readonly EditEmailUserValidator _editEmailUserValidator;
        private readonly EmailUserRepository _EmailUserRepository;

        public EmailUserApplicationService(
       AnaPreventionContext context,
       RegisterEmailUserValidator registerEmailUserValidator,
       EditEmailUserValidator editEmailUserValidator,
       EmailUserRepository EmailUserRepository)
        {
            _context = context;
            _registerEmailUserValidator = registerEmailUserValidator;
            _editEmailUserValidator = editEmailUserValidator;
            _EmailUserRepository = EmailUserRepository;
        }

        public Result<RegisterEmailUserResponse, Notification> RegisterEmailUser(RegisterEmailUserRequest request, Guid userId)
        {
            Notification notification = _registerEmailUserValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            int port = request.Port;
            string name = request.Name.Trim();
            string Host = request.Host.Trim();
            ProtocolType protocolType = request.ProtocolType;
            string password = request.Password.Trim();
            Email email = Email.Create(request.Email).Value;


            EmailUser EmailUser = new(Guid.NewGuid(), email, name, password, port, Host, protocolType);

            _EmailUserRepository.Save(EmailUser);

            _context.SaveChanges(userId);

            var response = new RegisterEmailUserResponse
            {
                Id = EmailUser.Id,
                Name = EmailUser.Name,
                Email = EmailUser.Email.Value,
                ProtocolType = EmailUser.ProtocolType,
                Status = EmailUser.Status,
            };

            return response;
        }

        public EditEmailUserResponse EditEmailUser(EditEmailUserRequest request, EmailUser EmailUser, Guid userId)
        {
            EmailUser.Port = request.Port;
            EmailUser.Name = request.Name.Trim();
            EmailUser.Host = request.Host.Trim();
            EmailUser.ProtocolType = request.ProtocolType;
            EmailUser.Password = request.Password.Trim();
            EmailUser.Email = Email.Create(request.Email).Value;

            _context.SaveChanges(userId);

            var response = new EditEmailUserResponse
            {
                Id = EmailUser.Id,
                Name = EmailUser.Name,
                Email = EmailUser.Email.Value,
                ProtocolType = EmailUser.ProtocolType,
                Status = EmailUser.Status,
            };

            return response;
        }

        public EditEmailUserResponse ActiveEmailUser(EmailUser EmailUser, Guid userId)
        {
            EmailUser.Status = true;

            _context.SaveChanges(userId);

            var response = new EditEmailUserResponse
            {
                Id = EmailUser.Id,
                Name = EmailUser.Name,
                Email = EmailUser.Email.Value,
                ProtocolType = EmailUser.ProtocolType,
                Status = EmailUser.Status,
            };

            return response;
        }
        public Notification ValidateEditEmailUserRequest(EditEmailUserRequest request)
        {
            return _editEmailUserValidator.Validate(request);
        }

        public EditEmailUserResponse RemoveEmailUser(EmailUser EmailUser, Guid userId)
        {
            EmailUser.Status = false;
            _context.SaveChanges(userId);

            var response = new EditEmailUserResponse
            {
                Id = EmailUser.Id,
                Name = EmailUser.Name,
                Email = EmailUser.Email.Value,
                ProtocolType = EmailUser.ProtocolType,
                Status = EmailUser.Status,
            };

            return response;
        }

        public EmailUser? GetById(Guid id)
        {
            return _EmailUserRepository.GetById(id);
        }

        public EmailUserDto? GetDtoById(Guid id)
        {
            return _EmailUserRepository.GetDtoById(id);
        }

        public List<EmailUserDto> GetListAll()
        {
            return _EmailUserRepository.GetListAll();
        }
        public Tuple<IEnumerable<EmailUserDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string? nameSearch = "", string? emailSearch = "")
        {
            return _EmailUserRepository.GetList(pageNumber, pageSize, status, nameSearch, emailSearch);
        }
    }
}
