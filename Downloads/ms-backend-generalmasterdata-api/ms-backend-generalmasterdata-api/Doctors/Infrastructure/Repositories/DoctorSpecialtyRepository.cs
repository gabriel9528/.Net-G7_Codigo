using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories
{
    public class DoctorSpecialtyRepository : Repository<DoctorSpecialty>
    {

        public DoctorSpecialtyRepository(AnaPreventionContext context) : base(context)
        {

        }

        public DoctorSpecialty? GetbyCode(string code)
        {
            return _context.Set<DoctorSpecialty>().SingleOrDefault(x => x.Code == code);
        }
        public DoctorSpecialty? GetbyDoctorIdSpecialtyId(Guid doctorId, Guid SpecialtyId)
        {
            return _context.Set<DoctorSpecialty>().SingleOrDefault(x => x.DoctorId == doctorId && x.SpecialtyId == SpecialtyId);
        }

        public DoctorSpecialty? GetbyAll(Guid doctorId, Guid SpecialtyId, string code)
        {
            return _context.Set<DoctorSpecialty>().SingleOrDefault(
                                x => x.DoctorId == doctorId && x.SpecialtyId == SpecialtyId
                                && x.Code == code
                                );
        }
        public bool CodeTakenForEdit(Guid doctorId, Guid specialtyId, string code)
        {
            return _context.Set<DoctorSpecialty>().Any(
                        c => (c.DoctorId != doctorId && c.SpecialtyId != specialtyId)
                        && c.Code == code);
        }

        public string GetSpecialtyAGGbyDoctorId(Guid doctorId)
        {
            var StringSpecialities = (from t1 in _context.Set<DoctorSpecialty>()
                                      join t2 in _context.Set<Specialty>() on t1.SpecialtyId equals t2.Id
                                      where t1.DoctorId == doctorId && t2.Status
                                      select t2.Description).ToList().ToString();

            if (string.IsNullOrEmpty(StringSpecialities))
                return string.Empty;

            return StringSpecialities;
        }

        public List<DoctorSpecialtyDto>? GetSpecialyDtoByDoctoId(Guid doctorId)
        {
            return (from t1 in _context.Set<DoctorSpecialty>()
                    join t2 in _context.Set<Specialty>() on t1.SpecialtyId equals t2.Id
                    where t1.DoctorId == doctorId && t2.Status
                    select new DoctorSpecialtyDto()
                    {
                        SpecialtyId = t2.Id,
                        Description = t2.Description,
                        Code = t2.Code,
                        DoctorId = t1.DoctorId
                    }).ToList();

        }
    }
}
