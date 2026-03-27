using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

[Table("RegistrosAlimentacion")]
public class RegistroAlimentacion
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int LoteId { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    [Column(TypeName = "time")]
    public TimeSpan Horario { get; set; }

    [Required]
    [StringLength(80)]
    public string TipoAlimento { get; set; } = string.Empty;

    [Range(0.01, 99999)]
    [Column(TypeName = "decimal(10,3)")]
    public decimal CantidadKg { get; set; }

    public int? InventarioItemId { get; set; }
}
