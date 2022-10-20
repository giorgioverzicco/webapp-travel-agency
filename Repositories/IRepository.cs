using System.Linq.Expressions;

namespace webapp_travel_agency.Repositories;

public interface IRepository<T> 
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string? includeRelations = null);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeRelations = null);
    Task AddAsync(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
}