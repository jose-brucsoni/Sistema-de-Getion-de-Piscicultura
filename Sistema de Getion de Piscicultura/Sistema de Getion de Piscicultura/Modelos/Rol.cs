using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

/// <summary>
/// Catálogo de roles. Tabla dbo.Rol (script BaseDeDatos/Tablas/Consulta.sql).
/// </summary>
[Table("Rol")]
public class Rol
{
    [Key]
    [Column("IdRol")]
    public int IdRol { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(150)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;
}
