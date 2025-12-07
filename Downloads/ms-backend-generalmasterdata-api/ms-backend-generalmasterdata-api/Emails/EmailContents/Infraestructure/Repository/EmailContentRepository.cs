using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Domain.Entitites;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Infraestructure.Repository
{
    public class EmailContentRepository : Repository<EmailContent>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public EmailContentRepository(AnaPreventionContext context) : base(context)
        {
        }

        public EmailContent? GetbyFromAddress(string fromAddress)
        {
            return _context.Set<EmailContent>().SingleOrDefault(t1 => t1.FromAddress == fromAddress);
        }
        public EmailContent? GetbyToAddress(string toAddress)
        {
            return _context.Set<EmailContent>().SingleOrDefault(x => x.ToAddress == toAddress);
        }
        public EmailContentDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id).SingleOrDefault();
        }

        private IQueryable<EmailContentDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<EmailContent>()
                    join t2 in _context.Set<Person>() on t1.ToPersonId equals t2.Id into t2_join
                    from t2 in t2_join.DefaultIfEmpty()
                    join t3 in _context.Set<EmailTemplate>() on t1.EmailTemplateId equals t3.Id into t3_join
                    from t3 in t3_join.DefaultIfEmpty()
                    where
                         t1.Status
                    select new EmailContentDto()
                    {
                        Id = t1.Id,
                        FromAddress = t1.FromAddress,
                        ToAddress = t1.ToAddress,
                        Body = t1.Body,
                        AttachmentUrls = CommonStatic.ConvertJsonToDto<List<AttachmentEmailContent>>(t1.AttachmentUrls),
                        DateSend = t1.DateSend.ToString(CommonStatic.FormatDateAndHour),
                        Status = t1.Status,
                        DateSendformat = t1.DateSend,
                        Result = t1.Result,
                        ToPersonId = t1.ToPersonId,
                        EmailTemplateId = t1.EmailTemplateId,
                        ToPerson = t2.Names + " " + t2.LastName,
                        EmailTagTemplateType = t3.EmailTagTemplateType,
                        EmailTemplate = t3.Description,
                        ReferenceId = t1.ReferenceId,
                        Subject = t1.Subject,
                    });
        }

        public List<EmailContentDto> GetListFilter(bool status = true, string fromAddress = "", string toAddress = "", EmailTagTemplateType? emailTagTemplateType = null, DateTime? dateStartSend = null, DateTime? dateFinishSend = null)
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(fromAddress))
                query = query.Where(t1 => t1.FromAddress != null && t1.FromAddress.Contains(fromAddress));

            if (!string.IsNullOrEmpty(toAddress))
                query = query.Where(t1 => t1.ToAddress != null && t1.ToAddress.Contains(toAddress));

            if (dateStartSend != null && dateFinishSend != null)
                query = query.Where(t1 => t1.DateSendformat >= dateStartSend && t1.DateSendformat <= dateFinishSend);

            if (emailTagTemplateType != null)
            {
                query = query.Where(t1 => t1.EmailTagTemplateType == emailTagTemplateType);
            }

            return [.. query.OrderByDescending(t1 => t1.DateSend)];
        }

        public Tuple<IEnumerable<EmailContentDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string fromAddress = "", string toAddress = "", EmailTagTemplateType? emailTagTemplateType = null, DateTime? dateStartSend = null, DateTime? dateFinishSend = null, string? subject = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(fromAddress))
                query = query.Where(t1 => t1.FromAddress != null && t1.FromAddress.Contains(fromAddress));

            if (!string.IsNullOrEmpty(toAddress))
                query = query.Where(t1 => t1.ToAddress != null && t1.ToAddress.Contains(toAddress));

            if (!string.IsNullOrEmpty(subject))
                query = query.Where(t1 => t1.Subject != null && t1.Subject.Contains(subject));

            if (dateStartSend != null && dateFinishSend != null)
                query = query.Where(t1 => t1.DateSendformat >= dateStartSend && t1.DateSendformat <= dateFinishSend);

            if(emailTagTemplateType != null)
            {
                query = query.Where(t1 => t1.EmailTagTemplateType == emailTagTemplateType);
            }

            var ListEmailTag = query.OrderByDescending(t1 => t1.DateSendformat)
               .Skip(pageSize * (pageNumber - 1))
               .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EmailContentDto>, PaginationMetadata>
                (ListEmailTag, paginationMetadata);
        }


    }
}
