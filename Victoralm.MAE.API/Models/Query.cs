using Victoralm.MAE.API.Context;

namespace Victoralm.MAE.API.Models;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Medic> GetMedics([Service] PostgreContext context) => context.Medics;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Patient> GetPatients([Service] PostgreContext context) => context.Patients;

    public string Teste => "Só um texto de teste";
}
