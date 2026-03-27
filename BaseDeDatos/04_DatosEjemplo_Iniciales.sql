/*
  Datos de ejemplo para arrancar el sistema sin cargar todo manualmente.
  Script idempotente: puedes ejecutarlo varias veces.

  Recomendado ejecutar despues de:
    1) 00_CrearBaseYPermisos.sql
    2) Tablas/Consulta.sql
    3) Procedimientos/Procedimientos_CRUD_Modelos.sql
    4) 03_CrearTablaAlerta_Modelos.sql (si usaras modulo de alertas nuevo)
*/

USE [Piscicultura];
GO

SET NOCOUNT ON;
GO

/* =========================
   1) Catalogos base
   ========================= */

IF OBJECT_ID(N'dbo.Especie', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Especie WHERE Nombre = N'Tilapia nilotica')
    BEGIN
        INSERT INTO dbo.Especie (Nombre, TempMin, TempMax, PhMin, PhMax, OxigenoMin, PesoComercialMin, PesoComercialMax, Activo)
        VALUES (N'Tilapia nilotica', 24.00, 30.00, 6.50, 8.50, 5.00, 350.00, 650.00, 1);
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.Especie WHERE Nombre = N'Trucha arcoiris')
    BEGIN
        INSERT INTO dbo.Especie (Nombre, TempMin, TempMax, PhMin, PhMax, OxigenoMin, PesoComercialMin, PesoComercialMax, Activo)
        VALUES (N'Trucha arcoiris', 10.00, 18.00, 6.80, 8.00, 6.50, 250.00, 450.00, 1);
    END;
END;

IF OBJECT_ID(N'dbo.Proveedor', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Proveedor WHERE Nombre = N'Alevines Andinos')
    BEGIN
        INSERT INTO dbo.Proveedor (Nombre, CertificacionSanitaria, Contacto, Activo)
        VALUES (N'Alevines Andinos', N'SENASA-PE-2026', N'+51 999 111 222', 1);
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.Proveedor WHERE Nombre = N'BioPeces del Sur')
    BEGIN
        INSERT INTO dbo.Proveedor (Nombre, CertificacionSanitaria, Contacto, Activo)
        VALUES (N'BioPeces del Sur', N'SENASA-PE-2025', N'+51 999 333 444', 1);
    END;
END;

IF OBJECT_ID(N'dbo.Estanque', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Estanque WHERE Codigo = N'E-01')
    BEGIN
        INSERT INTO dbo.Estanque (Codigo, Tipo, VolumenM3, CapacidadMaxima, Estado, Activo)
        VALUES (N'E-01', N'Tierra', 120.00, 8000, N'Activo', 1);
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.Estanque WHERE Codigo = N'E-02')
    BEGIN
        INSERT INTO dbo.Estanque (Codigo, Tipo, VolumenM3, CapacidadMaxima, Estado, Activo)
        VALUES (N'E-02', N'Geomembrana', 90.00, 6000, N'Activo', 1);
    END;
END;

/* =========================
   2) Usuarios de ejemplo (autenticacion)
   ========================= */

IF OBJECT_ID(N'dbo.Rol', N'U') IS NOT NULL AND OBJECT_ID(N'dbo.Usuario', N'U') IS NOT NULL
BEGIN
    DECLARE @IdRolTecnico INT = (SELECT TOP 1 IdRol FROM dbo.Rol WHERE Nombre = N'Tecnico');
    DECLARE @IdRolSupervisor INT = (SELECT TOP 1 IdRol FROM dbo.Rol WHERE Nombre = N'Supervisor');

    IF @IdRolTecnico IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Username = N'tecnico')
    BEGIN
        INSERT INTO dbo.Usuario (IdRol, Username, Correo, PasswordHash, Activo, CreatedAt)
        VALUES (
            @IdRolTecnico,
            N'tecnico',
            N'tecnico@piscicultura.local',
            N'PBKDF2$100000$QfGwyP4iRJ9Ycjb2fMNqUA==$nCxgGnS8+FvY8zlT8+I1YmMpvQxRzL19zv2Rk6RR7K0=',
            1,
            SYSUTCDATETIME()
        );
    END;

    IF @IdRolSupervisor IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Username = N'supervisor')
    BEGIN
        INSERT INTO dbo.Usuario (IdRol, Username, Correo, PasswordHash, Activo, CreatedAt)
        VALUES (
            @IdRolSupervisor,
            N'supervisor',
            N'supervisor@piscicultura.local',
            N'PBKDF2$100000$Rj5qS95QZtqj6FQW8xFyFQ==$LkccM4h8kE7jzCz3wWjK4V8h4NQ5IX0NVjMS6JUU5lM=',
            1,
            SYSUTCDATETIME()
        );
    END;
