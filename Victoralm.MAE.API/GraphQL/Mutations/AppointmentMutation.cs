using Victoralm.MAE.API.GraphQL.Mutations.Inputs;
using Victoralm.MAE.API.GraphQL.Mutations.Results;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<AppointmentResult> AddAppointment([Service] IUnitOfWork unitOfWork, AppointmentInput appointmentInput)
    {
        var id = Guid.NewGuid();
        AppointmentResult appointmentResult = new AppointmentResult()
        {
            Id = id,
            Schedule = appointmentInput.Schedule,
            PatientId = appointmentInput.PatientId,
            MedicId = appointmentInput.MedicId
        };

        Appointment appointment = new Appointment()
        {
            Id = appointmentResult.Id,
            Schedule = appointmentResult.Schedule,
            PatientId = appointmentResult.PatientId,
            MedicId = appointmentResult.MedicId
        };

        await unitOfWork.Appointments.Add(appointment);
        await unitOfWork.Appointments.SaveAsync();

        return appointmentResult;
    }
}
