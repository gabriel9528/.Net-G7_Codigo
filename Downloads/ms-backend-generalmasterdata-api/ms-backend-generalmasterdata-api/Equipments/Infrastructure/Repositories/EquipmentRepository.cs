using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Infrastructure.Repositories
{
    public class EquipmentRepository(AnaPreventionContext context) : Repository<Equipment>(context)
    {
        const int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public EquipmentDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public Equipment? GetbyDescription(string description)
        {
            return _context.Set<Equipment>().SingleOrDefault(t1 => t1.Description == description);
        }

        public bool DescriptionTakenForEdit(Guid Id, string description)
        {
            return _context.Set<Equipment>().Any(t1 => t1.Id != Id && t1.Description == description);
        }

        public List<EquipmentDto> GetListByMedicalAreaTypeAndSubsidiaryId(Guid subsidiaryId,MedicalAreaType medicalAreaType)
        {
            return GetDtoQueryable().Where(t1 => t1.Status && t1.SubsidiaryId == subsidiaryId && t1.MedicalAreaType == medicalAreaType ).OrderBy(t1 => t1.Description).ToList();
        }

        public List<EquipmentDto> GetListAll(Guid companyId)
        {
            return GetDtoQueryable().Where(t1 => t1.Status && t1.CompanyId == companyId).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode(Guid companyId)
        {
            var codeStrings = _context.Set<Equipment>().Where(t1 => t1.CompanyId == companyId)
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
        public List<EquipmentDto> GetListFilter(bool status, string? descriptionSearch, Guid companyId)
        {

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));


            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<EquipmentDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string? descriptionSearch, Guid companyId)
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));



            var ListEquipment = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EquipmentDto>, PaginationMetadata>
                (ListEquipment, paginationMetadata);
        }
        public Tuple<IEnumerable<EquipmentDto>, PaginationMetadata> GetListBySubsidiary(int pageNumber, int pageSize, bool status, Guid companyId, Guid SubsidiaryId)
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (SubsidiaryId != Guid.Empty)
                query = query.Where(t1 => t1.SubsidiaryId == SubsidiaryId);



            var ListEquipment = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EquipmentDto>, PaginationMetadata>
                (ListEquipment, paginationMetadata);
        }
        private IQueryable<EquipmentDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Equipment>()
                    join t2 in _context.Set<Person>() on t1.PersonDeviceManagerId equals t2.Id
                    join t3 in _context.Set<MedicalArea>() on t1.MedicalAreaId equals t3.Id
                    join t4 in _context.Set<Subsidiary>() on t1.SubsidiaryId equals t4.Id
                    select new EquipmentDto()
                    {
                        Id = t1.Id,
                        CompanyId = t1.CompanyId,
                        Description = t1.Description,
                        Brand = t1.Brand,
                        Model = t1.Model,
                        SerialNumber = t1.SerialNumber,
                        MedicalAreaId = t1.MedicalAreaId,
                        MedicalAreaType = t3.MedicalAreaType,
                        SubsidiaryId = t1.SubsidiaryId,
                        MedicalArea = t3.Description,
                        Subsidiary = t4.Description,
                        PersonDeviceManagerId = t1.PersonDeviceManagerId,
                        PersonDeviceManager = t2.Names + " " + t2.LastName,
                        Supplier = t1.Supplier,
                        DatecalibrationFormat = _context.Set<EquipmentCalibration>().Max(dto => dto.Datecalibration),
                        Datecalibration = _context.Set<EquipmentCalibration>().Max(dto => dto.Datecalibration).ToString(CommonStatic.FormatDate),
                        EquipmentCalibrations = (from st1 in _context.Set<EquipmentCalibration>()
                                                 where st1.EquipmentId == t1.Id
                                                 orderby st1.Datecalibration descending
                                                 select new EquipmentCalibrationDto()
                                                 {
                                                     Datecalibration = st1.Datecalibration.ToString(CommonStatic.FormatDate),
                                                     NextDatecalibration = st1.NextDatecalibration.ToString(CommonStatic.FormatDate)
                                                 }).ToList(),
                        Attachments = _context.Set<Attachment>().Where(st1 => st1.EntityId == t1.Id && st1.EntityType == EntityType.EQUIPMENT).ToList(),
                        Status = t1.Status,
                    }); 
        }
    }
}
