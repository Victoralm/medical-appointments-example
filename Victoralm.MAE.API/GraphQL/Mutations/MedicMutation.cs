using Victoralm.MAE.API.GraphQL.Mutations.Inputs;
using Victoralm.MAE.API.GraphQL.Mutations.Results;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<MedicResult> AddMedic([Service] IUnitOfWork unitOfWork, MedicInput medicInput)
    {
        var id = Guid.NewGuid();
        MedicResult medicResult = new MedicResult()
        {
            Id = id,
            Name = medicInput.Name,
            Address = medicInput.Address,
            Phone = medicInput.Phone,
            Email = medicInput.Email
        };

        Medic medic = new Medic()
        {
            Id = medicResult.Id,
            Name = medicResult.Name,
            Address = medicResult.Address,
            Phone = medicResult.Phone,
            Email = medicResult.Email
        };

        await unitOfWork.Medics.Add(medic);
        await unitOfWork.Medics.SaveAsync();

        return medicResult;
    }
}
