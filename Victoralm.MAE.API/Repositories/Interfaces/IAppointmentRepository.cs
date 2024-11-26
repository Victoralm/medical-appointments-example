using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.Repositories.Interfaces;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAppointmentsAsync();
}
