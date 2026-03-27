/*
  Solución típica al error:
  "No se puede abrir la base de datos 'Piscicultura' solicitada por el inicio de sesión"

  Ejecutar en SSMS con una cuenta con permisos (sysadmin o similar).

  1) Cambia la variable @LoginWindows al usuario exacto del mensaje de error.
  2) Ejecuta el script completo.
  3) Verifica que la cadena de conexión apunte a la misma instancia donde ejecutaste el script
     (localhost, localhost\SQLEXPRESS, etc.).
*/

USE master;
GO

IF DB_ID(N'Piscicultura') IS NULL
BEGIN
    CREATE DATABASE [Piscicultura];
END
GO

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @LoginWindows)
BEGIN
    DECLARE @sql NVARCHAR(500) = N'CREATE LOGIN ' + QUOTENAME(@LoginWindows) + N' FROM WINDOWS';
    EXEC sp_executesql @sql;
END
GO

USE [Piscicultura];
GO

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @LoginWindows)
BEGIN
    DECLARE @sql2 NVARCHAR(500) = N'CREATE USER ' + QUOTENAME(@LoginWindows) + N' FOR LOGIN ' + QUOTENAME(@LoginWindows);
    EXEC sp_executesql @sql2;
END
GO

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';

IF NOT EXISTS (
    SELECT 1
    FROM sys.database_role_members drm
    INNER JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
    INNER JOIN sys.database_principals m ON drm.member_principal_id = m.principal_id
    WHERE r.name = N'db_owner' AND m.name = @LoginWindows)
BEGIN
    DECLARE @sql3 NVARCHAR(500) = N'ALTER ROLE db_owner ADD MEMBER ' + QUOTENAME(@LoginWindows);
    EXEC sp_executesql @sql3;
END
GO
