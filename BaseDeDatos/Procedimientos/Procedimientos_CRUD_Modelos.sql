/*
  Procedimientos y funciones alineados con los modelos en:
    Sistema de Getion de Piscicultura/Modelos/
      - Lote.cs          → tabla dbo.Lotes
      - ParametroAgua.cs → tabla dbo.ParametrosAgua
      - InventarioItem.cs → tabla dbo.InventarioItems
      - RegistroAlimentacion.cs → tabla dbo.RegistrosAlimentacion
      - AlimentoInventario.cs → tabla dbo.AlimentoInventarios (opcional, variante de inventario)

  Motor: SQL Server (T-SQL)
  Ejecutar tras USE [SuBaseDeDatos]; GO

  Nota: El script Consulta.sql usa nombres distintos (dbo.Lote, IdLote, etc.). Esta capa sigue
  las propiedades de los modelos C# para uso directo con ADO.NET o EF con tablas homónimas.
  Si su BD solo tiene el esquema de Consulta.sql, cree vistas o sinónimos, o adapte los nombres
  en los procedimientos.
*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ---------- DDL opcional: tablas según modelos (omitir si ya existen con la misma forma) ---------- */

IF OBJECT_ID(N'dbo.Lotes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Lotes (
        Id                  INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Lotes PRIMARY KEY,
        Codigo              NVARCHAR(20)  NOT NULL,
        FechaSiembra        DATETIME2     NOT NULL,
        CantidadInicial     INT           NOT NULL,
        CantidadActual      INT           NOT NULL,
        EspecieId           INT           NOT NULL,
        EstanqueId          INT           NOT NULL,
        ProveedorId         INT           NOT NULL,
        FaseActual          INT           NOT NULL CONSTRAINT DF_Lotes_FaseActual DEFAULT (1),
        Estado              INT           NOT NULL CONSTRAINT DF_Lotes_Estado DEFAULT (1),
        FechaCreacion       DATETIME2     NOT NULL CONSTRAINT DF_Lotes_FechaCreacion DEFAULT (SYSUTCDATETIME()),
        FechaModificacion   DATETIME2     NULL,
        CONSTRAINT UQ_Lotes_Codigo UNIQUE (Codigo)
    );
    CREATE INDEX IX_Lotes_EstanqueId ON dbo.Lotes (EstanqueId);
    CREATE INDEX IX_Lotes_EspecieId ON dbo.Lotes (EspecieId);
END
GO

