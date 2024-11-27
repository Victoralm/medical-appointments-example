using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Victoralm.MAE.API.Models;

namespace Victoralm.MAE.API.GraphQL.Mutations.Results;

public class PatientResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<Appointment>? Appointments { get; set; }
}
