using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories
{
    public class BusinessRepository : Repository<Business>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public BusinessRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id).SingleOrDefault();
        }
        public Business? GetbyDescription(string description)
        {
            return _context.Set<Business>().SingleOrDefault(x => x.Description == description);
        }
        public Business? GetbyDocumentNumber(string documentNumber, Guid identityDocumentTypeId)
        {
            return _context.Set<Business>().SingleOrDefault(x => x.DocumentNumber == documentNumber && x.IdentityDocumentTypeId == identityDocumentTypeId);
        }
        public bool DescriptionTakenForEdit(Guid businessId, string description)
        {
            return _context.Set<Business>().Any(c => c.Id != businessId && c.Description == description);
        }
        public bool DocumentNumberTakenForEdit(Guid businessId, string documentNumber, Guid identityDocumentTypeId)
        {
            return _context.Set<Business>().Any(c => c.Id != businessId && c.DocumentNumber == documentNumber && c.IdentityDocumentTypeId == identityDocumentTypeId);
        }

        public List<BusinessDto> GetListAll(string? documentNumberAndDescription)
        {
            return GetDtoQueryable().Where(t1 => t1.Status && t1.DocumentNumberAndDescription.Contains(documentNumberAndDescription??"")).ToList();

        }

        public List<Business> GetListAll()
        {
            return _context.Set<Business>().Where(t1 => t1.Status).ToList();

        }

      
        public List<BusinessDto> GetListFilter(bool status = true, string descriptionSearch = "", string documentNumberSearch = "")
        {

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));

            if (!string.IsNullOrEmpty(documentNumberSearch))
                query = query.Where(t1 => t1.DocumentNumber.Contains(documentNumberSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<BusinessDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string documentNumberSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;


            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));

            if (!string.IsNullOrEmpty(documentNumberSearch))
                query = query.Where(t1 => t1.DocumentNumber.Contains(documentNumberSearch));


            var listBusinessDto = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessDto>, PaginationMetadata>
                (listBusinessDto, paginationMetadata);
        }

        private IQueryable<BusinessDto> GetDtoQueryable()
        {

            return (from t1 in _context.Set<Business>()
                    join t2 in _context.Set<District>() on t1.DistrictId equals t2.Id
                    join t3 in _context.Set<Province>() on t2.ProvinceId equals t3.Id
                    join t4 in _context.Set<Department>() on t3.DepartmentId equals t4.Id
                    join t5 in _context.Set<CreditTime>() on t1.CreditTimeId equals t5.Id
                    join medicalFormat in _context.Set<MedicalFormat>() on t1.MedicalFormatId equals medicalFormat.Id
                    join t7 in _context.Set<IdentityDocumentType>() on t1.IdentityDocumentTypeId equals t7.Id
                    select new BusinessDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Tradename = t1.Tradename,
                        Address = t1.Address,
                        MedicalFormatType = medicalFormat.MedicalFormatType,
                        IdentityDocumentTypeId = t1.IdentityDocumentTypeId,
                        IdentityDocumentType = t7.Description,
                        MedicalFormatId = t1.MedicalFormatId,
                        MedicalFormat = medicalFormat.Description,
                        CreditTimeId = t1.CreditTimeId,
                        CreditTimeNumberDay = _context.Set<CreditTime>().Where(x => x.Id == t1.CreditTimeId).Select(t1 => t1.NumberDay).FirstOrDefault(),
                        CreditTime = t5.Description,
                        DistrictId = t1.DistrictId,
                        District = t2.Description,
                        Province = t3.Description,
                        Department = t4.Description,
                        DocumentNumber = t1.DocumentNumber,
                        Comment = t1.Comment,
                        DocumentNumberAndDescription = t1.DocumentNumber + " - " + t1.Description,
                        DateInscription = t1.DateInscription,
                       // ListAttachments = (_context.Set<Attachment>().Where(st1 => st1.EntityId == t1.Id && st1.EntityType == EntityType.BUSINESS)).ToList(),
                        ListEconomicActivities = ((from tt1 in _context.Set<EconomicActivity>()
                                                   join tt2 in _context.Set<BusinessEconomicActivity>() on tt1.Id equals tt2.EconomicActivityId
                                                   where tt2.BusinessId == t1.Id
                                                   select new EconomicActivityDto()
                                                   {
                                                       Id = tt1.Id,
                                                       Description = tt1.Description,
                                                       Code = tt1.Code,
                                                       Status = tt1.Status,
                                                   }
                     ).ToList()),
                        IsActive = t1.IsActive,
                        IsWaybillShipping = t1.IsWaybillShipping,
                        IsSendingResultsPatients = t1.IsSendingResultsPatients,
                        IsPatientResults = t1.IsPatientResults,
                        IsGenerateUsers = t1.IsGenerateUsers,
                        IsMedicalReportDisplay = t1.IsMedicalReportDisplay,
                        ExceptionsByMainBusiness = CommonStatic.ConvertJsonToListGuid(t1.ExceptionsByMainBusinessJson),
                        Status = t1.Status,

                    });
        }
    }
}
