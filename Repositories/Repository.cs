using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using webapp_travel_agency.Data;

namespace webapp_travel_agency.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet;

    protected Repository(ApplicationDbContext db)
    {
        _dbSet = db.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(string? includeRelations = null)
    {
        IQueryable<T> query = _dbSet;

        if (includeRelations is not null)
        {
            var relations = 
                includeRelations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var relation in relations)
            {
                query = query.Include(relation);
            }
        }
        
        return await query.ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeRelations = null)
    {
        IQueryable<T> query = _dbSet;
        
        if (includeRelations is not null)
        {
            var relations = 
                includeRelations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var relation in relations)
            {
                query = query.Include(relation);
            }
        }
        
        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _dbSet;
        return await query.AnyAsync(filter);
    }
}