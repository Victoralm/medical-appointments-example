using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.GraphQL.Queries;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Appointment> GetAppointments([Service] PostgreContext context) => context.Appointments;
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Medic> GetMedics([Service] PostgreContext context) => context.Medics;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MedicalSpecialty> GetMedicalSpecialties([Service] PostgreContext context) => context.MedicalSpecialties;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Patient> GetPatients([Service] PostgreContext context) => context.Patients;
}
