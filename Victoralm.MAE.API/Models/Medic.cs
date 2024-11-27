using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Victoralm.MAE.API.Models;

public class Medic
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    [Phone]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [ForeignKey(nameof(MedicalSpecialty))]
    public List<Guid> MedicalSpecialtyId { get; set; }

    [JsonIgnore]
    public List<Appointment> Appointments { get; } = new();

}
