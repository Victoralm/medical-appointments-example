using Microsoft.EntityFrameworkCore;
using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Repositories.Interfaces;

namespace Victoralm.MAE.API.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly PostgreContext _context;
    protected readonly DbSet<T> _dbSet;
    public readonly ILogger _logger;

    public GenericRepository(PostgreContext context, ILogger logger)
    {
        _context = context;

        //Whatever Entity name we specify while creating the instance of GenericRepository
        //That Entity name  will be stored in the DbSet<T> variable
        _dbSet = context.Set<T>();

        _logger = logger;
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        
        return true;
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
