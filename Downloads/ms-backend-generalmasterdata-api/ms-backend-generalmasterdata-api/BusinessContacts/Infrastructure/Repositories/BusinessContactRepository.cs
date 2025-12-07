using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Infrastructure.Repositories
{
    public class BusinessContactRepository : Repository<BusinessContact>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public BusinessContactRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessContactDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id).SingleOrDefault();
        }


        public List<BusinessContactDto> GetListAll(Guid businessId)
        {
            return GetDtoQueryable().Where(t1 => t1.BusinessId == businessId && t1.Status).ToList();
        }

        public List<BusinessContactDto> GetListFilter(Guid businessId, bool status = true,
            string firstNameSearch = "", string lastNameSearch = "", string emailSearch = "")
        {

            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(firstNameSearch))
                query = query.Where(t1 => t1.FirstName.Contains(firstNameSearch));

            if (!string.IsNullOrEmpty(lastNameSearch))
                query = query.Where(t1 => t1.LastName.Contains(lastNameSearch));

            if (!string.IsNullOrEmpty(emailSearch))
                query = query.Where(t1 => t1.Email.Contains(emailSearch));

            return query.Where(t1 => t1.BusinessId == businessId && t1.Status == status).ToList();
        }
        public Tuple<IEnumerable<BusinessContactDto>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessId, bool status = true,
            string firstNameSearch = "", string lastNameSearch = "", string emailSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(firstNameSearch))
                query = query.Where(t1 => t1.FirstName.Contains(firstNameSearch));

            if (!string.IsNullOrEmpty(lastNameSearch))
                query = query.Where(t1 => t1.LastName.Contains(lastNameSearch));

            if (!string.IsNullOrEmpty(emailSearch))
                query = query.Where(t1 => t1.Email.Contains(emailSearch));

            query = query.Where(t1 => t1.Status == status && t1.BusinessId == businessId);

            var listBusinessContactDto = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessContactDto>, PaginationMetadata>
                (listBusinessContactDto, paginationMetadata);
        }
        private IQueryable<BusinessContactDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<BusinessContact>()
                    join t2 in _context.Set<Business>() on t1.BusinessId equals t2.Id
                    orderby t1.FirstName, t1.LastName
                    select new BusinessContactDto()
                    {
                        Id = t1.Id,
                        BusinessId = t1.BusinessId,
                        Status = t1.Status,
                        FirstName = t1.FirstName,
                        LastName = t1.LastName,
                        Position = t1.Position,
                        CellPhone = t1.CellPhone,
                        SecondCellPhone = t1.SecondCellPhone,
                        Phone = t1.Phone,
                        SecondPhone = t1.SecondPhone,
                        Email = t1.Email.Value,
                        Comment = t1.Comment,
                        Business = t2.Description
                    });
        }

    }
}
