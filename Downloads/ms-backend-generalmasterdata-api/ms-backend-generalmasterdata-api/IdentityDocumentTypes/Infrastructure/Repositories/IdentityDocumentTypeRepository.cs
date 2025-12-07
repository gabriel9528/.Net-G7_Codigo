using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories
{
    public class IdentityDocumentTypeRepository : Repository<IdentityDocumentType>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public IdentityDocumentTypeRepository(AnaPreventionContext context) : base(context)
        {
        }

        public IdentityDocumentType? GetDtoById(Guid id)
        {
            return _context.Set<IdentityDocumentType>().SingleOrDefault(x => x.Id == id);
        }
        public IdentityDocumentType? GetbyDescription(string description)
        {
            return _context.Set<IdentityDocumentType>().SingleOrDefault(x => x.Description == description);
        }
        public IdentityDocumentType? GetbyCode(string code)
        {
            return _context.Set<IdentityDocumentType>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid identityDocumentTypeId, string description)
        {
            return _context.Set<IdentityDocumentType>().Any(c => c.Id != identityDocumentTypeId && c.Description == description);
        }

        public bool CodeTakenForEdit(Guid identityDocumentTypeId, string code)
        {
            return _context.Set<IdentityDocumentType>().Any(c => c.Id != identityDocumentTypeId && c.Code == code);
        }

        public Tuple<IEnumerable<IdentityDocumentType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<IdentityDocumentType>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));
            if (!string.IsNullOrEmpty(abbreviationSearch))
                query = query = query.Where(t1 => t1.Abbreviation.Contains(abbreviationSearch));

            var listIdentityDocumentType = query.OrderBy(t1 => t1.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<IdentityDocumentType>, PaginationMetadata>
                (listIdentityDocumentType, paginationMetadata);
        }
        public List<IdentityDocumentType> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {

            var query = _context.Set<IdentityDocumentType>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            if (!string.IsNullOrEmpty(abbreviationSearch))
                query = query.Where(t1 => t1.Code.Contains(abbreviationSearch));
            return query.OrderBy(t1 => t1.Description).ToList();

        }

        public List<IdentityDocumentType> GetListAll()
        {
            return _context.Set<IdentityDocumentType>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public List<IdentityDocumentType> GetListOnlyPersonLegal()
        {
            return _context.Set<IdentityDocumentType>()
                    .Where(t1 => t1.Status && (t1.PersonType == PersonType.LEGAL_PERSON || t1.PersonType == PersonType.BOTH))
                    .OrderBy(t1 => t1.Description).ToList();
        }

        public List<IdentityDocumentType> GetListOnlyPersonNatural()
        {
            return _context.Set<IdentityDocumentType>()
                    .Where(t1 => t1.Status && (t1.PersonType == PersonType.NATURAL_PERSON || t1.PersonType == PersonType.BOTH))
                    .OrderBy(t1 => t1.Description).ToList();
        }
    }
}
