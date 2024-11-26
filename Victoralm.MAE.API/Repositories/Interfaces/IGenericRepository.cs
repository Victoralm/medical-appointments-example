namespace Victoralm.MAE.API.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<int> SaveAsync(CancellationToken cancellationToken);
}
