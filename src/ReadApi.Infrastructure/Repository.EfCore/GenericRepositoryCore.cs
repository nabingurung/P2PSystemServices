using ReadApi.Application.Interfaces.EfCore;
using ReadApi.Infrastructure.Peristence.EfCore;
using System.Linq.Expressions;

namespace ReadApi.Infrastructure.Repository.EfCore
{
    public class GenericRepositoryCore<T> : IGenericRepositoryCore<T> where T : class
    {
        protected readonly AppDbContext _context;
        public GenericRepositoryCore(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
           var n = _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
