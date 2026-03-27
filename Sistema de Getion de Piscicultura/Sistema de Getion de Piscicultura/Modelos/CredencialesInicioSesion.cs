using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

/// <summary>
/// Datos del formulario de login. No corresponde a una tabla; sirve para validar entrada
/// antes de comprobar credenciales contra <see cref="Usuario"/> (PasswordHash en BD).
/// </summary>
public class CredencialesInicioSesion
{
    [Required(ErrorMessage = "Ingrese el usuario.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El usuario es obligatorio.")]
    [Display(Name = "Usuario")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese la contraseña.")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; } = string.Empty;
}
