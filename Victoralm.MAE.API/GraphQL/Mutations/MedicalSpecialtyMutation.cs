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
}
