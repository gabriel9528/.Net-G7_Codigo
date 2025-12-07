using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;

using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories
{
    public class SubsidiaryRepository : Repository<Subsidiary>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public SubsidiaryRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<SubsidiaryMinDto>? GetMinDtoByIds(List<Guid> ids)
        {
            return (from t1 in _context.Set<Subsidiary>()
                    where ids.Contains(t1.Id)
                    select new SubsidiaryMinDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Status = t1.Status,
                    }).ToList();
        }

        public SubsidiaryMinDto? GetMinDtoById(Guid id, Guid companyId)
        {
            return (from t1 in _context.Set<Subsidiary>()
                    where t1.Id == id && t1.CompanyId == companyId
                    select new SubsidiaryMinDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Status = t1.Status,
                    }).SingleOrDefault();
        }
        public SubsidiaryDto? GetDtoById(Guid id, Guid companyId)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id && t1.CompanyId == companyId).SingleOrDefault();
        }
        public Subsidiary? GetbyDescription(string description, Guid companyId)
        {
            return _context.Set<Subsidiary>().SingleOrDefault(x => x.Description == description && x.CompanyId == companyId);
        }
        public Subsidiary? GetbyCode(string code, Guid companyId)
        {
            return _context.Set<Subsidiary>().SingleOrDefault(x => x.Code == code && x.CompanyId == companyId);
        }
        public bool DescriptionTakenForEdit(Guid subsidiary, string description, Guid companyId)
        {
            return _context.Set<Subsidiary>().Any(c => c.Id != subsidiary && c.Description == description && c.CompanyId == companyId);
        }
        public bool CodeTakenForEdit(Guid subsidiaryId, string code, Guid companyId)
        {
            return _context.Set<Subsidiary>().Any(c => c.Id != subsidiaryId && c.Code == code && c.CompanyId == companyId);
        }

        public List<SubsidiaryDto> GetListAll(Guid companyId)
        {
            return [.. GetDtoQueryable().Where(t1 => t1.CompanyId == companyId)];
        }

        public string GenerateCode(Guid companyId)
        {
            var codeStrings = _context.Set<Subsidiary>().Where(t1 => t1.CompanyId == companyId)
            .Select(t1 => t1.Code)
            .Where(c => !string.IsNullOrEmpty(c))
            .ToList();

            var codes = codeStrings
                .Select(c =>
                {
                    bool isValid = int.TryParse(c, out int parsedCode);
                    return new { IsValid = isValid, Code = parsedCode };
                })
                .Where(c => c.IsValid)
                .Select(c => c.Code)
                .ToList();

            var codeMax = codes.Count != 0 ? codes.Max() : 0;

            var newCode = (codeMax + 1).ToString("D" + CommonStatic.numberZerosCode);

            return newCode;
        }
        public List<SubsidiaryDto> GetListFilter(
            Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "", string serviceTypeSearch = "", string subsidiaryTypeSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            if (!string.IsNullOrEmpty(subsidiaryTypeSearch))
                query = query.Where(t1 => t1.SubsidiaryType.Contains(subsidiaryTypeSearch));

            if (!string.IsNullOrEmpty(serviceTypeSearch))
            {
                var SubsidiaryIds = (from t1 in _context.Set<ServiceType>()
                                     join t2 in _context.Set<SubsidiaryServiceType>() on t1.Id equals t2.ServiceTypeId
                                     where t1.Description.Contains(serviceTypeSearch)
                                     select t2.SubsidiaryId).ToArray();
                query = query.Where(t1 => SubsidiaryIds.Contains(t1.Id));
            }

            return [.. query.OrderBy(t1 => t1.Description)];
        }
        public Tuple<IEnumerable<SubsidiaryDto>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid companyId, bool status = true,
            string descriptionSearch = "", string codeSearch = "", string serviceTypeSearch = "", string subsidiaryTypeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            if (!string.IsNullOrEmpty(subsidiaryTypeSearch))
                query = query.Where(t1 => t1.SubsidiaryType.Contains(subsidiaryTypeSearch));

            if (!string.IsNullOrEmpty(serviceTypeSearch))
            {
                var SubsidiaryIds = (from t1 in _context.Set<ServiceType>()
                                     join t2 in _context.Set<SubsidiaryServiceType>() on t1.Id equals t2.ServiceTypeId
                                     where t1.Description.Contains(serviceTypeSearch)
                                     select t2.SubsidiaryId).ToArray();
                query = query.Where(t1 => SubsidiaryIds.Contains(t1.Id));
            }

            var listSubsidiary = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<SubsidiaryDto>, PaginationMetadata>
                (listSubsidiary, paginationMetadata);
        }
        private IQueryable<SubsidiaryDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Subsidiary>()
                    join t2 in _context.Set<District>() on t1.DistrictId equals t2.Id
                    join t3 in _context.Set<SubsidiaryType>() on t1.SubsidiaryTypeId equals t3.Id
                    join t4 in _context.Set<Doctor>() on t1.DoctorId equals t4.Id into t4_join
                    from t4 in t4_join.DefaultIfEmpty()
                    join t5 in _context.Set<Province>() on t2.ProvinceId equals t5.Id
                    join t6 in _context.Set<Department>() on t5.DepartmentId equals t6.Id
                    join t7 in _context.Set<Person>() on t4.PersonId equals t7.Id into t7_join
                    from t7 in t7_join.DefaultIfEmpty()
                    join t8 in _context.Set<Doctor>() on t1.CamoDoctorId equals t8.Id into t8_join
                    from t8 in t8_join.DefaultIfEmpty()
                    join t9 in _context.Set<Person>() on t8.PersonId equals t9.Id into t9_join
                    from t9 in t9_join.DefaultIfEmpty()
                    select new SubsidiaryDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        District = t2.Description,
                        Province = t5.Description,
                        Department = t6.Description,
                        DistrictId = t1.DistrictId,
                        Address = t1.Address,
                        LogoUrl = t1.LogoUrl,
                        //PhoneNumber = t1.PhoneNumber,
                        LedgerAccount = t1.LedgerAccount,
                        SubsidiaryType = t3.Description,
                        SubsidiaryTypeId = t1.SubsidiaryTypeId,
                        ServiceTypes = (from st1 in _context.Set<ServiceType>()
                                        join st2 in _context.Set<SubsidiaryServiceType>() on st1.Id equals st2.ServiceTypeId
                                        where
                                        st2.SubsidiaryId == t1.Id
                                        select new ServiceTypeDto()
                                        {
                                            Id = st1.Id,
                                            Code = st1.Code,
                                            Description = st1.Description,
                                            CompanyId = st1.CompanyId,
                                            Status = st1.Status,
                                        }).ToList(),
                        GeoLocation = t1.GeoLocation,
                        Capacity = t1.Capacity,
                        Doctor = t7.Names + " " + t7.LastName,
                        CamoDoctor = t9.Names + " " + t9.LastName,
                        CamoDoctorId = t1.CamoDoctorId,
                        DoctorId = t1.DoctorId,
                        OfficeHours = CommonStatic.ConvertJsonToListOfficeHourDto(t1.OfficeHours),
                        CompanyId = t1.CompanyId,
                        Status = t1.Status,
                        EmailForAppointment = t1.EmailForAppointment,
                    });
        }
    }
}
