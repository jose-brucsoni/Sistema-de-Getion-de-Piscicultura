using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

public class RegistroAlimentacion
{
    public int Id { get; set; }

    [Required]
    public int LoteId { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    public TimeSpan Horario { get; set; }

    [Required]
    [StringLength(80)]
    public string TipoAlimento { get; set; } = string.Empty;

    [Range(0.01, 99999)]
    public decimal CantidadKg { get; set; }
}
