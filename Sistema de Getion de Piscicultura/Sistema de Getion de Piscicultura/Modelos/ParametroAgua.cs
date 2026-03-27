using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

[Table("ParametrosAgua")]
public class ParametroAgua
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int LoteId { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Temperatura { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Ph { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,2)")]
    public decimal OxigenoDisuelto { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,3)")]
    public decimal Amonio { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,3)")]
    public decimal Nitritos { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Turbidez { get; set; }

    [StringLength(500)]
    public string? CondicionesClimaticas { get; set; }

    [StringLength(500)]
    public string? Observaciones { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaModificacion { get; set; }

    public (bool exito, string mensaje) Validar()
    {
        if (LoteId <= 0)
        {
            return (false, "Debe seleccionar un lote.");
        }

        if (Temperatura < 0 || Temperatura > 40)
        {
            return (false, "La temperatura debe estar entre 0 y 40 °C.");
        }

        if (Ph < 0 || Ph > 14)
        {
            return (false, "El pH debe estar entre 0 y 14.");
        }

        if (OxigenoDisuelto < 0 || OxigenoDisuelto > 20)
        {
            return (false, "El oxígeno disuelto debe estar entre 0 y 20 mg/L.");
        }

        if (Amonio < 0 || Nitritos < 0 || Turbidez < 0)
        {
            return (false, "Amonio, nitritos y turbidez no pueden ser negativos.");
        }

        return (true, "Validación correcta.");
    }
}
