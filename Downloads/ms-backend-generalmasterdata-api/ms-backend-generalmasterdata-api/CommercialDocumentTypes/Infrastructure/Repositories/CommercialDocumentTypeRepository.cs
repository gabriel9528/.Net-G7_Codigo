using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Infrastructure.Repositories
{
    public class CommercialDocumentTypeRepository :Repository<CommercialDocumentType>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public CommercialDocumentTypeRepository(AnaPreventionContext context) : base(context)
        {

        }

        public CommercialDocumentType? GetDtoById(Guid id)
        {
            return _context.Set<CommercialDocumentType>().SingleOrDefault(x => x.Id == id);
        }
        public CommercialDocumentType? GetbyDescription(string description)
        {
            return _context.Set<CommercialDocumentType>().SingleOrDefault(x => x.Description == description);
        }
        public CommercialDocumentType? GetbyCode(string code)
        {
            return _context.Set<CommercialDocumentType>().SingleOrDefault(x => x.Code == code);
        }
        public CommercialDocumentType? GetbyAbbreviation(string abbreviation)
        {
            return _context.Set<CommercialDocumentType>().SingleOrDefault(x => x.Abbreviation == abbreviation);
        }
        public bool DescriptionTakenForEdit(Guid commercialDocumentTypeId, string description)
        {
            return _context.Set<CommercialDocumentType>().Any(c => c.Id != commercialDocumentTypeId && c.Description == description);
        }

        public bool CodeTakenForEdit(Guid commercialDocumentTypeId, string code)
        {
            return _context.Set<CommercialDocumentType>().Any(c => c.Id != commercialDocumentTypeId && c.Code == code);
        }

        public bool AbbreviationTakenForEdit(Guid commercialDocumentTypeId, string abbreviation)
        {
            return _context.Set<CommercialDocumentType>().Any(c => c.Id != commercialDocumentTypeId && c.Abbreviation == abbreviation);
        }

        public List<CommercialDocumentType> GetListAll()
        {
            return _context.Set<CommercialDocumentType>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }
        public List<CommercialDocumentType> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            var query = _context.Set<CommercialDocumentType>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            if (!string.IsNullOrEmpty(abbreviationSearch))
                query = query.Where(t1 => t1.Abbreviation.Contains(abbreviationSearch));
            return query.OrderBy(t1 => t1.Description).ToList();

        }
        public Tuple<IEnumerable<CommercialDocumentType>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;


            var query = _context.Set<CommercialDocumentType>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            if (!string.IsNullOrEmpty(abbreviationSearch))
                query = query.Where(t1 => t1.Abbreviation.Contains(abbreviationSearch));

            var listCommercialDocumentType = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();
            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<CommercialDocumentType>, PaginationMetadata>
                (listCommercialDocumentType, paginationMetadata);
        }

    }
}