END;

/* =========================
   3) Lotes de ejemplo (tabla singular + plural)
   ========================= */

DECLARE @IdEspecieTilapia INT = NULL;
DECLARE @IdEspecieTrucha INT = NULL;
DECLARE @IdEstanqueE01 INT = NULL;
DECLARE @IdEstanqueE02 INT = NULL;
DECLARE @IdProveedorAlevines INT = NULL;
DECLARE @IdProveedorBioPeces INT = NULL;
DECLARE @IdFaseJuveniles INT = NULL;
DECLARE @IdFasePreEngorde INT = NULL;

IF OBJECT_ID(N'dbo.Especie', N'U') IS NOT NULL
BEGIN
    SELECT @IdEspecieTilapia = IdEspecie FROM dbo.Especie WHERE Nombre = N'Tilapia nilotica';
    SELECT @IdEspecieTrucha = IdEspecie FROM dbo.Especie WHERE Nombre = N'Trucha arcoiris';
END;

IF OBJECT_ID(N'dbo.Estanque', N'U') IS NOT NULL
BEGIN
    SELECT @IdEstanqueE01 = IdEstanque FROM dbo.Estanque WHERE Codigo = N'E-01';
    SELECT @IdEstanqueE02 = IdEstanque FROM dbo.Estanque WHERE Codigo = N'E-02';
END;

IF OBJECT_ID(N'dbo.Proveedor', N'U') IS NOT NULL
BEGIN
    SELECT @IdProveedorAlevines = IdProveedor FROM dbo.Proveedor WHERE Nombre = N'Alevines Andinos';
    SELECT @IdProveedorBioPeces = IdProveedor FROM dbo.Proveedor WHERE Nombre = N'BioPeces del Sur';
END;

IF OBJECT_ID(N'dbo.FaseCrianza', N'U') IS NOT NULL
BEGIN
    SELECT @IdFaseJuveniles = IdFase FROM dbo.FaseCrianza WHERE Nombre = N'Juveniles';
    SELECT @IdFasePreEngorde = IdFase FROM dbo.FaseCrianza WHERE Nombre = N'Pre-engorde';
END;

IF OBJECT_ID(N'dbo.Lote', N'U') IS NOT NULL
BEGIN
    IF @IdEspecieTilapia IS NOT NULL AND @IdEstanqueE01 IS NOT NULL AND @IdProveedorAlevines IS NOT NULL AND @IdFaseJuveniles IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.Lote WHERE Codigo = N'LOT-2026-001')
    BEGIN
        INSERT INTO dbo.Lote (IdEspecie, IdEstanque, IdProveedor, IdFaseActual, Codigo, FechaSiembra, CantidadInicial, CantidadActual, Estado)
        VALUES (@IdEspecieTilapia, @IdEstanqueE01, @IdProveedorAlevines, @IdFaseJuveniles, N'LOT-2026-001', DATEADD(DAY, -45, CAST(GETDATE() AS DATE)), 5000, 4900, N'Activo');
    END;

    IF @IdEspecieTrucha IS NOT NULL AND @IdEstanqueE02 IS NOT NULL AND @IdProveedorBioPeces IS NOT NULL AND @IdFasePreEngorde IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.Lote WHERE Codigo = N'LOT-2026-002')
    BEGIN
        INSERT INTO dbo.Lote (IdEspecie, IdEstanque, IdProveedor, IdFaseActual, Codigo, FechaSiembra, CantidadInicial, CantidadActual, Estado)
        VALUES (@IdEspecieTrucha, @IdEstanqueE02, @IdProveedorBioPeces, @IdFasePreEngorde, N'LOT-2026-002', DATEADD(DAY, -70, CAST(GETDATE() AS DATE)), 3200, 3050, N'Activo');
    END;
