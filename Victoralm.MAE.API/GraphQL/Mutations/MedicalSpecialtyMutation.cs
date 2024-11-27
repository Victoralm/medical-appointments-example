using Victoralm.MAE.API.GraphQL.Mutations.Inputs;
using Victoralm.MAE.API.GraphQL.Mutations.Results;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<MedicalSpecialtyResult> AddMedicalSpecialty([Service] IUnitOfWork unitOfWork, MedicalSpecialtyInput medicalSpecialtyInput)
    {
        var id = Guid.NewGuid();
        MedicalSpecialtyResult medicalSpecialtyResult = new MedicalSpecialtyResult()
        {
            Id = id,
            Name = medicalSpecialtyInput.Name
        };

        MedicalSpecialty medicalSpecialty = new MedicalSpecialty()
        {
            Id = medicalSpecialtyResult.Id,
            Name = medicalSpecialtyResult.Name
        };

        await unitOfWork.MedicalSpecialities.Add(medicalSpecialty);
        await unitOfWork.MedicalSpecialities.SaveAsync();

        return medicalSpecialtyResult;
    }

    public async Task<MedicalSpecialtyResult> UpdateMedicalSpecialtyt([Service] IUnitOfWork unitOfWork, Guid id, MedicalSpecialtyInput medicalSpecialtyInput)
    {
        MedicalSpecialty medicalSpecialty = await unitOfWork.MedicalSpecialities.GetById(id);

        if (medicalSpecialty == null) throw new GraphQLException(new Error("Medical Specialty not found", "MEDICALSPECIALTY_NOT_FOUND"));

        medicalSpecialty.Name = medicalSpecialtyInput.Name;

        await unitOfWork.MedicalSpecialities.Upsert(medicalSpecialty);

        MedicalSpecialtyResult medicalSpecialtyResult = new MedicalSpecialtyResult()
        {
            Id = medicalSpecialty.Id,
            Name = medicalSpecialty.Name
        };

        return medicalSpecialtyResult;
    }

    public async Task<MedicalSpecialtyResult> DeleteMedicalSpecialty([Service] IUnitOfWork unitOfWork, Guid id)
    {
        MedicalSpecialty medicalSpecialty = await unitOfWork.MedicalSpecialities.GetById(id);

        if (medicalSpecialty == null) throw new GraphQLException(new Error("Medical Specialty not found", "MEDICALSPECIALTY_NOT_FOUND"));

        MedicalSpecialtyResult medicalSpecialtyResult = new MedicalSpecialtyResult()
        {
            Id = medicalSpecialty.Id,
            Name = medicalSpecialty.Name
        };

        await unitOfWork.MedicalSpecialities.Delete(id);

        return medicalSpecialtyResult;
    }
}
