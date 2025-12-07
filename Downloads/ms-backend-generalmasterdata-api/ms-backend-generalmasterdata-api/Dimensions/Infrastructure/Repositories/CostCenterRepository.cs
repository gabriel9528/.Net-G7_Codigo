using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories
{
    public class CostCenterRepository : Repository<CostCenter>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;


        public CostCenterRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<CostCenter>? GetDtoByDimensionId(Guid dimensionId)
        {
            return _context.Set<CostCenter>().Where(x => x.DimensionId == dimensionId).ToList();
        }

        public CostCenter? GetbyDescription(string description)
        {
            return _context.Set<CostCenter>().SingleOrDefault(x => x.Description == description);
        }
        public CostCenter? GetbyCode(string code)
        {
            return _context.Set<CostCenter>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid CostCenterId, string description)
        {
            return _context.Set<CostCenter>().Any(
                                                        c => c.Id != CostCenterId &&
                                                        c.Description == description
                                                        );
        }
        public bool CodeTakenForEdit(Guid CostCenterId, string code)
        {
            return _context.Set<CostCenter>().Any(
                                                        c => c.Id != CostCenterId &&
                                                        c.Code == code
                                                        );
        }

        public List<CostCenter> GetListFilter(Guid dimensionId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {

            var query = _context.Set<CostCenter>().Where(t1 => t1.Status == status && t1.DimensionId == dimensionId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<CostCenter>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid dimensionId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;


            var query = _context.Set<CostCenter>().Where(t1 => t1.Status == status && t1.DimensionId == dimensionId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listCostCenter = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<CostCenter>, PaginationMetadata>
                (listCostCenter, paginationMetadata);
        }


    }
}