END;

IF OBJECT_ID(N'dbo.Lotes', N'U') IS NOT NULL
BEGIN
    IF @IdEspecieTilapia IS NOT NULL AND @IdEstanqueE01 IS NOT NULL AND @IdProveedorAlevines IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.Lotes WHERE Codigo = N'LOT-2026-001')
    BEGIN
        INSERT INTO dbo.Lotes (Codigo, FechaSiembra, CantidadInicial, CantidadActual, EspecieId, EstanqueId, ProveedorId, FaseActual, Estado, FechaCreacion, FechaModificacion)
        VALUES (N'LOT-2026-001', DATEADD(DAY, -45, CAST(GETDATE() AS DATE)), 5000, 4900, @IdEspecieTilapia, @IdEstanqueE01, @IdProveedorAlevines, 2, 1, SYSUTCDATETIME(), SYSUTCDATETIME());
    END;

    IF @IdEspecieTrucha IS NOT NULL AND @IdEstanqueE02 IS NOT NULL AND @IdProveedorBioPeces IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.Lotes WHERE Codigo = N'LOT-2026-002')
    BEGIN
        INSERT INTO dbo.Lotes (Codigo, FechaSiembra, CantidadInicial, CantidadActual, EspecieId, EstanqueId, ProveedorId, FaseActual, Estado, FechaCreacion, FechaModificacion)
        VALUES (N'LOT-2026-002', DATEADD(DAY, -70, CAST(GETDATE() AS DATE)), 3200, 3050, @IdEspecieTrucha, @IdEstanqueE02, @IdProveedorBioPeces, 3, 1, SYSUTCDATETIME(), SYSUTCDATETIME());
    END;
END;

/* =========================
   4) Inventario de alimento (singular + plural)
   ========================= */

IF OBJECT_ID(N'dbo.InventarioAlimento', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.InventarioAlimento WHERE TipoAlimento = N'Balanceado 35%')
    BEGIN
        INSERT INTO dbo.InventarioAlimento (TipoAlimento, StockActualKg, StockMinimoKg, FechaActualizacion, Activo)
        VALUES (N'Balanceado 35%', 620.000, 180.000, SYSUTCDATETIME(), 1);
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.InventarioAlimento WHERE TipoAlimento = N'Balanceado 42%')
    BEGIN
        INSERT INTO dbo.InventarioAlimento (TipoAlimento, StockActualKg, StockMinimoKg, FechaActualizacion, Activo)
        VALUES (N'Balanceado 42%', 410.000, 150.000, SYSUTCDATETIME(), 1);
    END;
END;

IF OBJECT_ID(N'dbo.InventarioItems', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.InventarioItems WHERE Nombre = N'Balanceado 35%')
    BEGIN
        INSERT INTO dbo.InventarioItems (Nombre, Categoria, StockKg, StockMinimoKg)
        VALUES (N'Balanceado 35%', N'Engorde', 620.000, 180.000);
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.InventarioItems WHERE Nombre = N'Balanceado 42%')
    BEGIN
        INSERT INTO dbo.InventarioItems (Nombre, Categoria, StockKg, StockMinimoKg)
        VALUES (N'Balanceado 42%', N'Juveniles', 410.000, 150.000);
    END;
END;

/* =========================
   5) Parametros de agua y alimentacion (singular + plural)
   ========================= */

DECLARE @IdLote001_S INT = NULL;
DECLARE @IdLote002_S INT = NULL;
DECLARE @IdLote001_P INT = NULL;
DECLARE @IdLote002_P INT = NULL;
DECLARE @IdInv35_P INT = NULL;
DECLARE @IdInv42_P INT = NULL;
DECLARE @IdInv35_S INT = NULL;
DECLARE @IdInv42_S INT = NULL;

