using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Repositories.Implementations;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.UoW.Implementations;

public class UnitOfWork : IUnitOfWork
{

    private readonly PostgreContext _context;
    private readonly ILogger _logger;

    public MedicalSpecialityRepository MedicalSpecialities { get; private set; }
    public MedicRepository Medics { get; private set; }
    public PatientRepository Patients { get; private set; }
    public AppointmentRepository Appointments { get; private set; }

    public UnitOfWork(PostgreContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        MedicalSpecialities = new MedicalSpecialityRepository(context, _logger);
        Medics = new MedicRepository(context, _logger);
        Patients = new PatientRepository(context, _logger);
        Appointments = new AppointmentRepository(context, _logger);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}
