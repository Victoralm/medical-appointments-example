using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.GraphQL.Queries;

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