IF OBJECT_ID(N'dbo.Lote', N'U') IS NOT NULL
BEGIN
    SELECT @IdLote001_S = IdLote FROM dbo.Lote WHERE Codigo = N'LOT-2026-001';
    SELECT @IdLote002_S = IdLote FROM dbo.Lote WHERE Codigo = N'LOT-2026-002';
END;

IF OBJECT_ID(N'dbo.Lotes', N'U') IS NOT NULL
BEGIN
    SELECT @IdLote001_P = Id FROM dbo.Lotes WHERE Codigo = N'LOT-2026-001';
    SELECT @IdLote002_P = Id FROM dbo.Lotes WHERE Codigo = N'LOT-2026-002';
END;

IF OBJECT_ID(N'dbo.InventarioItems', N'U') IS NOT NULL
BEGIN
    SELECT @IdInv35_P = Id FROM dbo.InventarioItems WHERE Nombre = N'Balanceado 35%';
    SELECT @IdInv42_P = Id FROM dbo.InventarioItems WHERE Nombre = N'Balanceado 42%';
END;

IF OBJECT_ID(N'dbo.InventarioAlimento', N'U') IS NOT NULL
BEGIN
    SELECT @IdInv35_S = IdInventario FROM dbo.InventarioAlimento WHERE TipoAlimento = N'Balanceado 35%';
    SELECT @IdInv42_S = IdInventario FROM dbo.InventarioAlimento WHERE TipoAlimento = N'Balanceado 42%';
END;

IF OBJECT_ID(N'dbo.ParametroAgua', N'U') IS NOT NULL AND @IdLote001_S IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.ParametroAgua WHERE IdLote = @IdLote001_S AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE))
    BEGIN
        INSERT INTO dbo.ParametroAgua (IdLote, IdUsuario, FechaRegistro, Temperatura, Ph, OxigenoDisuelto, Amonio, Nitritos, Turbidez, CondicionesClimaticas, Observaciones)
        VALUES (@IdLote001_S, NULL, DATEADD(HOUR, -2, SYSDATETIME()), 27.6, 7.4, 5.8, 0.06, 0.03, 13.2, N'Nublado parcial', N'Valores estables');
    END;
END;

IF OBJECT_ID(N'dbo.ParametrosAgua', N'U') IS NOT NULL AND @IdLote001_P IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.ParametrosAgua WHERE LoteId = @IdLote001_P AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE))
    BEGIN
        INSERT INTO dbo.ParametrosAgua (LoteId, FechaRegistro, Temperatura, Ph, OxigenoDisuelto, Amonio, Nitritos, Turbidez, CondicionesClimaticas, Observaciones, FechaCreacion, FechaModificacion)
        VALUES (@IdLote001_P, DATEADD(HOUR, -2, SYSDATETIME()), 27.6, 7.4, 5.8, 0.06, 0.03, 13.2, N'Nublado parcial', N'Valores estables', SYSUTCDATETIME(), SYSUTCDATETIME());
    END;
END;

IF OBJECT_ID(N'dbo.RegistroAlimentacion', N'U') IS NOT NULL AND @IdLote001_S IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.RegistroAlimentacion WHERE IdLote = @IdLote001_S AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE) AND Horario = '08:00')
    BEGIN
        INSERT INTO dbo.RegistroAlimentacion (IdLote, IdUsuario, IdInventario, FechaRegistro, Horario, TipoAlimento, CantidadKg, Observacion)
        VALUES (@IdLote001_S, NULL, @IdInv35_S, CAST(GETDATE() AS DATE), '08:00', N'Balanceado 35%', 18.500, N'Racion matutina');
    END;
END;

IF OBJECT_ID(N'dbo.RegistrosAlimentacion', N'U') IS NOT NULL AND @IdLote001_P IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.RegistrosAlimentacion WHERE LoteId = @IdLote001_P AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE) AND Horario = '08:00')
    BEGIN
        INSERT INTO dbo.RegistrosAlimentacion (LoteId, FechaRegistro, Horario, TipoAlimento, CantidadKg, InventarioItemId)
        VALUES (@IdLote001_P, CAST(GETDATE() AS DATE), '08:00', N'Balanceado 35%', 18.500, @IdInv35_P);
    END;
