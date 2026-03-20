using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Getion_de_Piscicultura.Modelos;

public enum FaseCrecimiento
{
    Alevinaje = 1,
    Juveniles = 2,
    PreEngorde = 3,
    EngordeFinal = 4
}

public enum EstadoLote
{
    Activo = 1,
    Inactivo = 2,
    Cosechado = 3
}

[Table("Lotes")]
public class Lote
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    public DateTime FechaSiembra { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int CantidadInicial { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int CantidadActual { get; set; }

    [Required]
    public int EspecieId { get; set; }

    [Required]
    public int EstanqueId { get; set; }

    [Required]
    public int ProveedorId { get; set; }

    [Required]
    public FaseCrecimiento FaseActual { get; set; } = FaseCrecimiento.Alevinaje;

    [Required]
    public EstadoLote Estado { get; set; } = EstadoLote.Activo;

    [Required]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaModificacion { get; set; }

    public (bool exito, string mensaje) CrearLote()
    {
        if (string.IsNullOrWhiteSpace(Codigo))
        {
            return (false, "El codigo del lote es obligatorio.");
        }

        if (CantidadInicial <= 0)
        {
            return (false, "La cantidad inicial debe ser mayor a cero.");
        }

        if (EspecieId <= 0 || EstanqueId <= 0 || ProveedorId <= 0)
        {
            return (false, "Especie, estanque y proveedor son obligatorios.");
        }

        CantidadActual = CantidadInicial;
        Estado = EstadoLote.Activo;
        FaseActual = FaseCrecimiento.Alevinaje;
        FechaCreacion = DateTime.UtcNow;
        FechaModificacion = DateTime.UtcNow;

        return (true, "Lote creado correctamente.");
    }

    public (bool exito, string mensaje) EditarLote(
        string codigo,
        DateTime fechaSiembra,
        int cantidadInicial,
        int especieId,
        int estanqueId,
        int proveedorId,
        FaseCrecimiento faseActual)
    {
        if (Estado == EstadoLote.Inactivo)
        {
            return (false, "No se puede editar un lote anulado.");
        }

        if (string.IsNullOrWhiteSpace(codigo))
        {
            return (false, "El codigo del lote es obligatorio.");
        }

        if (cantidadInicial <= 0)
        {
            return (false, "La cantidad inicial debe ser mayor a cero.");
        }

        if (especieId <= 0 || estanqueId <= 0 || proveedorId <= 0)
        {
            return (false, "Especie, estanque y proveedor son obligatorios.");
        }

        Codigo = codigo;
        FechaSiembra = fechaSiembra;
        CantidadInicial = cantidadInicial;
        EspecieId = especieId;
        EstanqueId = estanqueId;
        ProveedorId = proveedorId;
        FaseActual = faseActual;

        if (CantidadActual <= 0)
        {
            CantidadActual = cantidadInicial;
        }

        FechaModificacion = DateTime.UtcNow;

        return (true, "Lote editado correctamente.");
    }

    public (bool exito, string mensaje) AnularLote()
    {
        if (Estado == EstadoLote.Inactivo)
        {
            return (false, "El lote ya se encuentra anulado.");
        }

        Estado = EstadoLote.Inactivo;
        FechaModificacion = DateTime.UtcNow;

        return (true, "Lote anulado correctamente.");
    }
}
