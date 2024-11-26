using Microsoft.EntityFrameworkCore;
using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.Repositories.Interfaces;

namespace Victoralm.MAE.API.Repositories.Implementations;

public class MedicRepository : GenericRepository<Medic>, IMedicRepository
{
    public MedicRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

    public async Task<IEnumerable<Medic>> GetMedicsAsync()
    {
        try
        {
            return await _dbSet.OrderBy(i => i.Name).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetMedicsAsync function error", typeof(MedicRepository));
            return new List<Medic>();
        }
    }

    public async Task<IEnumerable<Medic>> GetMedicsBySpecialities(List<Guid> ids)
    {
        try
        {
            return await _dbSet.Where(x => x.MedicalSpecialtyId.Intersect(ids).Any()).OrderBy(i => i.Name).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetMedicsBySpecialities function error", typeof(MedicRepository));
            return new List<Medic>();
        }
    }

    public override async Task<bool> Upsert(Medic entity)
    {
        try
        {
            var exist = await _dbSet.Where(x => x.Id == entity.Id)
                                               .FirstOrDefaultAsync();

            if (exist == null) return await Add(entity);

            exist.Name = entity.Name;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(MedicRepository));
            return false;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var exist = await _dbSet.Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();

            if (exist == null) return false;

            _dbSet.Remove(exist);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete function error", typeof(MedicRepository));
            return false;
        }
    }
}
