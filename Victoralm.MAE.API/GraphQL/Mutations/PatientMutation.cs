using Victoralm.MAE.API.GraphQL.Mutations.Inputs;
using Victoralm.MAE.API.GraphQL.Mutations.Results;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<PatientResult> AddPatient([Service] IUnitOfWork unitOfWork, PatientInput patientInput)
    {
        var id = Guid.NewGuid();
        PatientResult patientResult = new PatientResult()
        {
            Id = id,
            Name = patientInput.Name,
            Address = patientInput.Address,
            Phone = patientInput.Phone,
            Email = patientInput.Email
        };

        Patient patient = new Patient()
        {
            Id = patientResult.Id,
            Name = patientResult.Name,
            Address = patientResult.Address,
            Phone = patientResult.Phone,
            Email = patientResult.Email
        };

        await unitOfWork.Patients.Add(patient);
        await unitOfWork.Patients.SaveAsync();

        return patientResult;
    }
}
