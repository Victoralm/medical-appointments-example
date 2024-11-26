using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.Repositories.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
}
