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

    public async Task<AppointmentResult> UpdateAppointment([Service] IUnitOfWork unitOfWork, Guid id, AppointmentInput appointmentInput)
    {
        Appointment appointment = await unitOfWork.Appointments.GetById(id);

        if (appointment == null) throw new GraphQLException(new Error("Appointment not found", "APPOINTMENT_NOT_FOUND"));

        appointment.Schedule = appointmentInput.Schedule;
        appointment.PatientId = appointmentInput.PatientId;
        appointment.MedicId = appointmentInput.MedicId;

        await unitOfWork.Appointments.Upsert(appointment);

        AppointmentResult appointmentResult = new AppointmentResult()
        {
            Id = appointment.Id,
            Schedule = appointment.Schedule,
            PatientId = appointment.PatientId,
            MedicId = appointment.MedicId
        };

        return appointmentResult;
    }

    public async Task<AppointmentResult> DeleteAppointment([Service] IUnitOfWork unitOfWork, Guid id)
    {
        Appointment appointment = await unitOfWork.Appointments.GetById(id);

        if (appointment == null) throw new GraphQLException(new Error("Appointment not found", "APPOINTMENT_NOT_FOUND"));

        AppointmentResult appointmentResult = new AppointmentResult()
        {
            Id = appointment.Id,
            Schedule = appointment.Schedule,
            PatientId = appointment.PatientId,
            MedicId = appointment.MedicId
        };

        await unitOfWork.Appointments.Delete(id);

        return appointmentResult;
    }
}
