using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Infrastructure.Repositories
{
    public class EmailTemplateRepository : Repository<EmailTemplate>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public EmailTemplateRepository(AnaPreventionContext context) : base(context)
        {
        }

        public EmailTemplate? GetDefault(EmailTagTemplateType emailTagTemplateType)
        {
            return _context.Set<EmailTemplate>().FirstOrDefault(t1 => t1.EmailTagTemplateType == emailTagTemplateType && t1.IsDefault);
        }

        public EmailTemplate? GetDefaultTakeEdit(EmailTagTemplateType emailTagTemplateType,Guid id)
        {
            return _context.Set<EmailTemplate>().FirstOrDefault(t1 => t1.EmailTagTemplateType == emailTagTemplateType && t1.IsDefault && t1.Id != id);
        }

        public EmailTemplate? GetbyDescription(string description)
        {
            return _context.Set<EmailTemplate>().SingleOrDefault(t1 => t1.Description == description);
        }

        public bool NameTakenForEdit(Guid id, string description)
        {
            return _context.Set<EmailTemplate>().Any(t1 => t1.Id != id && t1.Description == description);
        }

        public List<EmailTemplateDto> GetListAll()
        {
            return GetDtoQueryable().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }


        public EmailTemplateDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }

        public List<EmailTemplateDto> GetListFilter(bool status = true, string emailSearch = "", string descriptionSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(emailSearch))
                query.Where(t1 => t1.EmailUser != null && t1.EmailUser.Email.Contains(emailSearch));

            if (!string.IsNullOrEmpty(descriptionSearch))
                query.Where(t1 => t1.Description.Contains(descriptionSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<EmailTemplateDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string? descriptionSearch = "", string? emailSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query.Where(t1 => t1.Description.Contains(descriptionSearch));

            if (!string.IsNullOrEmpty(emailSearch))
                query.Where(t1 => t1.EmailUser != null && t1.EmailUser.Email.Contains(emailSearch));

            var list = query.OrderBy(t1 => t1.Description)
               .Skip(pageSize * (pageNumber - 1))
               .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EmailTemplateDto>, PaginationMetadata>
                (list, paginationMetadata);
        }

        private IQueryable<EmailTemplateDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<EmailTemplate>()
                    where
                         t1.Status
                    select new EmailTemplateDto()
                    {
                        Id = t1.Id,
                        Attachments = _context.Set<Attachment>().Where(st1 => st1.EntityId == t1.Id && st1.EntityType == Attachments.Domain.Enums.EntityType.EMAIL_TEMPLATE).ToList(),
                        Body = t1.Body,
                        Description = t1.Description,
                        EmailUserId = t1.EmailUserId,
                        EmailTagTemplateType = t1.EmailTagTemplateType,
                        Subject = t1.Subject,
                        IsDefault = t1.IsDefault,
                        EmailUser = (from st1 in _context.Set<EmailUser>()
                                     where
                                          st1.Status && st1.Id == t1.EmailUserId
                                     select new EmailUserDto()
                                     {
                                         Id = st1.Id,
                                         Email = st1.Email.Value,
                                         Name = st1.Name,
                                         Status = st1.Status,
                                         ProtocolType = st1.ProtocolType,
                                         Host = st1.Host,
                                         Password = st1.Password,
                                         Port = st1.Port,
                                     }).FirstOrDefault(),
                        Status = t1.Status,
                    });
        }
    }
}
