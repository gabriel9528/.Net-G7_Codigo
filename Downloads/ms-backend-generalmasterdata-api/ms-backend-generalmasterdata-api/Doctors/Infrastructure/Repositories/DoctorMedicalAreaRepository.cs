using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories
{
    public class DoctorMedicalAreaRepository : Repository<DoctorMedicalArea>
    {

        public DoctorMedicalAreaRepository(AnaPreventionContext context) : base(context)
        {

        }
        public DoctorMedicalArea? GetbyDoctorIdMedicalAreaId(Guid doctorId, Guid medicalAreaId)
        {
            return _context.Set<DoctorMedicalArea>().SingleOrDefault(x => x.DoctorId == doctorId && x.MedicalAreaId == medicalAreaId);
        }

        public List<MedicalAreaDto>? GetDtoByDoctorId(Guid doctorId)
        {
            return (from t1 in _context.Set<MedicalArea>()
                    join t2 in _context.Set<DoctorMedicalArea>() on t1.Id equals t2.MedicalAreaId
                    where t1.Status && t2.DoctorId == doctorId
                    select new MedicalAreaDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Status = t1.Status,

                    }).ToList();
        }
    }
}
