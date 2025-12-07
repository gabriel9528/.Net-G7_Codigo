using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF
{
    public class Repository<T> where T : class
    {
        protected readonly AnaPreventionContext _context;

        protected Repository(AnaPreventionContext context)
        {
            _context = context;
        }

        public virtual T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual void Save(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public virtual void SaveAsync(T entity)
        {
            _context.Set<T>().AddAsync(entity);
        }
        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
