namespace Victoralm.MAE.API.GraphQL.Mutations.Inputs;

public class MedicInput
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<Guid> MedicalSpecialtyId { get; set; }
}
