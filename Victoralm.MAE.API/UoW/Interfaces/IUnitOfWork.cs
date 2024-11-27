using Victoralm.MAE.API.Repositories.Implementations;

namespace Victoralm.MAE.API.UoW.Interfaces;

public interface IUnitOfWork
{
    // Define the Specific Repositories
    MedicalSpecialityRepository MedicalSpecialities { get; }
    MedicRepository Medics { get; }
    PatientRepository Patients { get; }
    AppointmentRepository Appointments { get; }

    Task CompleteAsync();
}
