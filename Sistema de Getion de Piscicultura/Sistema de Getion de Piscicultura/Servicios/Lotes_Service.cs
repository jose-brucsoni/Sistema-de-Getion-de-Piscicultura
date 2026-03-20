using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Lotes_Service
{
    private readonly List<Lote> _lotes = new()
    {
        new Lote
        {
            Id = 1,
            Codigo = "LOT-2026-001",
            EspecieId = 1,
            EstanqueId = 1,
            ProveedorId = 1,
            FechaSiembra = new DateTime(2026, 3, 15),
            CantidadInicial = 3000,
            CantidadActual = 2850,
            FaseActual = FaseCrecimiento.Alevinaje,
            Estado = EstadoLote.Activo,
            FechaCreacion = DateTime.UtcNow
        },
        new Lote
        {
            Id = 2,
            Codigo = "LOT-2026-004",
            EspecieId = 2,
            EstanqueId = 2,
            ProveedorId = 2,
            FechaSiembra = new DateTime(2026, 2, 10),
            CantidadInicial = 1800,
            CantidadActual = 1760,
            FaseActual = FaseCrecimiento.Juveniles,
            Estado = EstadoLote.Activo,
            FechaCreacion = DateTime.UtcNow
        }
    };

    private int _nextId = 3;

    public List<Lote> ObtenerActivos()
        => _lotes.Where(l => l.Estado == EstadoLote.Activo).OrderBy(l => l.Codigo).ToList();

    public List<Lote> ObtenerTodos()
        => _lotes.OrderBy(l => l.Codigo).ToList();

    public Lote? ObtenerPorId(int id)
        => _lotes.FirstOrDefault(l => l.Id == id);

    public (bool exito, string mensaje, int? loteId) Crear(Lote lote)
    {
        lote.Codigo = GenerarCodigoSiguiente();

        var resultado = lote.CrearLote();
        if (!resultado.exito)
        {
            return (false, resultado.mensaje, null);
        }

        lote.Id = _nextId++;
        _lotes.Add(lote);
        return (true, resultado.mensaje, lote.Id);
    }

    public string ObtenerCodigoSiguiente()
        => GenerarCodigoSiguiente();

    public (bool exito, string mensaje) Editar(
        int id,
        DateTime fechaSiembra,
        int cantidadInicial,
        int especieId,
        int estanqueId,
        int proveedorId,
        FaseCrecimiento faseActual)
    {
        var lote = ObtenerPorId(id);
        if (lote is null)
        {
            return (false, "Lote no encontrado.");
        }

        // El codigo no se edita manualmente; se conserva el asignado al crear.
        return lote.EditarLote(lote.Codigo, fechaSiembra, cantidadInicial, especieId, estanqueId, proveedorId, faseActual);
    }

    public (bool exito, string mensaje) Anular(int id)
    {
        var lote = ObtenerPorId(id);
        if (lote is null)
        {
            return (false, "Lote no encontrado.");
        }

        return lote.AnularLote();
    }

    private string GenerarCodigoSiguiente()
    {
        var anioActual = DateTime.Today.Year;
        var prefijo = $"LOT-{anioActual}-";

        var ultimoCorrelativo = _lotes
            .Select(l => l.Codigo)
            .Where(c => !string.IsNullOrWhiteSpace(c) && c.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase))
            .Select(c =>
            {
                var partes = c.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length != 3)
                {
                    return 0;
                }

                return int.TryParse(partes[2], out var numero) ? numero : 0;
            })
            .DefaultIfEmpty(0)
            .Max();

        return $"{prefijo}{(ultimoCorrelativo + 1):000}";
    }
}
