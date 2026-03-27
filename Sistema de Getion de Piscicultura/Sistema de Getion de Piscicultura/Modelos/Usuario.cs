using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

/// <summary>
/// Usuario de aplicación. Tabla dbo.Usuario (script BaseDeDatos/Tablas/Consulta.sql).
/// La contraseña en BD es PasswordHash; no almacenar texto plano.
/// </summary>
[Table("Usuario")]
public class Usuario
{
    [Key]
    [Column("IdUsuario")]
    public int IdUsuario { get; set; }

    [Required]
    public int IdRol { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Correo { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(IdRol))]
    public virtual Rol? Rol { get; set; }
}
