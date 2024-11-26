using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.Repositories.Interfaces;

public interface IMedicRepository : IGenericRepository<MedicalSpecialty>
{
    Task<IEnumerable<Medic>> GetMedicsAsync();
    Task<IEnumerable<Medic>> GetMedicsBySpecialities(List<Guid> ids);
}
