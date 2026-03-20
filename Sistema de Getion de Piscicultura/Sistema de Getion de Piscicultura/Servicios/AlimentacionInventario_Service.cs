using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class AlimentacionInventario_Service
{
    private readonly Lotes_Service _lotesService;
    private readonly List<AlimentoInventario> _inventario = new()
    {
        new AlimentoInventario { Id = 1, Nombre = "Micro-granulado Fase 1", TipoFase = "Alevinaje", StockKg = 420m, StockMinimoKg = 80m },
        new AlimentoInventario { Id = 2, Nombre = "Pellets juveniles Fase 2", TipoFase = "Juveniles", StockKg = 780m, StockMinimoKg = 120m },
        new AlimentoInventario { Id = 3, Nombre = "Concentrado engorde Fase 3-4", TipoFase = "PreEngorde/EngordeFinal", StockKg = 1050m, StockMinimoKg = 180m }
    };

    private readonly List<RegistroAlimentacion> _registros = new();
    private int _nextId = 1;

    public AlimentacionInventario_Service(Lotes_Service lotesService)
    {
        _lotesService = lotesService;
    }

    public IReadOnlyList<AlimentoInventario> ObtenerInventario()
        => _inventario.OrderBy(x => x.Id).ToList();

    public IReadOnlyList<RegistroAlimentacion> ObtenerRegistrosPorFecha(DateTime fecha)
        => _registros.Where(r => r.FechaRegistro.Date == fecha.Date).OrderBy(r => r.Horario).ToList();

    public IReadOnlyList<ConsumoEstanqueDto> ObtenerConsumoPorEstanque(DateTime fecha)
    {
        var registrosDelDia = _registros.Where(r => r.FechaRegistro.Date == fecha.Date);

        return registrosDelDia
            .GroupBy(r => r.LoteId)
            .Select(g =>
            {
                var lote = _lotesService.ObtenerPorId(g.Key);
                return new ConsumoEstanqueDto
                {
                    LoteId = g.Key,
                    CodigoLote = lote?.Codigo ?? $"Lote {g.Key}",
                    EstanqueId = lote?.EstanqueId ?? 0,
                    CantidadKg = g.Sum(x => x.CantidadKg)
                };
            })
            .OrderBy(x => x.EstanqueId)
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

        var alimento = _inventario.FirstOrDefault(a => a.Id == alimentoId);
        if (alimento is null)
        {
            return (false, "Alimento no encontrado.");
        }

        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        if (alimento.StockKg < cantidadKg)
        {
            return (false, "Stock insuficiente para registrar la alimentación.");
        }

        var frecuenciaMaxima = ObtenerFrecuenciaMaxima(lote.FaseActual);
        var frecuenciaActual = _registros.Count(r => r.LoteId == loteId && r.FechaRegistro.Date == fechaRegistro.Date);
        if (frecuenciaActual >= frecuenciaMaxima)
        {
            return (false, $"Se alcanzó la frecuencia máxima diaria para la fase {lote.FaseActual} ({frecuenciaMaxima}).");
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

        alimento.StockKg -= cantidadKg;
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
    public decimal CantidadKg { get; set; }
}
