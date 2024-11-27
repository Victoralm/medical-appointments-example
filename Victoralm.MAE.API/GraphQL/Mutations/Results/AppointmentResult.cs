namespace Victoralm.MAE.API.GraphQL.Mutations.Results;

public class AppointmentResult
{
    public Guid Id { get; set; }
    public DateTime Schedule { get; set; }
    public Guid PatientId { get; set; }
    public Guid MedicId { get; set; }
}
