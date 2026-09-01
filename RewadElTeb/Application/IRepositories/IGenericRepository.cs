using System.Linq.Expressions;

namespace Application.IRepositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(int id,CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(int id,Expression<Func<T, object>> include,CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, object>> include,CancellationToken cancellationToken = default);

        Task AddAsync(T entity,CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity,CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}