END;

/* =========================
   6) Eventos de crianza (Sprint 2, esquema singular)
   ========================= */

IF OBJECT_ID(N'dbo.Mortalidad', N'U') IS NOT NULL AND @IdLote001_S IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Mortalidad WHERE IdLote = @IdLote001_S AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE))
    BEGIN
        INSERT INTO dbo.Mortalidad (IdLote, IdUsuario, FechaRegistro, Cantidad, Causa, Observacion)
        VALUES (@IdLote001_S, NULL, DATEADD(HOUR, -1, SYSDATETIME()), 22, N'Estrés', N'Mortalidad dentro de rango esperado');
    END;
END;

IF OBJECT_ID(N'dbo.Muestreo', N'U') IS NOT NULL AND @IdLote001_S IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Muestreo WHERE IdLote = @IdLote001_S AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE))
    BEGIN
        INSERT INTO dbo.Muestreo (IdLote, IdUsuario, FechaRegistro, CantidadMuestra, PesoPromedio, Dispersion)
        VALUES (@IdLote001_S, NULL, DATEADD(HOUR, -3, SYSDATETIME()), 60, 285.400, N'270-300');
    END;
END;

IF OBJECT_ID(N'dbo.Tratamiento', N'U') IS NOT NULL AND @IdLote002_S IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Tratamiento WHERE IdLote = @IdLote002_S AND CAST(FechaAplicacion AS DATE) = CAST(GETDATE() AS DATE))
    BEGIN
        INSERT INTO dbo.Tratamiento (IdLote, IdUsuario, FechaAplicacion, Tipo, Dosis, Observacion)
        VALUES (@IdLote002_S, NULL, DATEADD(HOUR, -4, SYSDATETIME()), N'Antiparasitario', 1.250, N'Aplicacion preventiva');
    END;
END;

IF OBJECT_ID(N'dbo.RegistroBiomasa', N'U') IS NOT NULL AND @IdLote001_S IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.RegistroBiomasa WHERE IdLote = @IdLote001_S AND CAST(FechaRegistro AS DATE) = CAST(GETDATE() AS DATE))
    BEGIN
        INSERT INTO dbo.RegistroBiomasa (IdLote, FechaRegistro, CantidadPeces, BiomasaEstimadaKg, IdUsuario, IdMuestreoOrigen, Observaciones)
        VALUES (@IdLote001_S, DATEADD(HOUR, -2, SYSDATETIME()), 4878, 1391.500, NULL, NULL, N'Calculo inicial de biomasa');
    END;
END;

/* =========================
   7) Alertas de ejemplo (si existe tabla)
   ========================= */

IF OBJECT_ID(N'dbo.Alerta', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Alerta WHERE Mensaje = N'Stock de Balanceado 35% cercano al minimo operativo.')
    BEGIN
        INSERT INTO dbo.Alerta (IdLote, FechaHora, Tipo, Nivel, Mensaje, Atendida, IdParametroAgua, IdInventario)
        VALUES (NULL, DATEADD(MINUTE, -40, SYSDATETIME()), N'StockAlimento', N'Media', N'Stock de Balanceado 35% cercano al minimo operativo.', 0, NULL, NULL);
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.Alerta WHERE Mensaje = N'pH fuera de rango detectado en monitoreo preventivo.')
    BEGIN
        INSERT INTO dbo.Alerta (IdLote, FechaHora, Tipo, Nivel, Mensaje, Atendida, IdParametroAgua, IdInventario)
        VALUES (NULL, DATEADD(MINUTE, -20, SYSDATETIME()), N'CalidadAgua', N'Alta', N'pH fuera de rango detectado en monitoreo preventivo.', 0, NULL, NULL);
    END;
END;

PRINT N'Datos de ejemplo cargados correctamente.';
GO
