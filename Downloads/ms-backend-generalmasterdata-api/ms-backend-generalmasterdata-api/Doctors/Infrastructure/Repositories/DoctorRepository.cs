using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos.Security.Views;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;

using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories
{
    public class DoctorRepository : Repository<Doctor>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public DoctorRepository(AnaPreventionContext context) : base(context)
        {
        }

        public DoctorDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public Doctor? GetbyDescription(string description)
        {
            return _context.Set<Doctor>().SingleOrDefault(x => x.Signs == description);
        }
        public Doctor? GetbyCode(string code)
        {
            return _context.Set<Doctor>().SingleOrDefault(x => x.Code == code);
        }

        public Doctor? GetByPersonId(Guid personId)
        {
            return _context.Set<Doctor>().SingleOrDefault(x => x.PersonId == personId);
        }

        public DoctorDto? GetDtoByUserId(Guid userId)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.UserId == userId);
        }

        //public Doctor? GetByUserId(Guid userId)
        //{
        //    return (from t1 in _context.Set<Doctor>()
        //            join t2 in _context.Set<User>() on t1.PersonId equals t2.PersonId
        //            where t2.Id == userId
        //            select t1).FirstOrDefault();
        //}

        public bool DescriptionTakenForEdit(Guid doctorId, string description)
        {
            return _context.Set<Doctor>().Any(c => c.Id != doctorId && c.Signs == description);
        }
        public bool CodeTakenForEdit(Guid doctorId, string code)
        {
            return _context.Set<Doctor>().Any(c => c.Id != doctorId && c.Code == code);
        }

        public List<DoctorDto> GetListAll(string fullName)
        {
            return GetDtoQueryable().Where(t1 => (t1.Names + " " + t1.LastName + " " + t1.SecondLastName).Contains(fullName)).ToList();
        }

        public List<DoctorDto> GetListFilter(
            bool status = true, string displayNameSearch = "", string codeSearch = "", string documentNumberSearch = "", string SpecialtySearch = "")
        {

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(displayNameSearch))
                query = query.Where(t1 => (t1.Names + " " + t1.LastName + " " + t1.SecondLastName).Contains(displayNameSearch));

            if (!string.IsNullOrEmpty(documentNumberSearch))
                query = query.Where(t1 => t1.Code.Contains(documentNumberSearch));

            if (!string.IsNullOrEmpty(SpecialtySearch))
                query = query.Where(t1 => t1.Specialties != null && t1.Specialties.Where(t2 => t2.Description.Contains(SpecialtySearch)).Select(t2 => t2.DoctorId).Contains(t1.Id));


            return query.OrderBy(t1 => t1.LastName).ToList();
        }

        public Tuple<IEnumerable<DoctorDto>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, bool status = true, string displayNameSearch = "", string codeSearch = "", string documentNumberSearch = "", string SpecialtySearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => (t1.Code == codeSearch));

            if (!string.IsNullOrEmpty(displayNameSearch))
                query = query.Where(t1 => (t1.Names + " " + t1.LastName + " " + t1.SecondLastName).Contains(displayNameSearch));

            if (!string.IsNullOrEmpty(documentNumberSearch))
                query = query.Where(t1 => t1.DocumentNumber.Contains(documentNumberSearch));

            if (!string.IsNullOrEmpty(SpecialtySearch))
                query = query.Where(t1 => t1.Specialties != null && t1.Specialties.Where(t2 => t2.Description.Contains(SpecialtySearch)).Select(t2 => t2.DoctorId).Contains(t1.Id));

            var listDoctor = query.OrderBy(t1 => t1.LastName).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<DoctorDto>, PaginationMetadata>
                (listDoctor, paginationMetadata);
        }
        private IQueryable<DoctorDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Doctor>()
                    join t2 in _context.Set<Person>() on t1.PersonId equals t2.Id
                    join t3 in _context.Set<IdentityDocumentType>() on t2.IdentityDocumentTypeId equals t3.Id
                    join t4 in _context.Set<UserMin>() on t1.PersonId equals t4.PersonId into intoT4
                    from t4 in intoT4.DefaultIfEmpty()
                    where
                    t1.Status
                    select new DoctorDto()
                    {
                        Id = t1.Id,
                        UserId = t4.Id,
                        PersonId = t1.PersonId,
                        Certifications = t1.Certifications,
                        Photo = t1.Photo,
                        Signs = t1.Signs,
                        DocumentNumber = t2.DocumentNumber,
                        IdentityDocumentTypeId = t2.IdentityDocumentTypeId,
                        IdentityDocumentTypeDescription = t3.Description,
                        Names = t2.Names,
                        LastName = t2.LastName,
                        SecondLastName = t2.SecondLastName ?? "",
                        PhoneNumber = t2.PhoneNumber ?? "",
                        Email = t2.Email != null ? t2.Email.Value : "",
                        DisplayName = t2.Names + " " + t2.LastName,
                        Code = t1.Code,
                        ListCertifications = CommonStatic.ConvertJsonToDictionaryString(t1.Certifications),
                        Specialties = (from st1 in _context.Set<DoctorSpecialty>()
                                       join st2 in _context.Set<Specialty>() on st1.SpecialtyId equals st2.Id
                                       where st1.DoctorId == t1.Id && st2.Status
                                       select new DoctorSpecialtyDto()
                                       {
                                           SpecialtyId = st2.Id,
                                           Description = st2.Description,
                                           Code = st1.Code,
                                           DoctorId = st1.DoctorId
                                       }).ToList(),
                        Status = t1.Status,

                    });
        }
        //public DoctorFormatCertificationDto? GetDoctorFormatByUserId(Guid userId)
        //{
        //    return GetQueryable().Where(t1 => t1.UserId == userId).FirstOrDefault();
        //}


        //public DoctorFormatCertificationDto? GetDoctorFormatById(Guid doctorId)
        //{
        //    return GetQueryable().Where(t1 => t1.Id == doctorId).FirstOrDefault();
        //}

        //public IQueryable<DoctorFormatCertificationDto> GetQueryable()
        //{
        //    return (from doctor in _context.Set<Doctor>()
        //            join person in _context.Set<Person>() on doctor.PersonId equals person.Id
        //            join identityDocumentType in _context.Set<IdentityDocumentType>() on person.IdentityDocumentTypeId equals identityDocumentType.Id
        //            join user in _context.Set<User>() on person.Id equals user.PersonId into userInto
        //            from user in userInto.DefaultIfEmpty()

        //            select new DoctorFormatCertificationDto()
        //            {
        //                OccupationalHealthId = Guid.Empty,
        //                Id = doctor.Id,
        //                PersonId = doctor.PersonId,
        //                Photo = doctor.Photo,
        //                FullName = (person.Names + " " + person.LastName + " " + person.SecondLastName).Trim(),
        //                Name = person.Names,
        //                LastName = (person.LastName + " " + person.SecondLastName).Trim(),
        //                Signs = doctor.Signs,
        //                UserId = user.Id,
        //                Code = doctor.Code,
        //                Status = doctor.Status,
        //                DocumentNumber = person.DocumentNumber,
        //                DocumentType = identityDocumentType.Abbreviation,
        //                Specialties = (from st1 in _context.Set<DoctorSpecialty>()
        //                               join st2 in _context.Set<Specialty>() on st1.SpecialtyId equals st2.Id
        //                               where st1.DoctorId == doctor.Id && st2.Status
        //                               select new DoctorSpecialtyDto()
        //                               {
        //                                   SpecialtyId = st2.Id,
        //                                   Description = st2.Description,
        //                                   Code = st2.Code,
        //                                   DoctorId = st1.DoctorId
        //                               }).ToList()

        //            });
        //}

        //public DoctorFormatCertificationDto? GetDtoDoctorByOccupationalHealthId(Guid occupationalHealthId) => GetDoctorFromOccupationalHealth().Where(t1 => t1.OccupationalHealthId == occupationalHealthId).SingleOrDefault();

        public DoctorFormatCertificationDto? GetDtoDoctorCammoBySubsidiary(Guid subsidiatyId)
        {
            Guid? doctorid = _context.Set<Subsidiary>().Where(t1 => t1.Id == subsidiatyId).Select(t1 => t1.CamoDoctorId).FirstOrDefault();

            if (doctorid != null)
                return GetDoctorCertification().Where(t1 => t1.Id == (Guid)doctorid).FirstOrDefault();

            return null;
        }

        public IQueryable<DoctorFormatCertificationDto> GetDoctorCertification()
        {
            return (from doctor in _context.Set<Doctor>()
                    join person in _context.Set<Person>() on doctor.PersonId equals person.Id
                    join identityDocumentType in _context.Set<IdentityDocumentType>() on person.IdentityDocumentTypeId equals identityDocumentType.Id
                    join user in _context.Set<UserMin>() on person.Id equals user.PersonId into userInto
                    from user in userInto.DefaultIfEmpty()

                    select new DoctorFormatCertificationDto()
                    {
                        OccupationalHealthId = Guid.Empty,
                        Id = doctor.Id,
                        UserId = user.Id,
                        PersonId = doctor.PersonId,
                        Photo = doctor.Photo,
                        FullName = (person.Names + " " + person.LastName + " " + person.SecondLastName).Trim(),
                        Name = person.Names,
                        LastName = (person.LastName + " " + person.SecondLastName).Trim(),
                        Signs = doctor.Signs,
                        Code = doctor.Code,
                        Status = doctor.Status,
                        DocumentNumber = person.DocumentNumber,
                        DocumentType = identityDocumentType.Abbreviation,
                        Specialties = (from st1 in _context.Set<DoctorSpecialty>()
                                       join st2 in _context.Set<Specialty>() on st1.SpecialtyId equals st2.Id
                                       where st1.DoctorId == doctor.Id && st2.Status
                                       select new DoctorSpecialtyDto()
                                       {
                                           SpecialtyId = st2.Id,
                                           Description = st2.Description,
                                           Code = st2.Code,
                                           DoctorId = st1.DoctorId
                                       }).ToList()

                    });
        }

        //public IQueryable<DoctorFormatCertificationDto> GetDoctorFromOccupationalHealth()
        //{
        //    return (from doctor in _context.Set<Doctor>()
        //            join occupationalHealth in _context.Set<OccupationalHealth>() on doctor.Id equals occupationalHealth.DoctorId
        //            join person in _context.Set<Person>() on doctor.PersonId equals person.Id
        //            join identityDocumentType in _context.Set<IdentityDocumentType>() on person.IdentityDocumentTypeId equals identityDocumentType.Id
        //            join user in _context.Set<User>() on person.Id equals user.PersonId into userInto
        //            from user in userInto.DefaultIfEmpty()

        //            select new DoctorFormatCertificationDto()
        //            {
        //                OccupationalHealthId = occupationalHealth.Id,
        //                Id = doctor.Id,
        //                PersonId = doctor.PersonId,
        //                Photo = doctor.Photo,
        //                UserId = user.Id,
        //                FullName = (person.Names + " " + person.LastName + " " + person.SecondLastName).Trim(),
        //                Name = person.Names,
        //                LastName = (person.LastName + " " + person.SecondLastName).Trim(),
        //                Signs = doctor.Signs,
        //                Code = doctor.Code,
        //                Status = doctor.Status,
        //                DocumentNumber = person.DocumentNumber,
        //                DocumentType = identityDocumentType.Abbreviation,
        //                Specialties = (from st1 in _context.Set<DoctorSpecialty>()
        //                               join st2 in _context.Set<Specialty>() on st1.SpecialtyId equals st2.Id
        //                               where st1.DoctorId == doctor.Id && st2.Status
        //                               select new DoctorSpecialtyDto()
        //                               {
        //                                   SpecialtyId = st2.Id,
        //                                   Description = st2.Description,
        //                                   Code = st2.Code,
        //                                   DoctorId = st1.DoctorId
        //                               }).ToList()

        //            });
        //}
    }

}
