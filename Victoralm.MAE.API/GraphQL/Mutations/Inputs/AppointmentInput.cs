namespace Victoralm.MAE.API.GraphQL.Mutations.Inputs;

public class AppointmentInput
{
    public DateTime Schedule { get; set; }
    public Guid PatientId { get; set; }
    public Guid MedicId { get; set; }
}
