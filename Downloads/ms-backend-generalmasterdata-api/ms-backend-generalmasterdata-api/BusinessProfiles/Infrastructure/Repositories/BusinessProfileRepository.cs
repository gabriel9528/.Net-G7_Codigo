using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos;

using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using AnaPrevention.GeneralMasterData.Api.Occupationals.MedicalAppointments.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Infrastructure.Repositories
{
    public class BusinessProfileRepository : Repository<BusinessProfile>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public BusinessProfileRepository(AnaPreventionContext context) : base(context)
        {
        }
        public BusinessProfile? GetbyDescription(string description, Guid businessId)
        {
            return _context.Set<BusinessProfile>().SingleOrDefault(t1 => t1.Description == description && t1.BusinessId == businessId);
        }

        public bool DescriptionTakenForEdit(Guid businessProfile, string description, Guid businessId)
        {
            return _context.Set<BusinessProfile>().Any(t1 => t1.Id != businessProfile && t1.Description == description && t1.BusinessId == businessId);
        }
        public List<BusinessProfileMinDto>? GetListMinByIds(List<Guid>? ids)
        {
            if (ids == null)
                return new();
            return (from t1 in _context.Set<BusinessProfile>()
                    where ids.Contains(t1.Id)
                    select new BusinessProfileMinDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description
                    }).ToList();
        }
        //public List<BusinessProfileProtocolDto> GetValidListAllProtocol(Guid businessId, DateTime? dateTime = null, Guid? subsidiaryId = null)
        //{
        //    if (dateTime == null)
        //        dateTime = DateTimePersonalized.NowPeru;

        //    var list = GetQueryableBusinessProfileProtocolDto()
        //                        .Where(t1 => t1.BusinessId == businessId && t1.Status && t1.DateStart.Date <= ((DateTime)dateTime).Date && t1.DateFinish.Date >= ((DateTime)dateTime).Date).Distinct().ToList();

        //    if (subsidiaryId != null)
        //        list = list.Where(t1 => t1.SubsidiaryIds.Contains((Guid)subsidiaryId)).ToList();

        //    return list;
        //}



        //public List<BusinessProfileProtocolDto> GetListAllProtocol(Guid businessId)
        //{
        //    return GetQueryableBusinessProfileProtocolDto()
        //                        .Where(t1 => t1.BusinessId == businessId && t1.Status).Distinct().ToList();
        //}

        //public IQueryable<BusinessProfileProtocolDto> GetQueryableBusinessProfileProtocolDto()
        //{
        //    return (from t1 in _context.Set<BusinessProfile>()
        //            join t2 in _context.Set<Protocol>() on t1.Id equals t2.BusinessProfileId
        //            where
        //                    t2.StatusProtocol == StatusProtocol.AFFILIATED && t2.IsValidate
        //            select new BusinessProfileProtocolDto()
        //            {
        //                Id = t1.Id,
        //                Description = t1.Description,
        //                BusinessId = t1.BusinessId,
        //                Status = t1.Status,
        //                ProtocolId = t2.Id,
        //                DateFinish = t2.DateFinish,
        //                DateStart = t2.DateStart
        //            });
        //}

        public List<BusinessProfile> GetListAll(Guid businessId)
        {
            return _context.Set<BusinessProfile>()
                           .Where(t1 => t1.BusinessId == businessId && t1.Status)
                           .OrderBy(t1 => t1.Description)
                           .ToList();
        }

        public List<BusinessProfile> GetListFilter(Guid businessId, bool status = true, string descriptionSearch = "")
        {

            var query = _context.Set<BusinessProfile>().Where(t1 => t1.Status == status && t1.BusinessId == businessId).AsQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<BusinessProfile>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessId, bool status = true, string descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;


            var query = _context.Set<BusinessProfile>().Where(t1 => t1.Status == status && t1.BusinessId == businessId).AsQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            var listBusinessProfileDto = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessProfile>, PaginationMetadata>
                (listBusinessProfileDto, paginationMetadata);
        }
    }
}
