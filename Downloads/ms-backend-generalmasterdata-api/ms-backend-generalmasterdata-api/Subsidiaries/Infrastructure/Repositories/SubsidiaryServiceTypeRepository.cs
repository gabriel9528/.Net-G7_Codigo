using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories
{
    public class SubsidiaryServiceTypeRepository : Repository<SubsidiaryServiceType>
    {
        public SubsidiaryServiceTypeRepository(AnaPreventionContext context) : base(context)
        {

        }

        public SubsidiaryServiceType? GetbyDoctorIdSpecialtyId(Guid subsidiaryId, Guid serviceTypeId)
        {
            return _context.Set<SubsidiaryServiceType>().SingleOrDefault(x => x.SubsidiaryId == subsidiaryId && x.ServiceTypeId == serviceTypeId);
        }

        public List<SubsidiaryServiceType>? GetServiceTypesBySubsidiary(Guid subsidiaryId)
        {
            return _context.Set<SubsidiaryServiceType>().Where(x => x.SubsidiaryId == subsidiaryId).ToList();
        }

        public void RemoveSubsidiaryServiceTypeRange(List<SubsidiaryServiceType> subsidiaryServiceTypes, Guid userId)
        {
            _context.Set<SubsidiaryServiceType>().RemoveRange(subsidiaryServiceTypes);
            _context.SaveChanges(userId);
        }
        public List<ServiceTypeDto>? GetDtoBySubsidiary(Guid subsidiaryId)
        {
            return (from t1 in _context.Set<ServiceType>()
                    join t2 in _context.Set<SubsidiaryServiceType>() on t1.Id equals t2.ServiceTypeId
                    where
                    t2.SubsidiaryId == subsidiaryId
                    select new ServiceTypeDto()
                    {
                        Id = t1.Id,
                        Code = t1.Code,
                        Description = t1.Description,
                        CompanyId = t1.CompanyId,
                        Status = t1.Status,
                    }).ToList();
        }
    }
}
