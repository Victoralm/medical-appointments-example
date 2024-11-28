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

    public async Task<MedicResult> UpdateMedic([Service] IUnitOfWork unitOfWork, Guid id, MedicInput medicInput)
    {
        Medic medic = await unitOfWork.Medics.GetById(id);

        if (medic == null) throw new GraphQLException(new Error("Medic not found", "MEDIC_NOT_FOUND"));

        medic.Name = medicInput.Name;
        medic.Address = medicInput.Address;
        medic.Phone = medicInput.Phone;
        medic.Email = medicInput.Email;
        medic.MedicalSpecialtyId = medicInput.MedicalSpecialtyId;

        await unitOfWork.Medics.Upsert(medic);

        MedicResult medicResult = new MedicResult()
        {
            Id = medic.Id,
            Name = medic.Name,
            Address = medic.Address,
            Phone = medic.Phone,
            Email = medic.Email,
            MedicalSpecialtyId = medic.MedicalSpecialtyId
        };

        return medicResult;
    }

    public async Task<MedicResult> DeleteMedic([Service] IUnitOfWork unitOfWork, Guid id)
    {
        Medic medic = await unitOfWork.Medics.GetById(id);

        if (medic == null) throw new GraphQLException(new Error("Medic not found", "MEDIC_NOT_FOUND"));

        MedicResult medicResult = new MedicResult()
        {
            Id = medic.Id,
            Name = medic.Name,
            Address = medic.Address,
            Phone = medic.Phone,
            Email = medic.Email,
            MedicalSpecialtyId = medic.MedicalSpecialtyId
        };

        await unitOfWork.MedicalSpecialities.Delete(id);

        return medicResult;
    }
}
