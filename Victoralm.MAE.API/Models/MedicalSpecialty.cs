using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Victoralm.MAE.API.Models;

public class MedicalSpecialty
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

    [JsonIgnore]
    public List<Medic> Medics { get; } = new();
}