IF OBJECT_ID(N'dbo.ParametrosAgua', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ParametrosAgua (
        Id                      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ParametrosAgua PRIMARY KEY,
        LoteId                  INT           NOT NULL,
        FechaRegistro           DATETIME2     NOT NULL,
        Temperatura             DECIMAL(9,2)  NOT NULL,
        Ph                      DECIMAL(9,2)  NOT NULL,
        OxigenoDisuelto         DECIMAL(9,2)  NOT NULL,
        Amonio                  DECIMAL(9,3)  NOT NULL,
        Nitritos                DECIMAL(9,3)  NOT NULL,
        Turbidez                DECIMAL(9,2)  NOT NULL,
        CondicionesClimaticas   NVARCHAR(500) NULL,
        Observaciones           NVARCHAR(500) NULL,
        FechaCreacion           DATETIME2     NOT NULL CONSTRAINT DF_ParametrosAgua_FechaCreacion DEFAULT (SYSUTCDATETIME()),
        FechaModificacion       DATETIME2     NULL,
        CONSTRAINT FK_ParametrosAgua_Lotes FOREIGN KEY (LoteId) REFERENCES dbo.Lotes (Id)
    );
    CREATE INDEX IX_ParametrosAgua_LoteId_Fecha ON dbo.ParametrosAgua (LoteId, FechaRegistro DESC);
END
GO

IF OBJECT_ID(N'dbo.InventarioItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InventarioItems (
        Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_InventarioItems PRIMARY KEY,
        Nombre          NVARCHAR(80)  NOT NULL,
        Categoria       NVARCHAR(30)  NOT NULL,
        StockKg         DECIMAL(18,3) NOT NULL,
        StockMinimoKg   DECIMAL(18,3) NOT NULL,
        CONSTRAINT UQ_InventarioItems_Nombre UNIQUE (Nombre)
    );
END
GO

IF OBJECT_ID(N'dbo.RegistrosAlimentacion', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.RegistrosAlimentacion (
        Id                  INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_RegistrosAlimentacion PRIMARY KEY,
        LoteId              INT           NOT NULL,
        FechaRegistro       DATETIME2     NOT NULL,
        Horario             TIME          NOT NULL,
        TipoAlimento        NVARCHAR(80)  NOT NULL,
        CantidadKg          DECIMAL(10,3) NOT NULL,
        InventarioItemId    INT           NULL,
        CONSTRAINT FK_RegistrosAlimentacion_Lotes FOREIGN KEY (LoteId) REFERENCES dbo.Lotes (Id),
        CONSTRAINT FK_RegistrosAlimentacion_Inventario FOREIGN KEY (InventarioItemId) REFERENCES dbo.InventarioItems (Id)
    );
    CREATE INDEX IX_RegistrosAlimentacion_Lote_Fecha ON dbo.RegistrosAlimentacion (LoteId, FechaRegistro DESC);
END
GO

IF OBJECT_ID(N'dbo.AlimentoInventarios', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.AlimentoInventarios (
        Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AlimentoInventarios PRIMARY KEY,
        Nombre          NVARCHAR(80)  NOT NULL,
        TipoFase        NVARCHAR(30)  NOT NULL,
        StockKg         DECIMAL(18,3) NOT NULL,
        StockMinimoKg   DECIMAL(18,3) NOT NULL,
        CONSTRAINT UQ_AlimentoInventarios_Nombre UNIQUE (Nombre)
    );
END
GO

/* ---------- Función: siguiente código LOT-AAAA-NNN ---------- */
IF OBJECT_ID(N'dbo.fn_GenerarCodigoLoteSiguiente', N'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_GenerarCodigoLoteSiguiente;
GO

CREATE FUNCTION dbo.fn_GenerarCodigoLoteSiguiente ()
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @anio INT = YEAR(CONVERT(date, SYSUTCDATETIME()));
    DECLARE @prefijo NVARCHAR(12) = N'LOT-' + CAST(@anio AS NVARCHAR(4)) + N'-';
    DECLARE @maxNum INT = 0;

    SELECT @maxNum = MAX(
        TRY_CONVERT(INT, SUBSTRING(Codigo, LEN(@prefijo) + 1, 10)))
    FROM dbo.Lotes
    WHERE Codigo LIKE @prefijo + N'%';

    IF @maxNum IS NULL SET @maxNum = 0;

    RETURN @prefijo + RIGHT(N'000' + CAST(@maxNum + 1 AS NVARCHAR(10)), 3);
END;
GO

/* ---------- Lotes ---------- */
IF OBJECT_ID(N'dbo.sp_Lote_Insertar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_Lote_Insertar;
GO

CREATE PROCEDURE dbo.sp_Lote_Insertar
    @Codigo             NVARCHAR(20) = NULL,
    @FechaSiembra       DATETIME2,
    @CantidadInicial    INT,
    @EspecieId          INT,
    @EstanqueId         INT,
    @ProveedorId        INT,
    @FaseActual         INT = 1,
    @Estado             INT = 1,
    @IdNuevo            INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @Codigo IS NULL OR LTRIM(RTRIM(@Codigo)) = N''
        SET @Codigo = dbo.fn_GenerarCodigoLoteSiguiente();

    INSERT INTO dbo.Lotes (
        Codigo, FechaSiembra, CantidadInicial, CantidadActual,
        EspecieId, EstanqueId, ProveedorId, FaseActual, Estado,
        FechaCreacion, FechaModificacion
    )
    VALUES (
        @Codigo, @FechaSiembra, @CantidadInicial, @CantidadInicial,
        @EspecieId, @EstanqueId, @ProveedorId, @FaseActual, @Estado,
        SYSUTCDATETIME(), SYSUTCDATETIME()
    );

    SET @IdNuevo = CAST(SCOPE_IDENTITY() AS INT);
END;
GO

IF OBJECT_ID(N'dbo.sp_Lote_Actualizar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_Lote_Actualizar;
GO

CREATE PROCEDURE dbo.sp_Lote_Actualizar
    @Id                 INT,
    @FechaSiembra       DATETIME2,
    @CantidadInicial    INT,
    @EspecieId          INT,
    @EstanqueId         INT,
    @ProveedorId        INT,
    @FaseActual         INT,
    @FilasAfectadas     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Lotes
    SET FechaSiembra      = @FechaSiembra,
        CantidadInicial   = @CantidadInicial,
        EspecieId         = @EspecieId,
        EstanqueId        = @EstanqueId,
        ProveedorId       = @ProveedorId,
        FaseActual        = @FaseActual,
        FechaModificacion = SYSUTCDATETIME()
    WHERE Id = @Id
      AND Estado <> 2;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_Lote_Anular', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_Lote_Anular;
GO

CREATE PROCEDURE dbo.sp_Lote_Anular
    @Id                 INT,
    @FilasAfectadas     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Lotes
    SET Estado = 2,
        FechaModificacion = SYSUTCDATETIME()
    WHERE Id = @Id AND Estado <> 2;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_Lote_ObtenerPorId', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_Lote_ObtenerPorId;
GO

CREATE PROCEDURE dbo.sp_Lote_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Codigo, FechaSiembra, CantidadInicial, CantidadActual,
           EspecieId, EstanqueId, ProveedorId, FaseActual, Estado,
           FechaCreacion, FechaModificacion
    FROM dbo.Lotes
    WHERE Id = @Id;
END;
GO

IF OBJECT_ID(N'dbo.sp_Lote_Listar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_Lote_Listar;
GO

CREATE PROCEDURE dbo.sp_Lote_Listar
    @Estado     INT = NULL,
    @EspecieId  INT = NULL,
    @EstanqueId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Codigo, FechaSiembra, CantidadInicial, CantidadActual,
           EspecieId, EstanqueId, ProveedorId, FaseActual, Estado,
           FechaCreacion, FechaModificacion
    FROM dbo.Lotes
    WHERE (@Estado IS NULL OR Estado = @Estado)
      AND (@EspecieId IS NULL OR EspecieId = @EspecieId)
      AND (@EstanqueId IS NULL OR EstanqueId = @EstanqueId)
    ORDER BY Codigo;
END;
GO

/* ---------- ParametrosAgua ---------- */
IF OBJECT_ID(N'dbo.sp_ParametroAgua_Insertar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_ParametroAgua_Insertar;
GO

CREATE PROCEDURE dbo.sp_ParametroAgua_Insertar
    @LoteId                 INT,
    @FechaRegistro          DATETIME2,
    @Temperatura            DECIMAL(9,2),
    @Ph                     DECIMAL(9,2),
    @OxigenoDisuelto        DECIMAL(9,2),
    @Amonio                 DECIMAL(9,3),
    @Nitritos               DECIMAL(9,3),
    @Turbidez               DECIMAL(9,2),
    @CondicionesClimaticas  NVARCHAR(500) = NULL,
    @Observaciones          NVARCHAR(500) = NULL,
    @IdNuevo                INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.ParametrosAgua (
        LoteId, FechaRegistro, Temperatura, Ph, OxigenoDisuelto,
        Amonio, Nitritos, Turbidez, CondicionesClimaticas, Observaciones,
        FechaCreacion, FechaModificacion
    )
    VALUES (
        @LoteId, @FechaRegistro, @Temperatura, @Ph, @OxigenoDisuelto,
        @Amonio, @Nitritos, @Turbidez, @CondicionesClimaticas, @Observaciones,
        SYSUTCDATETIME(), NULL
    );

    SET @IdNuevo = CAST(SCOPE_IDENTITY() AS INT);
END;
GO

IF OBJECT_ID(N'dbo.sp_ParametroAgua_Actualizar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_ParametroAgua_Actualizar;
GO

CREATE PROCEDURE dbo.sp_ParametroAgua_Actualizar
    @Id                     INT,
    @LoteId                 INT,
    @FechaRegistro          DATETIME2,
    @Temperatura            DECIMAL(9,2),
    @Ph                     DECIMAL(9,2),
    @OxigenoDisuelto        DECIMAL(9,2),
    @Amonio                 DECIMAL(9,3),
    @Nitritos               DECIMAL(9,3),
    @Turbidez               DECIMAL(9,2),
    @CondicionesClimaticas  NVARCHAR(500) = NULL,
    @Observaciones          NVARCHAR(500) = NULL,
    @FilasAfectadas         INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ParametrosAgua
    SET LoteId                = @LoteId,
        FechaRegistro         = @FechaRegistro,
        Temperatura           = @Temperatura,
        Ph                    = @Ph,
        OxigenoDisuelto       = @OxigenoDisuelto,
        Amonio                = @Amonio,
        Nitritos              = @Nitritos,
        Turbidez              = @Turbidez,
        CondicionesClimaticas = @CondicionesClimaticas,
        Observaciones         = @Observaciones,
        FechaModificacion     = SYSUTCDATETIME()
    WHERE Id = @Id;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_ParametroAgua_Eliminar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_ParametroAgua_Eliminar;
GO

CREATE PROCEDURE dbo.sp_ParametroAgua_Eliminar
    @Id             INT,
    @FilasAfectadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.ParametrosAgua WHERE Id = @Id;
    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_ParametroAgua_ObtenerPorId', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_ParametroAgua_ObtenerPorId;
GO

CREATE PROCEDURE dbo.sp_ParametroAgua_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, LoteId, FechaRegistro, Temperatura, Ph, OxigenoDisuelto,
           Amonio, Nitritos, Turbidez, CondicionesClimaticas, Observaciones,
           FechaCreacion, FechaModificacion
    FROM dbo.ParametrosAgua
    WHERE Id = @Id;
END;
GO

IF OBJECT_ID(N'dbo.sp_ParametroAgua_ListarFiltrado', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_ParametroAgua_ListarFiltrado;
GO

CREATE PROCEDURE dbo.sp_ParametroAgua_ListarFiltrado
    @LoteId     INT = NULL,
    @Desde      DATETIME2 = NULL,
    @Hasta      DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @fin DATETIME2 = NULL;
    IF @Hasta IS NOT NULL
        SET @fin = DATEADD(DAY, 1, CONVERT(date, @Hasta));

    SELECT p.Id, p.LoteId, p.FechaRegistro, p.Temperatura, p.Ph, p.OxigenoDisuelto,
           p.Amonio, p.Nitritos, p.Turbidez, p.CondicionesClimaticas, p.Observaciones,
           p.FechaCreacion, p.FechaModificacion
    FROM dbo.ParametrosAgua p
    WHERE (@LoteId IS NULL OR p.LoteId = @LoteId)
      AND (@Desde IS NULL OR p.FechaRegistro >= @Desde)
      AND (@fin IS NULL OR p.FechaRegistro < @fin)
    ORDER BY p.FechaRegistro DESC;
END;
GO

IF OBJECT_ID(N'dbo.sp_ParametroAgua_ListarPorEstanque', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_ParametroAgua_ListarPorEstanque;
GO

CREATE PROCEDURE dbo.sp_ParametroAgua_ListarPorEstanque
    @EstanqueId INT,
    @Desde      DATETIME2 = NULL,
    @Hasta      DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @fin DATETIME2 = NULL;
    IF @Hasta IS NOT NULL
        SET @fin = DATEADD(DAY, 1, CONVERT(date, @Hasta));

    SELECT p.Id, p.LoteId, p.FechaRegistro, p.Temperatura, p.Ph, p.OxigenoDisuelto,
           p.Amonio, p.Nitritos, p.Turbidez, p.CondicionesClimaticas, p.Observaciones,
           p.FechaCreacion, p.FechaModificacion
    FROM dbo.ParametrosAgua p
    INNER JOIN dbo.Lotes l ON l.Id = p.LoteId
    WHERE l.EstanqueId = @EstanqueId
      AND (@Desde IS NULL OR p.FechaRegistro >= @Desde)
      AND (@fin IS NULL OR p.FechaRegistro < @fin)
    ORDER BY p.FechaRegistro DESC;
END;
GO

/* ---------- InventarioItems ---------- */
IF OBJECT_ID(N'dbo.sp_InventarioItem_Insertar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_InventarioItem_Insertar;
GO

CREATE PROCEDURE dbo.sp_InventarioItem_Insertar
    @Nombre         NVARCHAR(80),
    @Categoria      NVARCHAR(30),
    @StockKg        DECIMAL(18,3),
    @StockMinimoKg  DECIMAL(18,3),
    @IdNuevo        INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.InventarioItems (Nombre, Categoria, StockKg, StockMinimoKg)
    VALUES (@Nombre, @Categoria, @StockKg, @StockMinimoKg);

    SET @IdNuevo = CAST(SCOPE_IDENTITY() AS INT);
END;
GO

IF OBJECT_ID(N'dbo.sp_InventarioItem_Actualizar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_InventarioItem_Actualizar;
GO

CREATE PROCEDURE dbo.sp_InventarioItem_Actualizar
    @Id             INT,
    @Nombre         NVARCHAR(80),
    @Categoria      NVARCHAR(30),
    @StockKg        DECIMAL(18,3),
    @StockMinimoKg  DECIMAL(18,3),
    @FilasAfectadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.InventarioItems
    SET Nombre = @Nombre,
        Categoria = @Categoria,
        StockKg = @StockKg,
        StockMinimoKg = @StockMinimoKg
    WHERE Id = @Id;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_InventarioItem_ObtenerPorId', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_InventarioItem_ObtenerPorId;
GO

CREATE PROCEDURE dbo.sp_InventarioItem_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Categoria, StockKg, StockMinimoKg
    FROM dbo.InventarioItems
    WHERE Id = @Id;
END;
GO

IF OBJECT_ID(N'dbo.sp_InventarioItem_Listar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_InventarioItem_Listar;
GO

CREATE PROCEDURE dbo.sp_InventarioItem_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Categoria, StockKg, StockMinimoKg
    FROM dbo.InventarioItems
    ORDER BY Nombre;
END;
GO

IF OBJECT_ID(N'dbo.sp_InventarioItem_AgregarStock', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_InventarioItem_AgregarStock;
GO

CREATE PROCEDURE dbo.sp_InventarioItem_AgregarStock
    @Id             INT,
    @CantidadKg     DECIMAL(18,3),
    @FilasAfectadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.InventarioItems
    SET StockKg = StockKg + @CantidadKg
    WHERE Id = @Id;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_InventarioItem_DescontarStock', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_InventarioItem_DescontarStock;
GO

CREATE PROCEDURE dbo.sp_InventarioItem_DescontarStock
    @Id             INT,
    @CantidadKg     DECIMAL(18,3),
    @FilasAfectadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.InventarioItems
    SET StockKg = StockKg - @CantidadKg
    WHERE Id = @Id AND StockKg >= @CantidadKg;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

/* ---------- RegistrosAlimentacion ---------- */
IF OBJECT_ID(N'dbo.sp_RegistroAlimentacion_Insertar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_RegistroAlimentacion_Insertar;
GO

CREATE PROCEDURE dbo.sp_RegistroAlimentacion_Insertar
    @LoteId             INT,
    @FechaRegistro      DATETIME2,
    @Horario            TIME,
    @TipoAlimento       NVARCHAR(80),
    @CantidadKg         DECIMAL(10,3),
    @InventarioItemId   INT = NULL,
    @IdNuevo            INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.RegistrosAlimentacion (
        LoteId, FechaRegistro, Horario, TipoAlimento, CantidadKg, InventarioItemId
    )
    VALUES (
        @LoteId, @FechaRegistro, @Horario, @TipoAlimento, @CantidadKg, @InventarioItemId
    );

    SET @IdNuevo = CAST(SCOPE_IDENTITY() AS INT);
END;
GO

IF OBJECT_ID(N'dbo.sp_RegistroAlimentacion_Actualizar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_RegistroAlimentacion_Actualizar;
GO

CREATE PROCEDURE dbo.sp_RegistroAlimentacion_Actualizar
    @Id                 INT,
    @LoteId             INT,
    @FechaRegistro      DATETIME2,
    @Horario            TIME,
    @TipoAlimento       NVARCHAR(80),
    @CantidadKg         DECIMAL(10,3),
    @InventarioItemId   INT = NULL,
    @FilasAfectadas     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.RegistrosAlimentacion
    SET LoteId = @LoteId,
        FechaRegistro = @FechaRegistro,
        Horario = @Horario,
        TipoAlimento = @TipoAlimento,
        CantidadKg = @CantidadKg,
        InventarioItemId = @InventarioItemId
    WHERE Id = @Id;

    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_RegistroAlimentacion_Eliminar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_RegistroAlimentacion_Eliminar;
GO

CREATE PROCEDURE dbo.sp_RegistroAlimentacion_Eliminar
    @Id             INT,
    @FilasAfectadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.RegistrosAlimentacion WHERE Id = @Id;
    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_RegistroAlimentacion_ObtenerPorId', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_RegistroAlimentacion_ObtenerPorId;
GO

CREATE PROCEDURE dbo.sp_RegistroAlimentacion_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, LoteId, FechaRegistro, Horario, TipoAlimento, CantidadKg, InventarioItemId
    FROM dbo.RegistrosAlimentacion
    WHERE Id = @Id;
END;
GO

IF OBJECT_ID(N'dbo.sp_RegistroAlimentacion_ListarPorLote', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_RegistroAlimentacion_ListarPorLote;
GO

CREATE PROCEDURE dbo.sp_RegistroAlimentacion_ListarPorLote
    @LoteId INT,
    @Fecha  DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, LoteId, FechaRegistro, Horario, TipoAlimento, CantidadKg, InventarioItemId
    FROM dbo.RegistrosAlimentacion
    WHERE LoteId = @LoteId
      AND (@Fecha IS NULL OR CONVERT(date, FechaRegistro) = @Fecha)
    ORDER BY FechaRegistro DESC, Horario;
END;
GO

/* ---------- AlimentoInventario (modelo alternativo) ---------- */
IF OBJECT_ID(N'dbo.sp_AlimentoInventario_Insertar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_AlimentoInventario_Insertar;
GO

CREATE PROCEDURE dbo.sp_AlimentoInventario_Insertar
    @Nombre         NVARCHAR(80),
    @TipoFase       NVARCHAR(30),
    @StockKg        DECIMAL(18,3),
    @StockMinimoKg  DECIMAL(18,3),
    @IdNuevo        INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.AlimentoInventarios (Nombre, TipoFase, StockKg, StockMinimoKg)
    VALUES (@Nombre, @TipoFase, @StockKg, @StockMinimoKg);
    SET @IdNuevo = CAST(SCOPE_IDENTITY() AS INT);
END;
GO

IF OBJECT_ID(N'dbo.sp_AlimentoInventario_Actualizar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_AlimentoInventario_Actualizar;
GO

CREATE PROCEDURE dbo.sp_AlimentoInventario_Actualizar
    @Id             INT,
    @Nombre         NVARCHAR(80),
    @TipoFase       NVARCHAR(30),
    @StockKg        DECIMAL(18,3),
    @StockMinimoKg  DECIMAL(18,3),
    @FilasAfectadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.AlimentoInventarios
    SET Nombre = @Nombre, TipoFase = @TipoFase, StockKg = @StockKg, StockMinimoKg = @StockMinimoKg
    WHERE Id = @Id;
    SET @FilasAfectadas = @@ROWCOUNT;
END;
GO

IF OBJECT_ID(N'dbo.sp_AlimentoInventario_ObtenerPorId', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_AlimentoInventario_ObtenerPorId;
GO

CREATE PROCEDURE dbo.sp_AlimentoInventario_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, TipoFase, StockKg, StockMinimoKg FROM dbo.AlimentoInventarios WHERE Id = @Id;
END;
GO

IF OBJECT_ID(N'dbo.sp_AlimentoInventario_Listar', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_AlimentoInventario_Listar;
GO

CREATE PROCEDURE dbo.sp_AlimentoInventario_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, TipoFase, StockKg, StockMinimoKg FROM dbo.AlimentoInventarios ORDER BY Nombre;
END;
GO
