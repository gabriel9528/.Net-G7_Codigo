using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories
{
    public class TaxRepository : Repository<Tax>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;


        public TaxRepository(AnaPreventionContext context) : base(context)
        {
        }

        public Tax? GetDtoById(Guid id)
        {
            return _context.Set<Tax>().SingleOrDefault(x => x.Id == id);
        }
        public Tax? GetbyDescription(string description)
        {
            return _context.Set<Tax>().SingleOrDefault(x => x.Description == description );
        }
        public Tax? GetbyCode(string code)
        {
            return _context.Set<Tax>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid TaxId, string description)
        {
            return _context.Set<Tax>().Any(
                                                        c => c.Id != TaxId &&
                                                        c.Description == description 
                                                        );
        }

        public bool CodeTakenForEdit(Guid TaxId, string code)
        {
            return _context.Set<Tax>().Any(
                                                        c => c.Id != TaxId &&
                                                        c.Code == code
                                                        );
        }

        public List<Tax> GetListAll()
        {
            return _context.Set<Tax>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public List<Tax> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<Tax>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<Tax>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<Tax>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listTax = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<Tax>, PaginationMetadata>
                (listTax, paginationMetadata);
        }


    }
}

