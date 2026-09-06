using Application.IRepositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(
                new object[] { id },
                cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(
                entity,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task DeleteAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task<T?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(
                e => EF.Property<int>(e, "Id") == id,
                cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(CancellationToken cancellationToken = default,params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync(
                cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id,Expression<Func<T, object>> include,CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            query = query.Include(include);

            return await query.FirstOrDefaultAsync(
                e => EF.Property<int>(e, "Id") == id,
                cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, object>> include,CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            query = query.Include(include);

            return await query.ToListAsync(cancellationToken);
        }
        public async Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AnyAsync(predicate, cancellationToken);
        }

        
    }
}