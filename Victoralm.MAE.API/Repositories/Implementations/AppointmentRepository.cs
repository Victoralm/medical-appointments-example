using Microsoft.EntityFrameworkCore;
using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.Repositories.Interfaces;

namespace Victoralm.MAE.API.Repositories.Implementations;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

    public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetPatientsAsync function error", typeof(AppointmentRepository));
            return new List<Appointment>();
        }
    }

    public override async Task<bool> Upsert(Appointment entity)
    {
        try
        {
            var exist = await _dbSet.Where(x => x.Id == entity.Id)
                                               .FirstOrDefaultAsync();

            if (exist == null) return await Add(entity);

            exist.Schedule = entity.Schedule;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(AppointmentRepository));
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
            _logger.LogError(ex, "{Repo} Delete function error", typeof(AppointmentRepository));
            return false;
        }
    }
}
