using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Inventario_Service
{
    private readonly List<InventarioItem> _items = new()
    {
        new InventarioItem { Id = 1, Nombre = "Micro-granulado Fase 1", Categoria = "Alimento", StockKg = 420m, StockMinimoKg = 80m },
        new InventarioItem { Id = 2, Nombre = "Pellets juveniles Fase 2", Categoria = "Alimento", StockKg = 780m, StockMinimoKg = 120m },
        new InventarioItem { Id = 3, Nombre = "Concentrado engorde Fase 3-4", Categoria = "Alimento", StockKg = 1050m, StockMinimoKg = 180m }
    };

    private int _nextId = 4;

    public List<InventarioItem> ObtenerTodos()
        => _items.OrderBy(x => x.Nombre).ToList();

    public InventarioItem? ObtenerPorId(int itemId)
        => _items.FirstOrDefault(x => x.Id == itemId);

    public (bool exito, string mensaje) AgregarNuevoItem(string nombre, string categoria, decimal stockInicialKg, decimal stockMinimoKg)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return (false, "El nombre del insumo es obligatorio.");
        }

        if (_items.Any(i => i.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            return (false, "Ya existe un insumo con ese nombre.");
        }

        if (stockInicialKg < 0 || stockMinimoKg < 0)
        {
            return (false, "Los valores de stock no pueden ser negativos.");
        }

        _items.Add(new InventarioItem
        {
            Id = _nextId++,
            Nombre = nombre.Trim(),
            Categoria = string.IsNullOrWhiteSpace(categoria) ? "General" : categoria.Trim(),
            StockKg = stockInicialKg,
            StockMinimoKg = stockMinimoKg
        });

        return (true, "Insumo agregado al inventario.");
    }

    public (bool exito, string mensaje) AgregarStock(int itemId, decimal cantidadKg)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item is null)
        {
            return (false, "Insumo no encontrado.");
        }

        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        item.StockKg += cantidadKg;
        return (true, "Stock actualizado correctamente.");
    }

    public (bool exito, string mensaje) DescontarStock(int itemId, decimal cantidadKg)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item is null)
        {
            return (false, "Insumo no encontrado.");
        }

        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        if (item.StockKg < cantidadKg)
        {
            return (false, "Stock insuficiente.");
        }

        item.StockKg -= cantidadKg;
        return (true, "Stock descontado correctamente.");
    }
}
