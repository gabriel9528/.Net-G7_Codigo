using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Infrastructure.Repositories
{
    public class BusinessCampaignRepository : Repository<BusinessCampaign>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public BusinessCampaignRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessCampaignDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id && t1.Status).FirstOrDefault();
        }
        public BusinessCampaign? GetbyDescription(string description)
        {
            return _context.Set<BusinessCampaign>().SingleOrDefault(x => x.Description == description);
        }

        public bool DescriptionTakenForEdit(Guid businessCampaign, string description)
        {
            return _context.Set<BusinessCampaign>().Any(c => c.Id != businessCampaign && c.Description == description);
        }

        public List<BusinessCampaignDto> GetListAll(Guid businessId)
        {
            return GetDtoQueryable().Where(t1 => t1.BusinessId == businessId && t1.Status).ToList();
        }

        public List<BusinessCampaignDto> GetListFilter(Guid businessId, bool status = true, string descriptionSearch = "")
        {
            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.Where(t1 => t1.Status == status && t1.BusinessId == businessId).ToList();

        }
        public Tuple<IEnumerable<BusinessCampaignDto>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessId, bool status = true, string descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;


            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            query = query.Where(t1 => t1.Status == status && t1.BusinessId == businessId);

            var listBusinessCampaignDto = query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();

            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessCampaignDto>, PaginationMetadata>
                (listBusinessCampaignDto, paginationMetadata);
        }
        private IQueryable<BusinessCampaignDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<BusinessCampaign>()
                    orderby t1.Description
                    select new BusinessCampaignDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        BusinessId = t1.BusinessId,
                        Status = t1.Status,
                        DateStart = t1.DateStart,
                        DateFinish = t1.DateFinish
                    });
        }
    }
}
