using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

[Table("InventarioItems")]
public class InventarioItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Categoria { get; set; } = string.Empty;

    [Range(0, 999999)]
    [Column(TypeName = "decimal(18,3)")]
    public decimal StockKg { get; set; }

    [Range(0, 999999)]
    [Column(TypeName = "decimal(18,3)")]
    public decimal StockMinimoKg { get; set; }
}
