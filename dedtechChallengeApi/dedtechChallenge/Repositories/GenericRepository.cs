using DedtechChallenge.Data;
using DedtechChallenge.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DedtechChallenge.Repositories
{
    public class GenericRepository<T, Pk> : IGenericRepository<T, Pk> where T : class
    {
        protected readonly DedtechChallengeContext _context;
        public GenericRepository(DedtechChallengeContext context)
        {
            _context = context;
        }

        public Task<List<T>> GetAll()
        {
            return _context.Set<T>().ToListAsync();
        }

        public Task<List<T>> GetAllAndSortBy(Expression<Func<T, object>> sortBy, string order = "asc")
        {
            var query = _context.Set<T>();
            if (order.ToLower().Equals("desc"))
            {
                return query.OrderByDescending(sortBy).ToListAsync();

            }

            return query.OrderBy(sortBy).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Pk id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<List<T>> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<EntityEntry> SaveAsync(T entity)
        {
            return await _context.Set<T>().AddAsync(entity);
        }

        public EntityEntry Update(T entity)
        {
            return _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }
    }
}
