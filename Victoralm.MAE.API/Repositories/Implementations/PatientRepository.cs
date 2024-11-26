﻿using Microsoft.EntityFrameworkCore;
using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.Repositories.Interfaces;

namespace Victoralm.MAE.API.Repositories.Implementations;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        try
        {
            return await _dbSet.OrderBy(i => i.Name).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetPatientsAsync function error", typeof(PatientRepository));
            return new List<Patient>();
        }
    }

    public override async Task<bool> Upsert(Patient entity)
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
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(PatientRepository));
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
            _logger.LogError(ex, "{Repo} Delete function error", typeof(PatientRepository));
            return false;
        }
    }
}