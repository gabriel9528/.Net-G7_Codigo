using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalForms.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories
{
    public class MedicalFormRepository : Repository<MedicalForm>
    {
        const int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public MedicalFormRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<MedicalForm>? GetByListIds(List<Guid> ids)
        {
            return _context.Set<MedicalForm>().Where(t1 => ids.Contains(t1.Id)).ToList();
        }

        public MedicalFormDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id).SingleOrDefault();
        }
        public MedicalForm? GetbyDescription(string description)
        {
            return _context.Set<MedicalForm>().SingleOrDefault(t1 => t1.Description == description);
        }

        public bool DescriptionTakenForEdit(Guid medicalFormId, string description)
        {
            return _context.Set<MedicalForm>().Any(t1 => t1.Id != medicalFormId && t1.Description == description);
        }

        public MedicalForm? GetbyMedicalFormsType(MedicalFormsType medicalFormsType)
        {
            return _context.Set<MedicalForm>().FirstOrDefault(t1 => t1.MedicalFormsType == medicalFormsType);
        }


        public List<MedicalForm> GetListAll()
        {
            return _context.Set<MedicalForm>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public List<MedicalFormDto> GetListFilter(bool status = true, string descriptionSearch = "", string serviceTypeSearch = "", string medicalAreaSearch = "")
        {

            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(serviceTypeSearch))
                query = query.Where(t1 => t1.ServiceType.Contains(serviceTypeSearch));

            if (!string.IsNullOrEmpty(medicalAreaSearch))
                query = query.Where(t1 => t1.MedicalArea.Contains(medicalAreaSearch));

            return query.Where(t1 => t1.Status == status).ToList();

        }

        public Tuple<IEnumerable<MedicalFormDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string serviceTypeSearch = "", string medicalAreaSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(serviceTypeSearch))
                query = query.Where(t1 => t1.ServiceType.Contains(serviceTypeSearch));

            if (!string.IsNullOrEmpty(medicalAreaSearch))
                query = query.Where(t1 => t1.MedicalArea.Contains(medicalAreaSearch));




            var medicalFormDto = query.Where(t1 => t1.Status == status).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<MedicalFormDto>, PaginationMetadata>
                (medicalFormDto, paginationMetadata);
        }

        public List<MedicalForm>? GetListMedicalFormByServiceCatalogId(Guid serviceCatalogId)
        {
            return (from t1 in _context.Set<MedicalForm>()
                    join t2 in _context.Set<ServiceCatalogMedicalForm>() on t1.Id equals t2.MedicalFormId
                    where t2.ServiceCatalogId == serviceCatalogId
                    orderby t1.Description
                    select t1
                    ).ToList();

        }
        private IQueryable<MedicalFormDto> GetDtoQueryable()
        {

            return (from t1 in _context.Set<MedicalForm>()
                    join t2 in _context.Set<ServiceType>() on t1.ServiceTypeId equals t2.Id
                    join t3 in _context.Set<MedicalArea>() on t1.MedicalAreaId equals t3.Id
                    orderby t1.Description
                    select new MedicalFormDto()
                    {
                        Id = t1.Id,
                        Code = t1.Code,
                        Description = t1.Description,
                        ServiceTypeId = t1.ServiceTypeId,
                        ServiceType = t2.Description,
                        MedicalAreaId = t1.MedicalAreaId,
                        MedicalArea = t3.Description,
                        MedicalFormsType = t1.MedicalFormsType,
                        Status = t1.Status,

                    });
        }
        public List<MedicalFormByPrintDto>? GetMedicalFormByPrintDto()
        {
            var response = (from medicalForm in _context.Set<MedicalForm>()
                            group medicalForm by new { medicalForm.Code, medicalForm.IconDescription }
                            into medicalFormGroup
                            select new MedicalFormByPrintDto()
                            {
                                Description = medicalFormGroup.Key.IconDescription,
                                MedicalFormsTypes = medicalFormGroup.ToList().Select(t1 => t1.MedicalFormsType).ToList(),
                            }).ToList();
            var fileTypeNames = MedicalFormStatic.GetOrderFileTypeNames();

            foreach (var fileType in fileTypeNames)
            {
                response.Add(new()
                {
                    Description = fileType.Value,
                    OrderFileType = new() { fileType.Key }
                });
            }

            return response;


        }
    }
}
