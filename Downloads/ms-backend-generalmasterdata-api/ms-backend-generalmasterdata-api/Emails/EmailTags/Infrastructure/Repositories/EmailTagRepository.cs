using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Infrastructure.Repositories
{
    public class EmailTagRepository(AnaPreventionContext context) : Repository<EmailTag>(context)
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public EmailTag? GetbyTag(string tag, EmailTagTemplateType emailTagTemplateType)
        {
            return _context.Set<EmailTag>().SingleOrDefault(t1 => t1.Tag == tag && t1.EmailTagTemplateType == emailTagTemplateType);
        }
        public EmailTag? GetbyDescription(string description, EmailTagTemplateType emailTagTemplateType)
        {
            return _context.Set<EmailTag>().SingleOrDefault(t1 => t1.Description == description && t1.EmailTagTemplateType == emailTagTemplateType);
        }

        public bool DescriptionTakenForEdit(Guid id, string descrípcion, EmailTagTemplateType emailTagTemplateType)
        {
            return _context.Set<EmailTag>().Any(t1 => t1.Id != id && t1.Description == descrípcion && t1.EmailTagTemplateType == emailTagTemplateType);
        }
        public bool TagTakenForEdit(Guid id, string tag, EmailTagTemplateType emailTagTemplateType)
        {
            return _context.Set<EmailTag>().Any(t1 => t1.Id != id && t1.Tag == tag && t1.EmailTagTemplateType == emailTagTemplateType);
        }
        public EmailTagDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id).SingleOrDefault();
        }

        public List<EmailTagDto>? GetListAll(EmailTagTemplateType emailTagTemplateType)
        {
            return GetDtoQueryable().Where(t1 => t1.EmailTagTemplateType == emailTagTemplateType && t1.Status).ToList();
        }

        private IQueryable<EmailTagDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<EmailTag>()
                    where
                         t1.Status
                    select new EmailTagDto()
                    {
                        Id = t1.Id,
                        Tag = t1.Tag,
                        Description = t1.Description,
                        Status = t1.Status,
                        EmailTagTemplateType = t1.EmailTagTemplateType
                    });
        }

        public List<EmailTagDto> GetListFilter(bool status = true, string descripcionSearch = "", string tagSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descripcionSearch))
                query.Where(t1 => t1.Description.Contains(descripcionSearch));

            if (!string.IsNullOrEmpty(tagSearch))
                query.Where(t1 => t1.Tag.Contains(tagSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<EmailTagDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descripcionSearch = "", string tagSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(tagSearch))
                query.Where(t1 => t1.Tag.Contains(tagSearch));

            if (!string.IsNullOrEmpty(descripcionSearch))
                query.Where(t1 => t1.Description.Contains(descripcionSearch));

            var ListEmailTag = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EmailTagDto>, PaginationMetadata>
                (ListEmailTag, paginationMetadata);
        }
    }
}
