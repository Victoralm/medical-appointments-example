using Victoralm.MAE.API.Context;

namespace Victoralm.MAE.API.Models;

public class Query
{
    //[UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Medic> GetMedics([Service] PostgreContext context) => context.Medics;

    public string Teste => "Só um texto de teste";
}
