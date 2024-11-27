using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.Repositories.Interfaces;

public interface IMedicalSpecialityRepository : IGenericRepository<MedicalSpecialty>
{
    Task<IEnumerable<MedicalSpecialty>> GetMedicalSpecialtiesAsync();
}
