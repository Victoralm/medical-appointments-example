using System.ComponentModel.DataAnnotations;

namespace Victoralm.MAE.API.Models;

public class Testes
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}
