using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Infrastructure.Repositories
{
    public class ItemTypeRepository : Repository<ItemType>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public ItemTypeRepository(AnaPreventionContext context) : base(context)
        {
        }

        public ItemType? GetDtoById(Guid id)
        {
            return _context.Set<ItemType>().SingleOrDefault(x => x.Id == id);
        }
        public ItemType? GetbyDescription(string description)
        {
            return _context.Set<ItemType>().SingleOrDefault(x => x.Description == description);
        }
        public ItemType? GetbyCode(string code)
        {
            return _context.Set<ItemType>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid itemType, string description)
        {
            return _context.Set<ItemType>().Any(c => c.Id != itemType && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid itemType, string code)
        {
            return _context.Set<ItemType>().Any(c => c.Id != itemType && c.Code == code);
        }

        public List<ItemType> GetListAll()
        {
            return _context.Set<ItemType>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<ItemType>()
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
        public List<ItemType> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<ItemType>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<ItemType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<ItemType>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listIdentityDocumentType = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<ItemType>, PaginationMetadata>
                (listIdentityDocumentType, paginationMetadata);
        }
    }
}
