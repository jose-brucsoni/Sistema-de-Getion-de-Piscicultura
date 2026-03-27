/*
  Script de creación de tablas — Sistema de Gestión de Piscicultura
  Fuente: Documentacion/Diagramas/Diagrama_Logico_y_Fisico.md
  Alcance: Sprint 1 (auth, catálogos, lotes, parámetros de agua, inventario, alimentación, alertas)
           y Sprint 2 (mortalidad, muestreo, tratamientos, traslados, biomasa; tendencias HU-010 sobre ParametroAgua)

  Motor: SQL Server (T-SQL)
  Antes de ejecutar: crear o seleccionar la base de datos, por ejemplo:
    CREATE DATABASE Piscicultura;
    GO
    USE Piscicultura;
    GO
*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ---------- Catálogo: roles y usuarios (HU-018, HU-019) ---------- */
IF OBJECT_ID(N'dbo.Rol', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Rol (
        IdRol           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Rol PRIMARY KEY,
        Nombre          NVARCHAR(50)  NOT NULL,
        Descripcion     NVARCHAR(150) NULL,
        Activo          BIT NOT NULL CONSTRAINT DF_Rol_Activo DEFAULT (1),
        CONSTRAINT UQ_Rol_Nombre UNIQUE (Nombre)
    );
END
GO

IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuario (
        IdUsuario       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Usuario PRIMARY KEY,
        IdRol           INT NOT NULL,
        Username        NVARCHAR(50)  NOT NULL,
        Correo          NVARCHAR(120) NOT NULL,
        PasswordHash    NVARCHAR(255) NOT NULL,
        Activo          BIT NOT NULL CONSTRAINT DF_Usuario_Activo DEFAULT (1),
        CreatedAt       DATETIME2 NOT NULL CONSTRAINT DF_Usuario_CreatedAt DEFAULT (SYSUTCDATETIME()),
        CONSTRAINT FK_Usuario_Rol FOREIGN KEY (IdRol) REFERENCES dbo.Rol (IdRol),
        CONSTRAINT UQ_Usuario_Username UNIQUE (Username),
        CONSTRAINT UQ_Usuario_Correo UNIQUE (Correo)
    );
    CREATE INDEX IX_Usuario_IdRol ON dbo.Usuario (IdRol);
END
GO

/* ---------- Catálogos (HU-003 a HU-005) ---------- */
IF OBJECT_ID(N'dbo.Proveedor', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Proveedor (
        IdProveedor             INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Proveedor PRIMARY KEY,
        Nombre                  NVARCHAR(120) NOT NULL,
        CertificacionSanitaria  NVARCHAR(120) NULL,
        Contacto                NVARCHAR(120) NULL,
        Activo                  BIT NOT NULL CONSTRAINT DF_Proveedor_Activo DEFAULT (1)
    );
END
GO

IF OBJECT_ID(N'dbo.Especie', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Especie (
        IdEspecie           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Especie PRIMARY KEY,
        Nombre              NVARCHAR(80) NOT NULL,
        TempMin             DECIMAL(5,2) NOT NULL,
        TempMax             DECIMAL(5,2) NOT NULL,
        PhMin               DECIMAL(4,2) NOT NULL,
        PhMax               DECIMAL(4,2) NOT NULL,
        OxigenoMin          DECIMAL(6,2) NOT NULL,
        PesoComercialMin    DECIMAL(8,2) NOT NULL,
        PesoComercialMax    DECIMAL(8,2) NOT NULL,
        Activo              BIT NOT NULL CONSTRAINT DF_Especie_Activo DEFAULT (1),
        CONSTRAINT UQ_Especie_Nombre UNIQUE (Nombre)
    );
END
GO

IF OBJECT_ID(N'dbo.Estanque', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Estanque (
        IdEstanque      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Estanque PRIMARY KEY,
        Codigo          NVARCHAR(30) NOT NULL,
        Tipo            NVARCHAR(20) NULL,
        VolumenM3       DECIMAL(10,2) NULL,
        CapacidadMaxima INT NOT NULL,
        Estado          NVARCHAR(20) NULL,
        Activo          BIT NOT NULL CONSTRAINT DF_Estanque_Activo DEFAULT (1),
        CONSTRAINT UQ_Estanque_Codigo UNIQUE (Codigo)
    );
END
GO

/* Fases de cría (HU-001, HU-011 frecuencia; HU-015 traslado entre fases) */
IF OBJECT_ID(N'dbo.FaseCrianza', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.FaseCrianza (
        IdFase                      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_FaseCrianza PRIMARY KEY,
        Nombre                      NVARCHAR(40) NOT NULL,
        Orden                       INT NOT NULL,
        EdadMinMeses                INT NULL,
        EdadMaxMeses                INT NULL,
        FrecuenciaAlimentacionDia   INT NULL,
        CONSTRAINT UQ_FaseCrianza_Nombre UNIQUE (Nombre)
    );
END
GO

/* ---------- Lote (HU-001, HU-002) ---------- */
IF OBJECT_ID(N'dbo.Lote', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Lote (
        IdLote          INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Lote PRIMARY KEY,
        IdEspecie       INT NOT NULL,
        IdEstanque      INT NOT NULL,
        IdProveedor     INT NOT NULL,
        IdFaseActual    INT NOT NULL,
        Codigo          NVARCHAR(40) NOT NULL,
        FechaSiembra    DATE NOT NULL,
        CantidadInicial INT NOT NULL,
        CantidadActual  INT NOT NULL,
        Estado          NVARCHAR(20) NOT NULL,
        CONSTRAINT FK_Lote_Especie FOREIGN KEY (IdEspecie) REFERENCES dbo.Especie (IdEspecie),
        CONSTRAINT FK_Lote_Estanque FOREIGN KEY (IdEstanque) REFERENCES dbo.Estanque (IdEstanque),
        CONSTRAINT FK_Lote_Proveedor FOREIGN KEY (IdProveedor) REFERENCES dbo.Proveedor (IdProveedor),
        CONSTRAINT FK_Lote_FaseCrianza FOREIGN KEY (IdFaseActual) REFERENCES dbo.FaseCrianza (IdFase),
        CONSTRAINT UQ_Lote_Codigo UNIQUE (Codigo)
    );
    CREATE INDEX IX_Lote_IdEspecie ON dbo.Lote (IdEspecie);
    CREATE INDEX IX_Lote_IdEstanque ON dbo.Lote (IdEstanque);
    CREATE INDEX IX_Lote_IdFaseActual ON dbo.Lote (IdFaseActual);
END
GO

/* ---------- Parámetros de agua (HU-009, HU-010 tendencias) ---------- */
IF OBJECT_ID(N'dbo.ParametroAgua', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ParametroAgua (
        IdParametro         INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ParametroAgua PRIMARY KEY,
        IdLote              INT NOT NULL,
        IdUsuario           INT NULL,
        FechaRegistro       DATETIME2 NOT NULL,
        Temperatura         DECIMAL(5,2) NOT NULL,
        Ph                  DECIMAL(4,2) NOT NULL,
        OxigenoDisuelto     DECIMAL(6,2) NOT NULL,
        Amonio              DECIMAL(6,3) NOT NULL,
        Nitritos            DECIMAL(6,3) NOT NULL,
        Turbidez            DECIMAL(7,2) NOT NULL,
        CondicionesClimaticas NVARCHAR(500) NULL,
        Observaciones       NVARCHAR(500) NULL,
        CONSTRAINT FK_ParametroAgua_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_ParametroAgua_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario)
    );
    CREATE INDEX IX_ParametroAgua_IdLote_Fecha ON dbo.ParametroAgua (IdLote, FechaRegistro DESC);
END
GO

/* ---------- Inventario y alimentación (HU-006, HU-011, HU-012) ---------- */
IF OBJECT_ID(N'dbo.InventarioAlimento', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InventarioAlimento (
        IdInventario        INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_InventarioAlimento PRIMARY KEY,
        TipoAlimento        NVARCHAR(80) NOT NULL,
        StockActualKg       DECIMAL(12,3) NOT NULL,
        StockMinimoKg       DECIMAL(12,3) NOT NULL,
        FechaActualizacion  DATETIME2 NOT NULL CONSTRAINT DF_InventarioAlimento_FechaAct DEFAULT (SYSUTCDATETIME()),
        Activo              BIT NOT NULL CONSTRAINT DF_InventarioAlimento_Activo DEFAULT (1),
        CONSTRAINT UQ_InventarioAlimento_Tipo UNIQUE (TipoAlimento)
    );
END
GO

IF OBJECT_ID(N'dbo.RegistroAlimentacion', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.RegistroAlimentacion (
        IdAlimentacion  INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_RegistroAlimentacion PRIMARY KEY,
        IdLote          INT NOT NULL,
        IdUsuario       INT NULL,
        IdInventario    INT NULL,
        FechaRegistro   DATETIME2 NOT NULL,
        Horario         TIME NOT NULL,
        TipoAlimento    NVARCHAR(80) NOT NULL,
        CantidadKg      DECIMAL(10,3) NOT NULL,
        Observacion     NVARCHAR(500) NULL,
        CONSTRAINT FK_RegistroAlimentacion_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_RegistroAlimentacion_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario),
        CONSTRAINT FK_RegistroAlimentacion_Inventario FOREIGN KEY (IdInventario) REFERENCES dbo.InventarioAlimento (IdInventario)
    );
    CREATE INDEX IX_RegistroAlimentacion_Lote_Fecha ON dbo.RegistroAlimentacion (IdLote, FechaRegistro DESC);
END
GO

/* ---------- Alertas (HU-009, HU-006 stock bajo) ---------- */
IF OBJECT_ID(N'dbo.Alerta', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Alerta (
        IdAlerta    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Alerta PRIMARY KEY,
        IdLote      INT NULL,
        FechaHora   DATETIME2 NOT NULL,
        Tipo        NVARCHAR(40) NOT NULL,
        Nivel       NVARCHAR(20) NULL,
        Mensaje     NVARCHAR(250) NOT NULL,
        Atendida    BIT NOT NULL CONSTRAINT DF_Alerta_Atendida DEFAULT (0),
        IdParametroAgua   INT NULL,
        IdInventario      INT NULL,
        CONSTRAINT FK_Alerta_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_Alerta_ParametroAgua FOREIGN KEY (IdParametroAgua) REFERENCES dbo.ParametroAgua (IdParametro),
        CONSTRAINT FK_Alerta_Inventario FOREIGN KEY (IdInventario) REFERENCES dbo.InventarioAlimento (IdInventario)
    );
    CREATE INDEX IX_Alerta_IdLote_Fecha ON dbo.Alerta (IdLote, FechaHora DESC);
END
GO

/* ---------- Sprint 2: mortalidad (HU-007) ---------- */
IF OBJECT_ID(N'dbo.Mortalidad', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Mortalidad (
        IdMortalidad    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Mortalidad PRIMARY KEY,
        IdLote          INT NOT NULL,
        IdUsuario       INT NULL,
        FechaRegistro   DATETIME2 NOT NULL,
        Cantidad        INT NOT NULL,
        Causa           NVARCHAR(120) NULL,
        Observacion     NVARCHAR(500) NULL,
        CONSTRAINT FK_Mortalidad_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_Mortalidad_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario)
    );
    CREATE INDEX IX_Mortalidad_IdLote_Fecha ON dbo.Mortalidad (IdLote, FechaRegistro DESC);
END
GO

/* ---------- Sprint 2: muestreo peso/talla (HU-008); base para biomasa (HU-016) ---------- */
IF OBJECT_ID(N'dbo.Muestreo', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Muestreo (
        IdMuestreo      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Muestreo PRIMARY KEY,
        IdLote          INT NOT NULL,
        IdUsuario       INT NULL,
        FechaRegistro   DATETIME2 NOT NULL,
        CantidadMuestra INT NOT NULL,
        PesoPromedio    DECIMAL(10,3) NOT NULL,
        Dispersion      NVARCHAR(40) NULL,
        CONSTRAINT FK_Muestreo_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_Muestreo_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario)
    );
    CREATE INDEX IX_Muestreo_IdLote_Fecha ON dbo.Muestreo (IdLote, FechaRegistro DESC);
END
GO

/* ---------- Sprint 2: tratamientos (HU-013, HU-014) ---------- */
IF OBJECT_ID(N'dbo.Tratamiento', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tratamiento (
        IdTratamiento   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Tratamiento PRIMARY KEY,
        IdLote          INT NOT NULL,
        IdUsuario       INT NULL,
        FechaAplicacion DATETIME2 NOT NULL,
        Tipo            NVARCHAR(80) NOT NULL,
        Dosis           DECIMAL(12,3) NOT NULL,
        Observacion     NVARCHAR(500) NULL,
        CONSTRAINT FK_Tratamiento_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_Tratamiento_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario)
    );
    CREATE INDEX IX_Tratamiento_IdLote_Fecha ON dbo.Tratamiento (IdLote, FechaAplicacion DESC);
END
GO

/* ---------- Sprint 2: traslado entre fases/estanques (HU-015) ---------- */
IF OBJECT_ID(N'dbo.TrasladoLote', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TrasladoLote (
        IdTraslado              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TrasladoLote PRIMARY KEY,
        IdLote                  INT NOT NULL,
        IdEstanqueOrigen        INT NOT NULL,
        IdEstanqueDestino       INT NOT NULL,
        IdFaseOrigen            INT NOT NULL,
        IdFaseDestino           INT NOT NULL,
        FechaTraslado           DATETIME2 NOT NULL,
        CantidadTrasladada      INT NOT NULL,
        MortalidadEnTraslado    INT NOT NULL CONSTRAINT DF_TrasladoLote_Mortalidad DEFAULT (0),
        PesoPromedioGr          DECIMAL(10,3) NULL,
        IdUsuario               INT NULL,
        Observaciones           NVARCHAR(500) NULL,
        CONSTRAINT FK_TrasladoLote_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_TrasladoLote_EstanqueOrigen FOREIGN KEY (IdEstanqueOrigen) REFERENCES dbo.Estanque (IdEstanque),
        CONSTRAINT FK_TrasladoLote_EstanqueDestino FOREIGN KEY (IdEstanqueDestino) REFERENCES dbo.Estanque (IdEstanque),
        CONSTRAINT FK_TrasladoLote_FaseOrigen FOREIGN KEY (IdFaseOrigen) REFERENCES dbo.FaseCrianza (IdFase),
        CONSTRAINT FK_TrasladoLote_FaseDestino FOREIGN KEY (IdFaseDestino) REFERENCES dbo.FaseCrianza (IdFase),
        CONSTRAINT FK_TrasladoLote_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario)
    );
    CREATE INDEX IX_TrasladoLote_IdLote ON dbo.TrasladoLote (IdLote, FechaTraslado DESC);
END
GO

/* ---------- Sprint 2: inventario de peces / biomasa (HU-016) ---------- */
IF OBJECT_ID(N'dbo.RegistroBiomasa', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.RegistroBiomasa (
        IdRegistro          INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_RegistroBiomasa PRIMARY KEY,
        IdLote              INT NOT NULL,
        FechaRegistro       DATETIME2 NOT NULL,
        CantidadPeces       INT NOT NULL,
        BiomasaEstimadaKg   DECIMAL(14,3) NOT NULL,
        IdUsuario           INT NULL,
        IdMuestreoOrigen    INT NULL,
        Observaciones       NVARCHAR(500) NULL,
        CONSTRAINT FK_RegistroBiomasa_Lote FOREIGN KEY (IdLote) REFERENCES dbo.Lote (IdLote),
        CONSTRAINT FK_RegistroBiomasa_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario),
        CONSTRAINT FK_RegistroBiomasa_Muestreo FOREIGN KEY (IdMuestreoOrigen) REFERENCES dbo.Muestreo (IdMuestreo)
    );
    CREATE INDEX IX_RegistroBiomasa_IdLote_Fecha ON dbo.RegistroBiomasa (IdLote, FechaRegistro DESC);
END
GO

/*
  Datos mínimos de referencia (roles y fases). Ajustar o eliminar si ya existen datos.
*/
IF NOT EXISTS (SELECT 1 FROM dbo.Rol)
BEGIN
    SET IDENTITY_INSERT dbo.Rol ON;
    INSERT INTO dbo.Rol (IdRol, Nombre, Descripcion, Activo) VALUES
        (1, N'Administrador', N'Acceso completo al sistema', 1),
        (2, N'Tecnico', N'Operación diaria en piscicultura', 1),
        (3, N'Supervisor', N'Supervisión y reportes', 1);
    SET IDENTITY_INSERT dbo.Rol OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.FaseCrianza)
BEGIN
    INSERT INTO dbo.FaseCrianza (Nombre, Orden, EdadMinMeses, EdadMaxMeses, FrecuenciaAlimentacionDia) VALUES
        (N'Alevinaje',        1, 0, 2, 6),
        (N'Juveniles',        2, 2, 4, 4),
        (N'Pre-engorde',      3, 4, 6, 3),
        (N'Engorde final',    4, 6, NULL, 3);
END
GO
