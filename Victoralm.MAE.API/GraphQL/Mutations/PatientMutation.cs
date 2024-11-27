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

    public async Task<PatientResult> UpdatePatient([Service] IUnitOfWork unitOfWork, Guid id, PatientInput patientInput)
    {
        Patient patient = await unitOfWork.Patients.GetById(id);

        if (patient == null) throw new GraphQLException(new Error("Patient not found", "PATIENT_NOT_FOUND"));

        patient.Name = patientInput.Name;
        patient.Address = patientInput.Address;
        patient.Phone = patientInput.Phone;
        patient.Email = patientInput.Email;

        await unitOfWork.Patients.Upsert(patient);

        PatientResult patientResult = new PatientResult()
        {
            Id = patient.Id,
            Name = patient.Name,
            Address = patient.Address,
            Phone = patient.Phone,
            Email = patient.Email
        };

        return patientResult;
    }

    public async Task<PatientResult> DeletePatient([Service] IUnitOfWork unitOfWork, Guid id)
    {
        Patient patient = await unitOfWork.Patients.GetById(id);

        if (patient == null) throw new GraphQLException(new Error("Patient not found", "PATIENT_NOT_FOUND"));

        PatientResult patientResult = new PatientResult()
        {
            Id = patient.Id,
            Name = patient.Name,
            Address = patient.Address,
            Phone = patient.Phone,
            Email = patient.Email
        };


        await unitOfWork.Patients.Delete(id);

        return patientResult;
    }
}
