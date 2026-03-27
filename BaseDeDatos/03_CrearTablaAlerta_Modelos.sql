/*
  Tabla de alertas para el esquema usado por los servicios C# (tablas en plural):
  - dbo.Lotes
  - dbo.ParametrosAgua
  - dbo.InventarioItems
*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.Alerta', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Alerta (
        IdAlerta         INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Alerta PRIMARY KEY,
        IdLote           INT NULL,
        FechaHora        DATETIME2 NOT NULL CONSTRAINT DF_Alerta_FechaHora DEFAULT (SYSUTCDATETIME()),
        Tipo             NVARCHAR(40) NOT NULL,
        Nivel            NVARCHAR(20) NULL,
        Mensaje          NVARCHAR(250) NOT NULL,
        Atendida         BIT NOT NULL CONSTRAINT DF_Alerta_Atendida DEFAULT (0),
        IdParametroAgua  INT NULL,
        IdInventario     INT NULL,
        CONSTRAINT FK_Alerta_Lotes FOREIGN KEY (IdLote) REFERENCES dbo.Lotes (Id),
        CONSTRAINT FK_Alerta_ParametrosAgua FOREIGN KEY (IdParametroAgua) REFERENCES dbo.ParametrosAgua (Id),
        CONSTRAINT FK_Alerta_InventarioItems FOREIGN KEY (IdInventario) REFERENCES dbo.InventarioItems (Id)
    );

    CREATE INDEX IX_Alerta_Lote_Fecha ON dbo.Alerta (IdLote, FechaHora DESC);
    CREATE INDEX IX_Alerta_Atendida_Fecha ON dbo.Alerta (Atendida, FechaHora DESC);
END
GO
