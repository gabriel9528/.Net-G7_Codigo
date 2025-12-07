using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories
{
    public class SubsidiaryTypeRepository(AnaPreventionContext context) : Repository<SubsidiaryType>(context)
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public SubsidiaryType? GetDtoById(Guid id)
        {
            return _context.Set<SubsidiaryType>().SingleOrDefault(t1 => t1.Id == id );
        }
        public SubsidiaryType? GetbyDescription(string description)
        {
            return _context.Set<SubsidiaryType>().SingleOrDefault(t1 => t1.Description == description );
        }
        public SubsidiaryType? GetbyCode(string code)
        {
            return _context.Set<SubsidiaryType>().SingleOrDefault(t1 => t1.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid SubsidiaryTypeId, string description)
        {
            return _context.Set<SubsidiaryType>().Any(
                                                        t1 => t1.Id != SubsidiaryTypeId && 
                                                        t1.Description == description
                                                        );
        }

        public bool CodeTakenForEdit(Guid SubsidiaryTypeId, string code)
        {
            return _context.Set<SubsidiaryType>().Any(
                                                        t1 => t1.Id != SubsidiaryTypeId &&
                                                        t1.Code == code && t1.Status
                                                        );
        }

        public List<SubsidiaryType> GetListAll()
        {
            return _context.Set<SubsidiaryType>().Where(t1 => t1.Status ).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<SubsidiaryType>()
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
        public List<SubsidiaryType> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<SubsidiaryType>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<SubsidiaryType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "",string codeSearch= "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<SubsidiaryType>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listSubsidiaryType = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<SubsidiaryType>, PaginationMetadata>
                (listSubsidiaryType, paginationMetadata);
        }


    }
}
