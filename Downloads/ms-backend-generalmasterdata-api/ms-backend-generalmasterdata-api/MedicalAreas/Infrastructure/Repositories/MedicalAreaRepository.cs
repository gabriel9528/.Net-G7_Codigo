using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories
{
    public class MedicalAreaRepository : Repository<MedicalArea>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public MedicalAreaRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<MedicalArea>? GetByIds(List<Guid> ids)
        {
            return _context.Set<MedicalArea>().Where(t1 => ids.Contains(t1.Id)).ToList();
        }
        public MedicalArea? GetDtoById(Guid id)
        {
            return _context.Set<MedicalArea>().SingleOrDefault(x => x.Id == id);
        }
        public MedicalArea? GetbyDescription(string description)
        {
            return _context.Set<MedicalArea>().SingleOrDefault(x => x.Description == description);
        }
        public MedicalArea? GetbyCode(string code)
        {
            return _context.Set<MedicalArea>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid medicalArea, string description)
        {
            return _context.Set<MedicalArea>().Any(c => c.Id != medicalArea && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid medicalArea, string code)
        {
            return _context.Set<MedicalArea>().Any(c => c.Id != medicalArea && c.Code == code);
        }

        public List<MedicalArea> GetListAll()
        {
            return _context.Set<MedicalArea>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }
        public string GenerateCode()
        {

            var codeStrings = _context.Set<MedicalArea>()
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

            ;

            var codeMax = codes.Count != 0 ? codes.Max() : 0;

            var newCode = (codeMax + 1).ToString("D" + CommonStatic.numberZerosCode);

            return newCode;
        }

        public List<MedicalArea> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<MedicalArea>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return [.. query.OrderBy(t1 => t1.Description)];
        }
        public Tuple<IEnumerable<MedicalArea>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<MedicalArea>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listMedicalArea = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<MedicalArea>, PaginationMetadata>
                (listMedicalArea, paginationMetadata);
        }
    }
}
