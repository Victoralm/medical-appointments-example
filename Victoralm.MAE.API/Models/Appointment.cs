using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Victoralm.MAE.API.Models;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime Schedule { get; set; }

    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; }

    [ForeignKey(nameof(Medic))]
    public Guid MedicId { get; set; }
}
