using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class AlimentacionInventario_Service
{
    private readonly Lotes_Service _lotesService;
    private readonly Inventario_Service _inventarioService;

    private readonly List<RegistroAlimentacion> _registros = new();
    private int _nextId = 1;

    public AlimentacionInventario_Service(Lotes_Service lotesService, Inventario_Service inventarioService)
    {
        _lotesService = lotesService;
        _inventarioService = inventarioService;
    }

    public IReadOnlyList<InventarioItem> ObtenerInventario()
        => _inventarioService.ObtenerTodos();

    public IReadOnlyList<RegistroAlimentacion> ObtenerRegistrosPorFecha(DateTime fecha)
        => _registros.Where(r => r.FechaRegistro.Date == fecha.Date).OrderBy(r => r.Horario).ToList();

    public IReadOnlyList<ConsumoEstanqueDto> ObtenerConsumoPorEstanque(DateTime fecha)
    {
        var registrosDelDia = _registros.Where(r => r.FechaRegistro.Date == fecha.Date);

        return AgruparConsumoPorEstanque(registrosDelDia);
    }

    public IReadOnlyList<ConsumoEstanqueDto> ObtenerConsumoPorEstanqueRango(DateTime fechaInicio, DateTime fechaFin)
    {
        var inicio = fechaInicio.Date;
        var fin = fechaFin.Date;
        if (fin < inicio)
        {
            (inicio, fin) = (fin, inicio);
        }

        var registrosRango = _registros.Where(r => r.FechaRegistro.Date >= inicio && r.FechaRegistro.Date <= fin);
        return AgruparConsumoPorEstanque(registrosRango);
    }

    private IReadOnlyList<ConsumoEstanqueDto> AgruparConsumoPorEstanque(IEnumerable<RegistroAlimentacion> registros)
    {
        return registros
            .GroupBy(r => new { r.LoteId, r.TipoAlimento })
            .Select(g =>
            {
                var lote = _lotesService.ObtenerPorId(g.Key.LoteId);
                return new ConsumoEstanqueDto
                {
                    LoteId = g.Key.LoteId,
                    CodigoLote = lote?.Codigo ?? $"Lote {g.Key.LoteId}",
                    EstanqueId = lote?.EstanqueId ?? 0,
                    TipoAlimento = g.Key.TipoAlimento,
                    CantidadKg = g.Sum(x => x.CantidadKg)
                };
            })
            .OrderBy(x => x.TipoAlimento)
            .ThenBy(x => x.EstanqueId)
            .ThenBy(x => x.CodigoLote)
            .ToList();
    }

    public (bool exito, string mensaje) RegistrarAlimentacion(
        int loteId,
        DateTime fechaRegistro,
        TimeSpan horario,
        int alimentoId,
        decimal cantidadKg)
    {
        var lote = _lotesService.ObtenerPorId(loteId);
        if (lote is null)
        {
            return (false, "Lote no encontrado.");
        }

        var alimento = _inventarioService.ObtenerPorId(alimentoId);
        if (alimento is null)
        {
            return (false, "Alimento no encontrado.");
        }

        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        var frecuenciaMaxima = ObtenerFrecuenciaMaxima(lote.FaseActual);
        var frecuenciaActual = _registros.Count(r => r.LoteId == loteId && r.FechaRegistro.Date == fechaRegistro.Date);
        if (frecuenciaActual >= frecuenciaMaxima)
        {
            return (false, $"Se alcanzó la frecuencia máxima diaria para la fase {lote.FaseActual} ({frecuenciaMaxima}).");
        }

        var descuento = _inventarioService.DescontarStock(alimentoId, cantidadKg);
        if (!descuento.exito)
        {
            return (false, "Stock insuficiente para registrar la alimentación.");
        }

        _registros.Add(new RegistroAlimentacion
        {
            Id = _nextId++,
            LoteId = loteId,
            FechaRegistro = fechaRegistro.Date,
            Horario = horario,
            TipoAlimento = alimento.Nombre,
            CantidadKg = cantidadKg
        });

        return (true, "Alimentación registrada correctamente.");
    }

    private static int ObtenerFrecuenciaMaxima(FaseCrecimiento fase)
        => fase switch
        {
            FaseCrecimiento.Alevinaje => 6,
            FaseCrecimiento.Juveniles => 4,
            _ => 3
        };
}

public class ConsumoEstanqueDto
{
    public int LoteId { get; set; }
    public string CodigoLote { get; set; } = string.Empty;
    public int EstanqueId { get; set; }
    public string TipoAlimento { get; set; } = string.Empty;
    public decimal CantidadKg { get; set; }
}
