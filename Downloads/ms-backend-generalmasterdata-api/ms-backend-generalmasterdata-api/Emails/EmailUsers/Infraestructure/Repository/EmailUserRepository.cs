using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Infrastructure.Repositories
{
    public class EmailUserRepository : Repository<EmailUser>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public EmailUserRepository(AnaPreventionContext context) : base(context)
        {
        }
        public EmailUser? GetbyName(string name)
        {
            return _context.Set<EmailUser>().SingleOrDefault(x => x.Name == name);
        }
        public EmailUser? GetbyEmail(Email email)
        {
            return _context.Set<EmailUser>().SingleOrDefault(x => x.Email == email);
        }
        public bool NameTakenForEdit(Guid id, string name)
        {
            return _context.Set<EmailUser>().Any(c => c.Id != id && c.Name == name);
        }
        public bool EmailTakenForEdit(Guid id, Email email)
        {
            return _context.Set<EmailUser>().Any(c => c.Id != id && c.Email == email);
        }
        public List<EmailUserDto> GetListAll()
        {
            return GetDtoQueryable().Where(t1 => t1.Status).OrderBy(t1 => t1.Name).ToList();
        }

        public EmailUserDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }

        public List<EmailUserDto> GetListFilter(bool status = true, string emailSearch = "", string nameSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(emailSearch))
                query.Where(t1 => t1.Email.Contains(emailSearch));

            if (!string.IsNullOrEmpty(nameSearch))
                query.Where(t1 => t1.Name.Contains(nameSearch));

            return query.OrderBy(t1 => t1.Name).ToList();
        }

        public Tuple<IEnumerable<EmailUserDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string? nameSearch = "", string? emailSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(nameSearch))
                query.Where(t1 => t1.Name.Contains(nameSearch));

            if (!string.IsNullOrEmpty(emailSearch))
                query.Where(t1 => t1.Email.Contains(emailSearch));

            var ListEmailUser = query.OrderBy(t1 => t1.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EmailUserDto>, PaginationMetadata>
                (ListEmailUser, paginationMetadata);
        }

        private IQueryable<EmailUserDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<EmailUser>()
                    where
                         t1.Status
                    select new EmailUserDto()
                    {
                        Id = t1.Id,
                        Email = t1.Email.Value,
                        Name = t1.Name,
                        Status = t1.Status,
                        ProtocolType = t1.ProtocolType,
                        Host = t1.Host,
                        Password = t1.Password,
                        Port = t1.Port,
                    });
        }
    }
}
